using AutoMapper;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.AutoMapper.Profiles
{
    public class MovieProfile:Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieAddDto, Movie>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<MovieAddDto, Movie>().ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<MovieUpdateDto, Movie>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<MovieUpdateDto, Movie>().ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
        }
    }
}
