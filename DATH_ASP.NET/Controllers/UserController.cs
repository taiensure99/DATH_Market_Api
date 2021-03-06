using DATH_ASP.NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATH_ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            var database = dbClient.GetDatabase("PTTKHTTTHD");
            var collection = database.GetCollection<User>("User");
            var query = collection.AsQueryable()
                            .GroupBy(p => new { p.district, p.province }, (k, s) => new { District = k, Count = s.Count() });
            return new JsonResult(query);
        }

    }
}
