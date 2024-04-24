using AutoMapper;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Prices;
using DormSearchBe.Domain.Entity;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Service
{
    public class PricesService : IPricesService
    {
        private readonly IPricesRepository _priceRepository;
        private readonly IMapper _mapper;
        public PricesService(IPricesRepository priceRepository, IMapper mapper)
        {
            _priceRepository = priceRepository;
            _mapper = mapper;
        }

        public DataResponse<PricesQuery> Create(PricesDto dto)
        {
            dto.PriceId = Guid.NewGuid();
            var newData = _priceRepository.Create(_mapper.Map<Prices>(dto));
            if (newData != null)
            {
                return new DataResponse<PricesQuery>(_mapper.Map<PricesQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.AddedSuccesfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.AddedError);
        }

        public DataResponse<PricesQuery> Delete(Guid id)
        {
            var item = _priceRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var data = _priceRepository.Delete(id);
            if (data != null)
            {
                return new DataResponse<PricesQuery>(_mapper.Map<PricesQuery>(item), HttpStatusCode.OK, HttpStatusMessages.DeletedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.DeletedError);
        }

        public DataResponse<PricesQuery> GetById(Guid id)
        {
            var item = _priceRepository.GetById(id);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            return new DataResponse<PricesQuery>(_mapper.Map<PricesQuery>(item), HttpStatusCode.OK, HttpStatusMessages.OK);
        }

        public PagedDataResponse<PricesQuery> Items(CommonListQuery commonList)
        {
            var query = _mapper.Map<List<PricesQuery>>(_priceRepository.GetAllData());
            if (!string.IsNullOrEmpty(commonList.keyword))
            {
                query = query.Where(x => x.Price.Contains(commonList.keyword)).ToList();
            }
            var paginatedResult = PaginatedList<PricesQuery>.ToPageList(query, commonList.page, commonList.limit);
            return new PagedDataResponse<PricesQuery>(paginatedResult, 200, query.Count());
        }

        public DataResponse<List<PricesDto>> ItemsList()
        {
            var query = _priceRepository.GetAllData();
            if (query != null && query.Any())
            {
                var PricesDtos = _mapper.Map<List<PricesDto>>(query);
                return new DataResponse<List<PricesDto>>(_mapper.Map<List<PricesDto>>(query), HttpStatusCode.OK, HttpStatusMessages.OK);
            }
            throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
        }


        public DataResponse<PricesQuery> Update(PricesDto dto)
        {
            var item = _priceRepository.GetById(dto.PriceId);
            if (item == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, HttpStatusMessages.NotFound);
            }
            var newData = _priceRepository.Update(_mapper.Map(dto, item));
            if (newData != null)
            {
                return new DataResponse<PricesQuery>(_mapper.Map<PricesQuery>(newData), HttpStatusCode.OK, HttpStatusMessages.UpdatedSuccessfully);
            }
            throw new ApiException(HttpStatusCode.BAD_REQUEST, HttpStatusMessages.UpdatedError);
        }
    }
}
