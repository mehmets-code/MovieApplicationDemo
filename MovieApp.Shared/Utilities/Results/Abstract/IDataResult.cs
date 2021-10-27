using MovieApp.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T>:IResult
    {
        public T Data { get; }
    }
}
