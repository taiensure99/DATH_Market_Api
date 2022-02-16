using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATH_ASP.NET.Models
{
    public class Order
    {
        public ObjectId id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime orderDate { get; set; }
        public string fullname { get; set; }
        public string phoneNumber { get; set; }
        public int totalPrice { get; set; }
        public Address address { get; set; }
        public List<Product> product { get; set; }
        public string status { get; set; }
        public Payment paymentOnline { get; set; }
        public string payments { get; set; }
        public string customerId { get; set; }
        public string discount { get; set; }
        public string shipperId { get; set; }
        public string shipperName { get; set; }
        public string storeId { get; set; }
        public string _class { get; set; }
    }


    public class Product
    {
        public string productID { get; set; }
        public string productName { get; set; }
        public int productPrice { get; set; }
        public int quantity { get; set; }
        public string productImage { get; set; }
        public string unit { get; set; }

    }

    public class Payment
    {
        public string nameOnCard { get; set; }
        public string creditCardNumber { get; set; }
        public string bankName { get; set; }
        public string expYear { get; set; }
        public string cvcNumber { get; set; }
    }

    public class ResultOrder
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Quater { get; set; } //1,2,3,4
    }
}
