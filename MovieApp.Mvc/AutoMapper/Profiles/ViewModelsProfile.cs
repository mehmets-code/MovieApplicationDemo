using AutoMapper;
using MovieApp.Entities.Dtos;
using MovieApp.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.AutoMapper.Profiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<MovieEditViewModel, MovieAddDto>();
            CreateMap<MovieAddDto,MovieEditViewModel>();
            CreateMap<MovieEditViewModel, MovieUpdateDto>();
            CreateMap<MovieUpdateDto, MovieEditViewModel>();
        }
    }
}
