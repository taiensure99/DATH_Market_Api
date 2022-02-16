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
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly object filterBuilder;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult GetTotalOrder()
        {
            var start = DateTime.Now;
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            var database = dbClient.GetDatabase("PTTKHTTTHD");
            var collection = database.GetCollection<Order>("order");
            var query = collection.AsQueryable();

            return new JsonResult(query);
        }

        [Route("topProduct")]
        [HttpGet]
        public JsonResult GetTopProduct()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            var database = dbClient.GetDatabase("PTTKHTTTHD");
            var collection = database.GetCollection<Order>("order");
            var query = collection.AsQueryable()
                        .ToList();
            var listproduct = new List<List<Product>>();
            foreach (var item in query)
            {
                listproduct.Add(item.product);
            }

            List<ResultOrder> rsOr = new List<ResultOrder>();
            foreach (var item1 in listproduct) 
            {
                #region c1
                // them tung phan tu vao chuoi co san
               //var test = item1.Select(x => new ResultOrder { ProductName = x.productName, Quantity = x.quantity });
               //foreach(var item2 in test)
               //{
               //     rsOr.Add(item2);
               //}
                #endregion
                
               // c2 them noi tiep vao chuoi co san 
                rsOr.AddRange(item1.Select(x => new ResultOrder { ProductName = x.productName, Quantity = x.quantity }));
            }

            
            var topProduct = rsOr.GroupBy(x => x.ProductName, (k, s) => new { Product = k, Quantity = s.Select(p => p. Quantity).Sum()})
                    .OrderByDescending(group => group.Quantity).Take(10); 
            return new JsonResult(topProduct);
        }

        [Route("topStatisticalProduct")]
        [HttpGet]
        public JsonResult GetTopProductYear([FromQuery] int? option)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("AppCon"));
            var database = dbClient.GetDatabase("PTTKHTTTHD");
            var collection = database.GetCollection<Order>("order");
            var query = collection.AsQueryable().ToList();
            var lsMonth = query.GroupBy(x => new { Year = x.orderDate.Year, Month = x.orderDate.Month }).ToList();
            var listproduct = new List<List<Product>>();
            List<ResultOrder> results = new List<ResultOrder>();
            switch (option)
            {
                case 1: //year
                    var lsYear = query.GroupBy(x => new { Year = x.orderDate.Year }).ToList();
                    foreach(var item in lsYear)
                    {
                        var lsProYear = query.Where(x => x.orderDate.Year == item.Key.Year).ToList();
                        List<List<Product>> lsOrderProDuct = new List<List<Product>>();
                        var lspro = lsProYear.Select(x => x.product).ToList();
                        lsOrderProDuct.AddRange(lspro);

                        List<Product> lsPr = new List<Product>();
                        foreach(var itemOrPro in lsOrderProDuct)
                        {
                            foreach(var itemPro in itemOrPro)
                            {
                                var itemRs = new ResultOrder()
                                {
                                    Year = item.Key.Year,
                                    ProductName = itemPro.productName,
                                    Quantity = itemPro.quantity
                                    
                                };
                                results.Add(itemRs);
                            }    
                            
                        }                       
                    }

                    results = results.GroupBy(x => new { Year = x.Year, ProductName = x.ProductName })
                                       .Select(y => new ResultOrder
                                       {
                                           Year = y.Key.Year,
                                           ProductName = y.Key.ProductName,
                                           Quantity = y.Sum(z => z.Quantity)
                                       }).ToList();                    
                    break;
                case 2: //Month
                    foreach (var item in lsMonth)
                    {
                        var lsProYear = query.Where(x => x.orderDate.Year == item.Key.Year 
                                                    && x.orderDate.Month == item.Key.Month)
                                               .ToList();
                        List<List<Product>> lsOrderProDuct = new List<List<Product>>();
                        var lsPro = lsProYear.Select(x => x.product).ToList();
                        lsOrderProDuct.AddRange(lsPro);

                        List<Product> lsPr = new List<Product>();
                        foreach (var itemOrPro in lsOrderProDuct)
                        {
                            foreach (var itemPro in itemOrPro)
                            {
                                var itemRs = new ResultOrder()
                                {
                                    Year = item.Key.Year,
                                    ProductName = itemPro.productName,
                                    Month = item.Key.Month,
                                    Quantity = itemPro.quantity

                                };
                                results.Add(itemRs);
                            }

                        }
                    }

                    results = results.GroupBy(x => new { Year = x.Year, ProductName = x.ProductName, Month = x.Month })
                                      .Select(y => new ResultOrder
                                      {
                                          Year = y.Key.Year,
                                          ProductName = y.Key.ProductName,
                                          Month = y.Key.Month,
                                          Quantity = y.Sum(z => z.Quantity)
                                      }).ToList();
                    break;
                case 3: //Quater
                    foreach (var item in lsMonth)
                    {
                        var lsProYear = query.Where(x => x.orderDate.Year == item.Key.Year
                                                    && x.orderDate.Month == item.Key.Month)
                                               .ToList();
                        List<List<Product>> lsOrderProDuct = new List<List<Product>>();
                        var lsPro = lsProYear.Select(x => x.product).ToList();
                        lsOrderProDuct.AddRange(lsPro);

                        List<Product> lsPr = new List<Product>();
                        foreach (var itemOrPro in lsOrderProDuct)
                        {
                            foreach (var itemPro in itemOrPro)
                            {
                                var itemRs = new ResultOrder()
                                {
                                    Year = item.Key.Year,
                                    ProductName = itemPro.productName,
                                    Month = item.Key.Month,
                                    Quantity = itemPro.quantity

                                };
                                results.Add(itemRs);
                            }

                        }
                    }

                    results = results.GroupBy(x => new { Year = x.Year, ProductName = x.ProductName, Month = x.Month })
                                      .Select(y => new ResultOrder
                                      {
                                          Year = y.Key.Year,
                                          ProductName = y.Key.ProductName,
                                          Month = y.Key.Month,
                                          Quantity = y.Sum(z => z.Quantity),
                                          Quater = GetQuater(y.Key.Month)
                                      }).ToList();

                    var ts = results.GroupBy(x => x.Quater).ToList();
                    List<ResultOrder> rsQua = new List<ResultOrder>();
                    foreach(var itemrs in ts)
                    {
                        rsQua.AddRange(itemrs);
                    }
                    results = rsQua;
                    break;

            }

            return new JsonResult(results); 
        }

        
        private int GetQuater(int Month)
        {
            if (Month == 0) return 0;
            int rs = 0;
            switch(Month)
            {
                case 1:
                case 2:
                case 3:
                    rs = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    rs = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    rs = 3;
                    break;
                default:
                    rs = 4;
                    break;
            }    
            return rs;
        }
        
    }
}
