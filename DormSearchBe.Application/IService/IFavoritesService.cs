using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Favorites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IFavoritesService
    {
        PagedDataResponse<FavoritesDto> Items(CommonListQuery commonList);
        DataResponse<List<FavoritesDto>> ItemsList();
        DataResponse<FavoritesDto> Create(FavoritesDto dto);
        DataResponse<FavoritesDto> Update(FavoritesDto dto);
        DataResponse<FavoritesDto> Delete(Guid id);
        DataResponse<FavoritesDto> GetById(Guid id);
    }
}
