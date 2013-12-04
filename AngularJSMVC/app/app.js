(function () {
    'use strict';

    var app = angular.module('tweetApp', ['ngRoute']);

    app.config(function($routeProvider) {
        $routeProvider.when("/", {
            controller: "homeController",
            templateUrl: "app/views/home.html"
        });
    });

})();