using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
public class OrderWithPayementIntentIdSpecification : BaseSpecification<Order , Guid>
    {
        public OrderWithPayementIntentIdSpecification(string PaymentIntentId):base(O=>O.PaymentIntentId == PaymentIntentId)
        {
            
        }


    }
}
