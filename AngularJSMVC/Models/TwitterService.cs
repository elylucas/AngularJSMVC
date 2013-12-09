using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Caching;
using LinqToTwitter;
using Newtonsoft.Json;

namespace AngularJSMVC.Models
{
    public class TwitterService : ITwitterService
    {
        private TwitterContext _twitterCtx;
        private ApplicationDbContext _dbContext;

        public TwitterService()
        {
            var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var claims = claimsIdentity.Claims;
            var accessTokenClaim = claims.FirstOrDefault(x => x.Type == "urn:tokens:twitter:accesstoken");
            var accessTokenSecretClaim = claims.FirstOrDefault(x => x.Type == "urn:tokens:twitter:accesstokensecret");
    
            var auth = new SingleUserAuthorizer
            {
                Credentials = new SingleUserInMemoryCredentials
                {
                    ConsumerKey =
                        ConfigurationManager.AppSettings["twitterConsumerKey"],
                    ConsumerSecret =
                        ConfigurationManager.AppSettings["twitterConsumerSecret"],
                    TwitterAccessToken = accessTokenClaim.Value,
                    TwitterAccessTokenSecret = accessTokenSecretClaim.Value
                }
            };

            _twitterCtx = new TwitterContext(auth);
            _dbContext = new ApplicationDbContext();
        }

        public List<Tweet> GetUserStatuses()
        {
            var statuses = HttpContext.Current.Cache["timeline_tweets"] as List<Tweet>;
            if (statuses == null)
            {
                var srch =
                    (from tweets in _twitterCtx.Status
                     where tweets.Type == StatusType.Home
                     select tweets);

                statuses = srch.Select(x => new Tweet
                {
                    statusId = x.StatusID,
                    name = x.User.Name,
                    screenName = x.User.Identifier.ScreenName,
                    createdAt = x.CreatedAt.ToString(),
                    text = x.Text,
                    imageUrl = x.User.ProfileImageUrl,

                }).ToList();

                HttpContext.Current.Cache.Add("timeline_tweets", statuses, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }

            var favoriteTweetIds = _dbContext.FavoriteTweets.Select(x => x.statusId).ToList();
            statuses.ForEach(tweet =>
            {
                tweet.isFavorite = favoriteTweetIds.Any(x => x == tweet.statusId);
            });

            return statuses;
        }

        public TwitterUser GetUser(string username)
        {
            var twitterUser = (from user in _twitterCtx.User
                               where user.Type == UserType.Show &&
                                     user.ScreenName == username
                               select new TwitterUser()
                               {
                                   description = user.Description,
                                   followers = user.FollowersCount,
                                   following = user.FriendsCount,
                                   imageUrl = user.ProfileImageUrl,
                                   screenName = user.ScreenName,
                                   name = user.Name,
                                   tweets = user.StatusesCount
                               }).SingleOrDefault();
            return twitterUser;
        }

        public void FavoriteTweet(Tweet tweet)
        {
            _dbContext.FavoriteTweets.Add(tweet);
            _dbContext.SaveChanges();
        }

        public void UnFavoriteTweet(string statusId)
        {
            var foundTweet = _dbContext.FavoriteTweets.SingleOrDefault(x => x.statusId == statusId);
            if (foundTweet != null)
            {
                _dbContext.FavoriteTweets.Remove(foundTweet);
                _dbContext.SaveChanges();
            }
        }

        public List<Tweet> GetFavorites()
        {
            var tweets = _dbContext.FavoriteTweets.ToList();
            return tweets;
        }

    }

    public class OfflineTwitterService : ITwitterService
    {
        private ApplicationDbContext _dbContext;
        public OfflineTwitterService()
        {
            _dbContext = new ApplicationDbContext();
        }
        public List<Tweet> GetUserStatuses()
        {
            var statuses = HttpContext.Current.Cache["timeline_tweets"] as List<Tweet>;
            if (statuses == null)
            {
                var jsonString = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "\\statuses.json");
                statuses = JsonConvert.DeserializeObject<List<Tweet>>(jsonString);
                HttpContext.Current.Cache.Add("timeline_tweets", statuses, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }

            var favoriteTweetIds = _dbContext.FavoriteTweets.Select(x => x.statusId).ToList();
            statuses.ForEach(tweet =>
            {
                tweet.isFavorite = favoriteTweetIds.Any(x => x == tweet.statusId);
            });

            return statuses;
        }

        public TwitterUser GetUser(string username)
        {
            var jsonString = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "\\twitteruser.json");
            var twitterUser = JsonConvert.DeserializeObject<TwitterUser>(jsonString);
            return twitterUser;
        }

        public void FavoriteTweet(Tweet tweet)
        {
            _dbContext.FavoriteTweets.Add(tweet);
            _dbContext.SaveChanges();
        }

        public void UnFavoriteTweet(string statusId)
        {
            var foundTweet = _dbContext.FavoriteTweets.SingleOrDefault(x => x.statusId == statusId);
            if (foundTweet != null)
            {
                _dbContext.FavoriteTweets.Remove(foundTweet);
                _dbContext.SaveChanges();
            }
        }

        public List<Tweet> GetFavorites()
        {
            var tweets = _dbContext.FavoriteTweets.ToList();
            return tweets;
        }
    }

    public interface ITwitterService
    {
        List<Tweet> GetUserStatuses();
        TwitterUser GetUser(string username);
        void FavoriteTweet(Tweet tweet);
        void UnFavoriteTweet(string statusId);
        List<Tweet> GetFavorites();
    }
}