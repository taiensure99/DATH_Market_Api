using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATH_ASP.NET.Models
{
    public class Shipper
    {
        public ObjectId Id { get; set; }
        public string dateOfBirth { get; set; }
        public string fullname { get; set; }
        public Boolean isApprove { get; set; }
        public string numberID { get; set; }
        public string phoneNumber { get; set; }
        public Address address { get; set; }
    }

    public class Address
    {
        public string province { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        public string street { get; set; }
        public string home_number { get; set; }
    }
}
