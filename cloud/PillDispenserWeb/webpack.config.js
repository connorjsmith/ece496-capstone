/// <binding />
"use strict";

var webpack = require("webpack");

module.exports = {
    entry: "./Javascript/index.jsx",
    output: {
        filename: "./wwwroot/dist/bundle.js"
    },
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9000
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                loader: "babel-loader",
                exclude: /(node_modules|bower_components)/,
                query: {
                    presets: ['env', 'react']
                }
            }
        ]
    }
};