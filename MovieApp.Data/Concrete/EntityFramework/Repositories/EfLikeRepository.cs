﻿using Microsoft.EntityFrameworkCore;
using MovieApp.Data.Abstract;
using MovieApp.Entities.Concrete;
using MovieApp.Shared.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Data.Concrete.EntityFramework.Repositories
{
    public class EfLikeRepository : EfEntityRepositoryBase<Like>, ILikeRepository
    {
        public EfLikeRepository(DbContext context) : base(context)
        {
        }
    }
}
