using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATH_ASP.NET.Models
{
    public class User
    {
        public ObjectId Id { get; set; }

        public string name { get; set; }

        public string username { get; set; }

        public string passWord { get; set; }

        public string phone { get; set; }

        public string province { get; set; }

        public string district { get; set; }

        public string ward { get; set; }

        public string street { get; set; }

        public int role { get; set; }
    }
}
