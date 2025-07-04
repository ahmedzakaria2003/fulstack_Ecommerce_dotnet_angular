using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using DomainLayer.Models.OrderModule;
using Service.Specification;
using Service.Specification.OrderModuleSpecifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTOS;
using Shared.DataTransferObjects.OrderDTOS;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainProduct = DomainLayer.Models.Product;

namespace Service
{
    public class OrderService(IMapper _mapper , IBasketRepoistory _cartRepoistory , IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO, string Email)
        {
          
            // Map Address To OrderAddress 
            var OrderAddress =  _mapper.Map<AdressDTO , OrderAddress> (orderDTO.shipToAddress);

            // Get Cart
            var Cart = await _cartRepoistory.GetCartAsync(orderDTO.CartId)
                ?? throw new CartNotFoundException(orderDTO.CartId);

            ArgumentNullException.ThrowIfNullOrEmpty(Cart.paymentIntentId);
            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();  
            var OrderSpec = new OrderWithPayementIntentIdSpecification(Cart.paymentIntentId);
            var ExistingOrder = await OrderRepo.GetbyIdAsync(OrderSpec);
            if (ExistingOrder != null)
            {
                OrderRepo.Remove(ExistingOrder);
            }
            // Create Order Item List
            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<DomainProduct , int>();
            foreach (var item in Cart.Items)
            {
                var Product = await ProductRepo.GetbyIdAsync(item.Id) 
                    ?? throw new ProductNotFoundExeptions(item.Id);
                var OrderItem = new OrderItem() { 
                Product = new ProductItemOrdered() { 
                   ProductItemId = Product.Id , 
                 ProductName = Product.Name , 
                    PictureUrl = Product.PictureUrl
                },

                Price = Product.Price,
                Quantity = item.Quantity

                };
                OrderItems.Add(OrderItem);
            }

            // Get Delivery Method
            var DeliveryMethodRepo = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetbyIdAsync(orderDTO.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDTO.DeliveryMethodId);

            // Calculate Subtotal
            var Subtotal = OrderItems.Sum(item => item.Price * item.Quantity);
            var Order = new Order(Email, OrderAddress, DeliveryMethodRepo, OrderItems, Subtotal , Cart.paymentIntentId);

            await  OrderRepo.AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order, OrderToReturnDTO>(Order);

        }

      

        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTO>>(Orders);

        }

        public async Task<IEnumerable<DeliveryMethodsDTO>> GetDeliveryMethodsAsync()
        {
           var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod , int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod> , IEnumerable<DeliveryMethodsDTO>>(DeliveryMethods);
        }

        public async Task<OrderToReturnDTO> GetOrderByIdAsync(Guid Id)
        {
            var Spec = new OrderSpecifications(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetbyIdAsync(Spec);

            return _mapper.Map<Order, OrderToReturnDTO>(Order);
             

        }
    }
}
