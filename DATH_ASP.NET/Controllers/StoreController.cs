﻿using DATH_ASP.NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATH_ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StoreController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            var database = dbClient.GetDatabase("PTTKHTTTHD");
            var collection = database.GetCollection<Stores>("store");
            var query = collection.AsQueryable()
                        .GroupBy(p => new { p.address.district, p.address.province }, (k, s) => new { District = k, Count = s.Count() });
            return new JsonResult(query);
        }

        //get all store
        [Route("All")]
        [HttpGet]
        public JsonResult GetAll()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            var database = dbClient.GetDatabase("PTTKHTTTHD");
            var collection = database.GetCollection<Stores>("store");
            var query = collection.AsQueryable();
            return new JsonResult(query);
        }

        //DetailStore
        [HttpGet("{id}")]
        public JsonResult GetStoreDetail(String id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            var connection = dbClient.GetDatabase("PTTKHTTTHD").GetCollection<Stores>("store");
            var filter = Builders<Stores>.Filter.Eq("_id", new ObjectId(id));
            var result = connection.Find(filter).ToList();

            return new JsonResult(result);
        }
    }
}