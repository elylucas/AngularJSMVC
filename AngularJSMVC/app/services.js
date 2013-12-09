(function () {
    'use strict';

    var app = angular.module('tweetApp');

    app.factory('twitterService', function($http, $q) {

        var getTweets = function() {
            var deferred = $q.defer();
            $http.get('/api/twitter/')
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data, status, header, config) {
                    deferred.reject(status);
                });
            return deferred.promise;
        };

        var getProfile = function(username) {
            var deferred = $q.defer();
            $http.get('/api/twitter/profile/' + username)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data, status, header, config) {
                    deferred.reject(status);
                });
            return deferred.promise;
        };

        var getFavorites = function () {
            var deferred = $q.defer();
            $http.get('/api/twitter/favorite')
                .success(function(data) {
                    deferred.resolve(data);
                })
                .error(function(data, status, header, config) {
                    deferred.reject(status);
                });
            return deferred.promise;
        };

        var favoriteTweet = function(tweet) {
            var deferred = $q.defer();
            $http.post('/api/twitter/favorite', tweet)
                .success(function(data) {
                    deferred.resolve(data);
                })
                .error(function(data, status, header, config) {
                    deferred.reject(status);
                });
            return deferred.promise;
        };

        var unfavoriteTweet = function(tweet) {
            var deferred = $q.defer();
            $http.delete('/api/twitter/favorite/' + tweet.statusId)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data, status, header, config) {
                    deferred.reject(status);
                });
            return deferred.promise;
        };

        return {
            getTweets: getTweets,
            getProfile: getProfile,
            getFavorites: getFavorites,
            favoriteTweet: favoriteTweet,
            unfavoriteTweet: unfavoriteTweet
        };

    });
})();