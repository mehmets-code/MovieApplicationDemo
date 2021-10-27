﻿using MovieApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entities.Concrete
{
    public class MovieCategory : IEntity
    {
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}