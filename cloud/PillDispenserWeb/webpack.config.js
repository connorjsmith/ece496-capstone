/// <binding />
"use strict";

var webpack = require("webpack");
var path = require("path");

module.exports = {
    entry: {
        home: "./Frontend/Javascript/index.jsx",
        antd: "./Frontend/Javascript/antd.jsx",
        side_menu: "./Frontend/Javascript/SideMenu.jsx"
    },
    output: {
        path: path.join(__dirname, "wwwroot", "dist"),
        filename: "[name]-dist.js"
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
                    presets: ['env', 'react'],
                    plugins: [["import", { "libraryName": "antd", "style": "css" }]]
                }
            },
            {
                test: /\.css$/,
                loader: "style-loader!css-loader"
            }
        ],
    }
};