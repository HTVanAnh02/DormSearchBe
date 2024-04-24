using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IRatingsService
    {
        PagedDataResponse<RatingsQuery> Items(CommonListQuery commonList, Guid id);
        DataResponse<RatingsQuery> Create(RatingsDto dto);
        DataResponse<RatingsQuery> Update(RatingsDto dto);
        DataResponse<RatingsQuery> Delete(Guid id);
        DataResponse<RatingsQuery> GetById(Guid id);
        PagedDataResponse<RatingsList> ItemsByUser(CommonQueryByHome commonList, Guid id);
        PagedDataResponse<RatingsList> ItemsByUserSuitable(CommonQueryByHome commonList, Guid id);
        PagedDataResponse<RatingsList> ItemsByStudents(CommonListQuery commonList, Guid id);
        DataResponse<RatingsList> ChangeStatus(RatingsChangeStatus changeStatus);
        DataResponse<RatingsList> ChangeFeedback(RatingsChangeFeedback changeFeedback);
    }
}
