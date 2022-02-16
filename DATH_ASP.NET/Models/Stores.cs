using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATH_ASP.NET.Models
{
    public class Stores
    {
        public ObjectId id { get; set; }
        public string name { get; set; }
        public Address address { get; set; }
        public string phone_number { get; set; }
        public string isApprove { get; set; }
        public DateTime createdAt { get; set; }
        public string _class { get; set; }
    }
}
