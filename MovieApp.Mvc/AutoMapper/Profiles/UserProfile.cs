using AutoMapper;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<User, UserAddDto>();
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
