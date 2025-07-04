using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
   public sealed class CartNotFoundException(string id) : NotFoundException($"Cart with Id{id} Is Not Found")
    {


    }
}
