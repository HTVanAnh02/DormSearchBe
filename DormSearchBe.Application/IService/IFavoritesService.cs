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
        PagedDataResponse<FavoritesQuery> Items(CommonListQuery commonList);
        DataResponse<List<FavoritesDto>> ItemsList();
        DataResponse<FavoritesQuery> Create(FavoritesDto dto);
        DataResponse<FavoritesQuery> Update(FavoritesDto dto);
        DataResponse<FavoritesQuery> Delete(Guid id);
        DataResponse<FavoritesQuery> GetById(Guid id);
    }
}
