using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }

        [Required(ErrorMessage = "User required to create order")]
        public User user { get; set; }

        public Subscription subscription { get; set; }

        [Required(ErrorMessage = "First name required to create order")]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Last name required to create order")]
        public string last_name { get; set; }

        [Required(ErrorMessage = "Address 1 required to create order")]
        public string address_1 { get; set; }

        [Required(ErrorMessage = "Address 2 required to create order")]
        public string address_2 { get; set; }

        [Required(ErrorMessage = "City required to create order")]
        public string city { get; set; }

        [Required(ErrorMessage = "State required to create order")]
        public string state { get; set; }

        [Required(ErrorMessage = "Zip required to create order")]
        public int zip { get; set; }

        [Required(ErrorMessage = "Billing Address 1 required to create order")]
        public string billing_address_1 { get; set; }

        [Required(ErrorMessage = "Billing Address 2 required to create order")]
        public string billing_address_2 { get; set; }

        [Required(ErrorMessage = "Billing City required to create order")]
        public string billing_city { get; set; }

        [Required(ErrorMessage = "Billing State required to create order")]
        public string billing_state { get; set; }

        [Required(ErrorMessage = "Billing Zip required to create order")]
        public int billing_zip { get; set; }

        public int provider_id { get; set; }

        public string initial_url { get; set; }

        public bool rebill { get; set; }

        public string response { get; set; }

        public bool success { get; set; }

        public Company company { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IOrderRepository
    {
        Order GetOrder(int order_id);
        Order CreateOrder(Order order);
        void Save();
    }

    public class OrderRepository : IOrderRepository
    {
        private ApplicationContext context;

        public OrderRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Order CreateOrder(Order order)
        {
            order.created = DateTime.Now;
            order.updated = DateTime.Now;

            context.Orders.Add(order);
            Save();
            return order;
        }

        public Order GetOrder(int order_id)
        {
            return context.Orders.Find(order_id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}