using Microsoft.AspNetCore.Http;
using MovieApp.Entities.ComplexTypes;
using MovieApp.Entities.Dtos;
using MovieApp.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null) ;
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
