using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
public sealed  class ItemNotFoundException(int id) : Exception ($"Item with id {id} not found.")
    {
    }
}
