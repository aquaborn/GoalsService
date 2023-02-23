using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Core.Exceptions
{
    public interface IApiException
    {
        List<string> Errors { get;}
    }
}
