
using BlueCat.Api.Common;
using BlueCat.Api.Service.Interface;
using BlueCat.Contract;
using BlueCat.Jwt;
using BlueCat.Jwt.Algorithms;
using BlueCat.Jwt.Serializers;
using FluentValidation.Results;
using JWT;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BlueCat.Api.Controllers
{
    public class ProductsController : ApiController
    {
        [Import]
        public IProductService ProductService { get; set; }



        Product[] products = new Product[] 
        { 
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M } 
        };

        [Route("v1/products/get")]
        [HttpGet]
        public IEnumerable<Product>GetAllProducts()
        {

            return products;
        }


        [Route("v1/products/get")]
        [HttpPost]
        public IHttpActionResult GetAllProduct([FromBody] Product product)
        {
            ProductValidator productValidator = new ProductValidator();
            ValidationResult result = productValidator.Validate(product);
            if (!result.IsValid)
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                });
                return BadRequest(ModelState);
            }

            return Ok(products);
        }


        public class SimpleError
        {
            public string PropertyName { get; set; }
            public string ErrorMessage { get; set; }
        }


        [Route("v1/token/getToken")]
        [HttpGet]
        public String GetToken()
        {
            var payload = new Dictionary<string, object>
            {
                { "username", "xuliangjie" },
                 { "id", 11111111 },
                { "exprisetime", TimeConvert.ConvertDateTimeInt(DateTime.Now.AddMinutes(30)) }
            };
            var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);

            return token;
        }

        [Route("v1/verification/token")]
        [HttpGet]
        public String VerificationToken()
        {

            var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJjbGFpbTEiOjAsImNsYWltMiI6ImNsYWltMi12YWx1ZSJ9.8pwBI_HtXqI3UgQHQ_rDRnSQRxFL1SR8fbQoS-5kM5s";
            var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, secret, verify: true);
                return json;
            }
            catch (TokenExpiredException)
            {
                return "Token has expired";
            }
            catch (SignatureVerificationException)
            {
                return "Token has invalid signature";
            }
        }



        [HttpGet]
        [ActionName("Thumbnail")]
        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return products.Where(
                (p) => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}
