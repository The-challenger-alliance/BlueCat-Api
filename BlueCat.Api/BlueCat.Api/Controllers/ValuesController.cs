using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlueCat.Api.Controllers
{
    public class ValuesController : ApiController
    {
        public void Post()
        {
        }

        public HttpResponseMessage Get()
        {
            //return new TextResult("hello", Request);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent("hello", Encoding.Unicode);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };
            return response;
        } 
    }

    //public interface IHttpActionResult
    //{
    //    Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken);
    //}

    //public class TextResult : IHttpActionResult
    //{
    //    string _value;
    //    HttpRequestMessage _request;

    //    public TextResult(string value, HttpRequestMessage request)
    //    {
    //        _value = value;
    //        _request = request;
    //    }
    //    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    //    {
    //        var response = new HttpResponseMessage()
    //        {
    //            Content = new StringContent(_value),
    //            RequestMessage = _request
    //        };
    //        return Task.FromResult(response);
    //    }
    //}
}
