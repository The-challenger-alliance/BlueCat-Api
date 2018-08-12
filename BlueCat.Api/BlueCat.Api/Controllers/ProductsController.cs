using BlueCat.Api.Common;
using BlueCat.Api.Service.Interface;
using BlueCat.Contract;
using BlueCat.Jwt;
using BlueCat.Jwt.Algorithms;
using BlueCat.Jwt.Serializers;
using BlueCat.Validation;
using FluentValidation.Results;
using JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BlueCat.Api.Controllers
{
    public class ProductsController : ApiController
    {
        public IStudentService StudentService { get; set; }

        public ProductsController(IStudentService studentService)
        {
            this.StudentService = studentService;
        }


        //[Route("v1/products/get")]
        //[HttpGet]
        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return products;
        //}

        /// <summary>
        /// Test code
        /// </summary>
        /// <param name="request">Request params</param>
        /// <returns></returns>
        [Route("v1/products")]
        [HttpPost]
        public IHttpActionResult GetAllProduct([FromBody] GetStudentsRequest request)
        {
            GetStudentsRequestValidator getStudentsRequestValidator = new GetStudentsRequestValidator();
            ValidationResult result = getStudentsRequestValidator.Validate(request);
            if (!result.IsValid)
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                });
                return BadRequest(ModelState);
            }

            GetStudentsResponse response = this.StudentService.GetStudents(request);

            return Ok(response);
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


        [Route("v1/Thumbnail")]
        [HttpGet]        
        public ApiResponseMessage<ApiResult<string>> GetProductById()
        {
            ApiResult<string> result = new ApiResult<string>();

            result.resultData = "1111";

            return Request.ToResponse(result);
        }
    }
}
