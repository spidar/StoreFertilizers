/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    livereload = require('gulp-livereload');
var webserver = require('gulp-webserver');

var paths = {
    webroot: "./wwwroot/"
};

paths.appJs = paths.webroot + "app/**/*.js";
paths.appHtml = paths.webroot + "app/**/*.html";
paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, paths.appJs,"!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task('reload', function () {
    // Change the filepath, when you want to live reload a different page in your project.
    livereload.reload(paths.appJs);
    livereload.reload(paths.appHtml);
});

// This task should be run, when you want to reload the webpage, when files change on disk.
// This task will only watch JavaScript file changes in the folder "/Client" and it's subfolders.
gulp.task('watch', function () {
    livereload.listen();
    gulp.watch(paths.appJs['reload']);
    gulp.watch(paths.appHtml, ['reload']);
});

gulp.task('webserver', function () {
    gulp.src('wwwroot')
      .pipe(webserver({
          //host: '0.0.0.0',
          port: 8080,
          livereload: { enable: true, port: 8081 },
          open: true,
          fallback: 'index.html',
          //https: true,
          //key: 'config/grunt-connect/server.key',
          //cert: 'config/grunt-connect/server.crt',
          /*
          proxies: [
              {
                  // redirect API requests to our DEV environment
                  // since we don't run the servers on our local machine
                  source: '/api',
                  target: proxyDomainURL + '/api' // proxyDomainURL is your API server location 
              },
              {
                  source: '/test', // the proxy path for the subdirectory
                  target: 'https://0.0.0.0:8080' // is our running website
              }
          ]
          */
      }));
});


gulp.task("min", ["min:js", "min:css"]);

gulp.task('default', ['webserver', 'watch']);
