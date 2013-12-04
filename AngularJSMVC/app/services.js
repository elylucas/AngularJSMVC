(function () {
    'use strict';

    var app = angular.module('tweetApp');

    app.factory('twitterService', function($http, $q) {

        var getTweets = function() {
            var deferred = $q.defer();

            $http.get('/api/twitter')
                .success(function(data) {
                    deferred.resolve(data);
                })
                .error(function() {
                    deferred.reject();
                });
            return deferred.promise;
        };

        return {
            getTweets: getTweets
        };

    });
})();