﻿(function () {
    'use strict';

    var app = angular.module('tweetApp');

    app.controller('homeController', function($scope, twitterService) {
        twitterService.getTweets().then(function(data) {
            $scope.tweets = data;
        });
    });
    
    app.controller('profileController', function ($scope, twitterService, $routeParams) {
        twitterService.getProfile($routeParams.username).then(function(data) {
            $scope.profile = data;
        });
    });
    
})();