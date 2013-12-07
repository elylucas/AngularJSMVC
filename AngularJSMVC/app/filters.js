(function () {
    'use strict';

    var app = angular.module('tweetApp');
    
    app.filter('twitterLinks', function () {
        return function (input) {
            return input.parseTwitterHandles().parseTwitterHashTags();
        };
    });

    String.prototype.parseTwitterHandles = function() {
        return this.replace(/[@]+[A-Za-z0-9_-]+/g, function(value) {
            var username = value.replace('@', '');
            var link = '<a href=' + '#/profile/' + username + '>' + value + '</a>';
            return link;
        });
    };
    
    String.prototype.parseTwitterHashTags = function () {
        return this.replace(/[#]+[A-Za-z0-9_-]+/g, function (value) {
            var hashtag = value.replace('#', '');
            var link = '<a href=' + 'http://www.twitter.com/search?q=%23' + hashtag + '>' + value + '</a>';
            return link;
        });
    };

})();