using DormSearchBe.Domain.Entity;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Infrastructure.Repositories
{
    public class HousesRepository:GenericRepository<Houses>,IHousesRepository
    {
        public HousesRepository(DormSearch_DbContext context) : base(context) { }   
    }
}
