using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        List<T> GetAllData();
        T GetById(Guid id);
        T Create(T entity);
        T Update(T entity);
        T Delete(Guid id);
        T GetByString(string id);
        T DeleteByString(string id);
        T GetByName(string name);

    }
}
