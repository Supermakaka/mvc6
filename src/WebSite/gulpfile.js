/// <binding />
var gulp = require('gulp');
var gutil = require('gulp-util');
var del = require('del');
var webpack = require('webpack');
var WebpackDevServer = require("webpack-dev-server");

var webpackConfig = require('./webpack.config.js');
var appConfig = require('./appsettings.json');

gulp.task('clean', function () {
    return del(webpackConfig.output.path);
});

gulp.task('webpack:build-prod', ['clean'], function (callback) {

    var config = Object.create(webpackConfig);

    config.plugins = config.plugins.concat(
        new webpack.optimize.DedupePlugin(), //may crash, just disable then
        new webpack.optimize.UglifyJsPlugin()
    );

    // run webpack
    webpack(config, function (err, stats) {
        if (err) throw new gutil.PluginError('webpack', err);
        gutil.log('[webpack]', stats.toString({
            colors: true
        }));
        callback();
    });
});

var devServerConfig = Object.create(webpackConfig);
devServerConfig.devtool = 'inline-source-map';
devServerConfig.debug = true;
// create a single instance of the compiler to allow caching ("cache: true" in config)
var devServerCompiler = webpack(devServerConfig);

gulp.task("webpack:start-dev-server", function (callback) {
    // start a webpack-dev-server
    new WebpackDevServer(devServerCompiler, {
        publicPath: devServerConfig.output.publicPath,
        stats: {
            colors: true
        },
        headers: { "Access-Control-Allow-Origin": "*" }
    }).listen(appConfig.DevelopmentSettings.WebpackDevServerPort, "localhost", function (err) {
        if (err) throw new gutil.PluginError("webpack-dev-server", err);
        gutil.log("[webpack-dev-server]", "http://localhost:" + appConfig.DevelopmentSettings.WebpackDevServerPort  + "/webpack-dev-server/index.html");
    });
});