module.exports = {
    entry: "/Content/scripts/app.js",
    output: {
        path: __dirname + '/wwwroot/scripts/',
        filename: "app.js",
        library: {
            name: "app",
            type: "window"
        }
    },
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules)/,
                loader: "babel-loader",
                options: {
                    presets: [
                        "@babel/preset-env",
                        ["@babel/preset-react", { "runtime": "automatic" }]
                    ]
                }
            }
        ]
    }
}