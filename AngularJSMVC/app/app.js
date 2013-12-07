(function () {
    'use strict';

    var app = angular.module('tweetApp', ['ngRoute', 'ngSanitize']);

    app.config(function($routeProvider) {
        $routeProvider.when('/', {
            controller: 'homeController',
            templateUrl: 'app/views/home.html'
        });

        $routeProvider.when('/profile/:username', {
            controller: 'profileController',
            templateUrl: 'app/views/profile.html'
        });

    });
    


})();