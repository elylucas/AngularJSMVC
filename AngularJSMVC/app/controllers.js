(function () {
    'use strict';

    var app = angular.module('tweetApp');

    app.controller('homeController', function($scope, twitterService) {
        twitterService.getTweets().then(function(data) {
            $scope.tweets = data;
        });

        $scope.favoriteTweet = function(tweet) {
            if (tweet.isFavorite) {
                twitterService.unfavoriteTweet(tweet);
            } else {
                twitterService.favoriteTweet(tweet);
            }
            tweet.isFavorite = !tweet.isFavorite;
        };
        
    });
    
    app.controller('profileController', function ($scope, twitterService, $routeParams) {
        twitterService.getProfile($routeParams.username).then(function(data) {
            $scope.profile = data;
        });
    });
    
    app.controller('favoritesController', function ($scope, twitterService) {
        twitterService.getFavorites().then(function(data) {
            $scope.tweets = data;
        });
        
        $scope.favoriteTweet = function (tweet) {
            if (tweet.isFavorite) {
                twitterService.unfavoriteTweet(tweet);
            } else {
                twitterService.favoriteTweet(tweet);
            }
            tweet.isFavorite = !tweet.isFavorite;
        };
    });
    


    
})();