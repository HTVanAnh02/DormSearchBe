using AutoMapper;
using CloudinaryDotNet;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Auth;
using DormSearchBe.Domain.Dto.Role;
using DormSearchBe.Domain.Dto.User;
using DormSearchBe.Domain.Entity;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TokenDto = DormSearchBe.Domain.Dto.User.TokenDto;

namespace DormSearchBe.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IApprovaRepository _approvalRepository;
        private readonly IRefreshTokenRepository _refeshtokenRepository;
        private readonly JWTSettings _jwtSettings;
        private readonly Cloudinary _cloudinary;

        public UserService(IOptions<JWTSettings> jwtSettings, IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository, IApprovaRepository approvalRepository, Cloudinary cloudinary, IRefreshTokenRepository refeshtokenRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _approvalRepository = approvalRepository;
            _cloudinary = cloudinary;
            _jwtSettings = jwtSettings.Value;
            _refeshtokenRepository = refeshtokenRepository;
        }
        public DataResponse<UserQuery> Create(UserDto dto)
        {
            UpLoadImage upload = new UpLoadImage(_cloudinary);
            dto.UserId = Guid.NewGuid();
            dto.Password = PasswordHelper.CreateHashedPassword(dto.Password = "12345678a@");
            if (dto.file != null)
            {
                dto.Avatar = upload.ImageUpload(dto.file);
            }
            var newData = _userRepository.Create(_mapper.Map<User>(dto));
            if (newData != null)
            {
                return new DataResponse<UserQuery>(_mapper.Map<UserQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);
        }

        public DataResponse<UserQuery> Delete(Guid id)
        {
            UpLoadImage upload = new UpLoadImage(_cloudinary);

            var item = _userRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            if (item.Avatar != null)
            {
                upload.DeleteImage(item.Avatar);
            }
            var data = _userRepository.Delete(id);
            if (data != null)
            {
                return new DataResponse<UserQuery>(_mapper.Map<UserQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);
        }

        public PagedDataResponse<UserQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<UserQuery>>(_userRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x =>
                    x.FullName.Contains(commonList.keyword) ||
                    x.Email.Contains(commonList.keyword)
                ).ToList();
            }
            foreach (var item in query)
            {
                var approval = _roleRepository.GetById(item.RoleId ?? Guid.Empty);
                if (approval != null)
                {
                    item.RoleName = approval.RoleName;
                }
            }
            var paginatedResult = PaginatedList<UserQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<UserQuery>(paginatedResult, 200, query.Count());

        }

        public DataResponse<UserQuery> Update(UserDto dto)
        {
            UpLoadImage upload = new UpLoadImage(_cloudinary);
            var item = _userRepository.GetById(dto.UserId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            if (dto.imageDelete != null)
            {
                upload.DeleteImage(dto.imageDelete);
            }
            if (dto.file != null)
            {
                if (item.Avatar != null)
                {
                    upload.DeleteImage(item.Avatar);
                }
                dto.Avatar = upload.ImageUpload(dto.file);
            }
            var newData = _userRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<UserQuery>(_mapper.Map<UserQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }

        public DataResponse<UserQuery> GetById(Guid id)
        {
            var item = _userRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<UserQuery>(_mapper.Map<UserQuery>(item), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
        }

    
        public DataResponse<TokenDto> Login(Login dto)
        {
            var user = _userRepository.GetAllData().Where(x => x.Email == dto.Email).SingleOrDefault();
            if (user == null)
            {
                throw new ApiException(401, "Tài khoản không tồn tại");
            }
            var isPasswordValid = PasswordHelper.VerifyPassword(dto.Password, user.Password);
            if (!isPasswordValid)
            {
                throw new ApiException(401, "Mật khẩu không chính xác");
            }
            else
            {
                _userRepository.Update(user);
                return new DataResponse<TokenDto>(CreateToken(user), 200, "Đăng nhập thành công");
            }
            throw new ApiException(401, "Đăng nhập thất bại");
        }
        public DataResponse<TokenDto> Register(Register dto)
        {
            try
            {
                var existingUser = _userRepository.GetAllData().FirstOrDefault(x => x.Email == dto.Email);
                if (existingUser != null)
                {
                    throw new ApiException(400, "Email đã được sử dụng cho một tài khoản khác");
                }

                var newUser = new User
                {
                    Email = dto.Email,
                    Password = PasswordHelper.CreateHashedPassword(dto.Password),
                    // Các thuộc tính khác của người dùng
                };

                _userRepository.Create(newUser);

                return new DataResponse<TokenDto>(CreateToken(newUser), 200, "Đăng ký thành công");
            }
            catch (Exception)
            {
                throw new ApiException(500, "Đã xảy ra lỗi khi đăng ký người dùng.");
            }
        }
        public DataResponse<UserDto> GetProfile(Guid userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                throw new ApiException(404, "Người dùng không tồn tại");
            }

            var profile = new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Avatar = user.Avatar,
            };

            return new DataResponse<UserDto>(profile, 200, "Lấy thông tin profile thành công");
        }
        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        public TokenDto CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_jwtSettings.RefreshTokenExpiration);
            var securityKey = Encoding.ASCII.GetBytes(_jwtSettings.SecurityKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey),
                SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience[0],
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaims(user,_jwtSettings.Audience),
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = (int)((DateTimeOffset)accessTokenExpiration).ToUnixTimeSeconds(),
                RefreshTokenExpiration = (int)((DateTimeOffset)refreshTokenExpiration).ToUnixTimeSeconds(),
                Role = user.Role
            };
            var refresh_token = new RefreshTokenDto
            {
                UserId = user.UserId,
                RefreshToken = tokenDto.RefreshToken,
                RefreshTokenExpiration = tokenDto.RefreshTokenExpiration,
                Refresh_TokenExpires = refreshTokenExpiration
            };
            if (_refeshtokenRepository.GetById(user.UserId) == null)
            {
                _refeshtokenRepository.Create(_mapper.Map<Refresh_Token>(refresh_token));
            }
            else
            {
                _refeshtokenRepository.Update(_mapper.Map<Refresh_Token>(refresh_token));
            }
            return tokenDto;
        }
        private IEnumerable<Claim> GetClaims(User user, List<string> audiences)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,user.Role)
            };
            claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return claims;
        }
        public DataResponse<TokenDto> Refresh_Token(RefreshTokenSettings token)
        {
            var existRefreshToken = _refeshtokenRepository.GetAllData().Where(x => x.RefreshToken == token.Refresh_Token).FirstOrDefault();
            if (existRefreshToken == null)
            {
                throw new ApiException(404, "Refresh Token không hợp lệ");
            }
            var user = _userRepository.GetById(existRefreshToken.UserId);
            if (user == null)
            {
                throw new ApiException(404, "Thông tin người dùng không tồn tại");
            }
            if (existRefreshToken.Refresh_TokenExpires < DateTime.Now)
            {
                throw new ApiException(404, "Refresh Token hết hạn");
            }

            return new DataResponse<TokenDto>(RefreshToken(user, _mapper.Map<RefreshTokenDto>(existRefreshToken)), 200, "Refresh token success");
        }
        public TokenDto RefreshToken(User user, RefreshTokenDto refreshtoken)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_jwtSettings.RefreshTokenExpiration);
            var securityKey = Encoding.ASCII.GetBytes(_jwtSettings.SecurityKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey),
                SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience[0],
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaims(user, _jwtSettings.Audience),
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = (int)((DateTimeOffset)accessTokenExpiration).ToUnixTimeSeconds(),
                RefreshTokenExpiration = refreshtoken.RefreshTokenExpiration,
            };
            var refresh_token = new RefreshTokenDto
            {
                UserId = user.UserId,
                RefreshToken = tokenDto.RefreshToken,
                RefreshTokenExpiration = refreshtoken.RefreshTokenExpiration,
                Refresh_TokenExpires = refreshtoken.Refresh_TokenExpires
            };
            _refeshtokenRepository.Update(_mapper.Map<Refresh_Token>(refresh_token));
            return tokenDto;
        }

       /* public DataResponse<TokenDto> UserLoginByGoole(GoogleLoginRequest request)
        {
            try
            {
                var validPayload = GoogleJsonWebSignature.ValidateAsync(request.Credential).Result;
                if (validPayload == null)
                {
                    throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.TokenOrUserNotFound);
                }
                if (!checkEmail(validPayload.Email))
                {

                    var job_seeker = _userRepository.GetAllData().Where(x => x.Email == validPayload.Email).SingleOrDefault();
                    _userRepository.Update(job_seeker);
                    var token = CreateToken(job_seeker);
                    var tokenres = new TokenDto()
                    {
                        AccessToken = token.AccessToken,
                        AccessTokenExpiration = token.AccessTokenExpiration,
                        RefreshToken = token.RefreshToken,
                        RefreshTokenExpiration = token.RefreshTokenExpiration,
                        Role = token.Role

                    };
                    return new DataResponse<TokenDto>(tokenres, 200, "Đăng nhập thành công");
                }
                else
                {

                    var dto = new UserDto()
                    {
                        UserId = Guid.NewGuid(),
                        Email = validPayload.Email,
                        Avatar = validPayload.Picture,
                        FullName = validPayload.Name,
                    };
                    var user = _userRepository.Create(_mapper.Map<User>(dto));
                    var token = CreateToken(_mapper.Map<User>(user));
                    var tokenres = new TokenDto()
                    {
                        AccessToken = token.AccessToken,
                        AccessTokenExpiration = token.AccessTokenExpiration,
                        RefreshToken = token.RefreshToken,
                        RefreshTokenExpiration = token.RefreshTokenExpiration,
                        Role = token.Role
                    };
                    return new DataResponse<TokenDto>(tokenres, 200, "Đăng nhập thành công");
                }
            }
            catch (InvalidJwtException)
            {
                throw new ApiException(HttpStatusCode.BAD_REQUEST, "Token không hợp lệ");
            }
        }*/
        public bool checkEmail(string email)
        {
            var job_seeker = _userRepository.GetAllData().FirstOrDefault(x => x.Email.Equals(email));
            if (job_seeker == null)
            {
                return true;
            }
            return false;
        }

        public DataResponse<List<UserDto>> ItemsList()
        {
            throw new NotImplementedException();
        }


        public DataResponse<TokenDto> UserLoginByGoole(GoogleLoginRequest request)
        {
            throw new NotImplementedException();
        }

        public List<Approval> GetUserApproval(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
