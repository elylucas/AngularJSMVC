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

        [Route("profile/{username}")]
        public HttpResponseMessage GetProfile(string username)
        {
            var profile = _twitterService.GetUser(username);
            return Request.CreateResponse(HttpStatusCode.OK, profile);
        }

        [Route("favorite")]
        public HttpResponseMessage GetFavorites()
        {
            var tweets = _twitterService.GetFavorites();
            return Request.CreateResponse(HttpStatusCode.OK, tweets);
        }

        [Route("favorite")]
        [HttpPost]
        public HttpResponseMessage FavoriteTweet(Tweet tweet)
        {
            _twitterService.FavoriteTweet(tweet);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("favorite/{statusId}")]
        [HttpDelete]
        public HttpResponseMessage UnFavoriteTweet(string statusId)
        {
            _twitterService.UnFavoriteTweet(statusId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
