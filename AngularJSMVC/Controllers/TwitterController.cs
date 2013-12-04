using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSMVC.Models;

namespace AngularJSMVC.Controllers
{
    [RoutePrefix("api/twitter")]
    public class TwitterController : ApiController
    {
        private ITwitterService _twitterService;

        public TwitterController()
        {
            _twitterService = new TwitterService();
        }

        [Route("")]
        public HttpResponseMessage GetTweets()
        {
            var tweets = _twitterService.GetUserStatuses();
            return Request.CreateResponse(HttpStatusCode.OK, tweets);
        }
    }
}
