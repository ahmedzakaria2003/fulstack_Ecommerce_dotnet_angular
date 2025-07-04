using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification.OrderModuleSpecifications
{
    class OrderSpecifications : BaseSpecification<Order , Guid>
    {
        // Get All Orders By User Email
        public OrderSpecifications(string Email) : base(O => O.BuyerEmail == Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.OrderItems);
            AddOrderByDescending(O => O.OrderDate);

        }
        // Get Order By Id 
        public OrderSpecifications(Guid Id) : base(O => O.Id == Id )
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.OrderItems);
        }

    }
}
