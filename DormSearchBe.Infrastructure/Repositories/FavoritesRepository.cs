﻿using DormSearchBe.Domain.Entity;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Infrastructure.Repositories
{
    public class FavoritesRepository:GenericRepository<Favorites>,IFavoritesRepository
    {
        public FavoritesRepository(DormSearch_DbContext context) : base(context) { }
    }
}
