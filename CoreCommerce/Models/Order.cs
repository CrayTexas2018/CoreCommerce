using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCommerce.Models
{
    public class Order : CommonFields
    {
        [Key]
        public int order_id { get; set; }

        public int user_id { get; set; }

        [Required(ErrorMessage = "User required to create order")]
        [ForeignKey("user_id")]
        [Column("user_id")]
        [JsonIgnore]
        public User user { get; set; }

        public int? subscription_id { get; set; }

        [ForeignKey("subscription_id")]
        [Column("subscription_id")]
        [JsonIgnore]
        public Subscription subscription { get; set; }

        public int checkout_id { get; set; }

        [JsonIgnore]
        [ForeignKey("checkout_id")]
        public Checkout checkout { get; set; }

        public string stripe_charge_id { get; set; }

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

        public long shopify_id { get; set; }

        public string stripe_id { get; set; }

        public bool rebill { get; set; }

        public string response { get; set; }

        public bool success { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostOrder
    {
        public int user_id { get; set; }

        public int? subsciption_id { get; set; }

        public int? checkout_id { get; set; }

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
    }

    public interface IOrderRepository
    {
        Order GetOrder(int order_id);
        Order CreateOrder(PostOrder order);
        void Save();
    }

    public class OrderRepository : IOrderRepository
    {
        private ApplicationContext context;

        public OrderRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Order CreateOrder(PostOrder postOrder)
        {
            UserRepository ur = new UserRepository(context);
            SubscriptionRepository sr = new SubscriptionRepository(context);

            Order order = new Order
            {
                active = true,
                address_1 = postOrder.address_1,
                address_2 = postOrder.address_2,
                billing_address_1 = postOrder.billing_address_1,
                billing_address_2 = postOrder.billing_address_2,
                billing_city = postOrder.billing_city,
                billing_state = postOrder.billing_state,
                billing_zip = postOrder.billing_zip,
                city = postOrder.city,
                first_name = postOrder.first_name,
                last_name = postOrder.last_name,
                state = postOrder.state,
                user_id = postOrder.user_id,
                zip = postOrder.zip,
            };

            order.created = DateTime.Now;
            order.updated = DateTime.Now;
            order.user = ur.GetUserById(postOrder.user_id);
            order.subscription = sr.GetSubscription(postOrder.subsciption_id);

            if (postOrder.checkout_id == null)
            {
                CheckoutRepository cr = new CheckoutRepository(context);
                order.checkout_id = cr.GetLastUserCheckout(postOrder.user_id).checkout_id;
            }
            else
            {
                order.checkout_id = (int)postOrder.checkout_id;
            }

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