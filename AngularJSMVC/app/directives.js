(function () {
    'use strict';
    
    var app = angular.module('tweetApp');

    app.directive('tweet', function() {
        return {
            restrict: 'AE',
            templateUrl: 'app/views/tweet.html'
        };
    });
})();