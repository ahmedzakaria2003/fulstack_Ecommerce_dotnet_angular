using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class BadRequestException(List<string> errors) : Exception("Validtion Error")
    {
        public List<string> errors { get; } = errors;
    }
}
