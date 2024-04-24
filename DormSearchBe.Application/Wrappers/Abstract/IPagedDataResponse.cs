using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.Wrappers.Abstract
{
    public interface IPagedDataResponse<T> : IResponse
    {
        int TotalItems { get; }
        List<T> Items { get; }
    }
}
