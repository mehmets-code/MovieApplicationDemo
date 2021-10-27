using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;

namespace MovieApp.Services.AutoMapper.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentAddDto, Comment>();



        }
    }
}
