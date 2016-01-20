var path = require("path");
var webpack = require("webpack");
var BowerWebpackPlugin = require('bower-webpack-plugin');
var ExtractTextPlugin = require("extract-text-webpack-plugin");

module.exports = {
    cache: true,
    entry: {
        common: ["site/site.js", "bootstrap/bootstrap.scss", "open-iconic/font/css/open-iconic-bootstrap.css", "./clientside/site/site.scss"],
        userList: ["./clientside/pages/admin/user-list.js"],
    },
    output: {
        publicPath: "/build/",
        path: path.join(__dirname, "wwwroot/build/"),
        filename: "[name].js"
    },
    module: {
        preLoaders: [
            { test: /\.js$/, include: /clientside/, loader: "eslint" }
        ],
        loaders: [
            // make entry modules available as global variables- to reference them from HTML pages
            { test: /clientside\\.*site\.js$/, loader: 'expose?site' },
            { test: /clientside\\.*user-list\.js$/, loader: 'expose?userList' },

            // make jQuery module available as global variables ($ and jQuery)- to reference from HTML pages and other modules
            { test: /jquery\.js$/, loader: 'expose?$!expose?jQuery' },

            // Disable AMD detection for PNotify as it doesn't work
            { test: /pnotify/, loader: 'imports?define=>false' },

			// support for "require('./file.css')"
            { test: /\.css$/, loader: ExtractTextPlugin.extract("css?sourceMap") },

            // support for "require('./file.less')"
            { test: /\.less$/, loader: ExtractTextPlugin.extract('css?sourceMap!less?sourceMap') },

            // support for "require('./file.scss')"
            { test: /\.scss/, loader: ExtractTextPlugin.extract('css?sourceMap!resolve-url!sass?sourceMap') },

            // support for "require('./template.hbs')"
            { test: /\.hbs$/, loader: "handlebars" },

            // copy images and fonts from bower_packages folder into output directory
            { test: /bower_packages\\.*\.(woff|svg|ttf|eot|otf)([\?]?.*)$/, loader: "file?name=assets/packages/[path][name].[ext]&context=./bower_packages/" },
            { test: /bower_packages\\.*\.(png|jpg)$/, loader: "file?name=assets/packages/[path][name].[ext]&context=./bower_packages/" },

            // copy images and fonts from ClientSide folder into output directory
            { test: /clientside\\.*\.(woff|svg|ttf|eot|otf)([\?]?.*)$/, loader: "file?name=assets/[path][name].[ext]&context=./clientside/" },
            { test: /clientside\\.*\.(png|jpg|ico)$/, loader: "file?name=assets/[path][name].[ext]&context=./clientside/" }
        ]
    },
    resolve: {
        modulesDirectories: ["clientside", "clientside/datatables", "bower_packages", "node_modules"],
        alias: {
            //"alias": "path/file.js",
        }
    },
    plugins: [
        new BowerWebpackPlugin(),
        new webpack.optimize.CommonsChunkPlugin("common", "common.js"),
        new ExtractTextPlugin("[name].css")
    ],
    eslint: {
        configFile: path.join(__dirname, ".eslintrc.json")
    }
};