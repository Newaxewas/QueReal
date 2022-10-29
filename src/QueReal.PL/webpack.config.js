module.exports = {
    entry: "/Content/scripts/app.jsx",
    output: {
        path: __dirname + '/wwwroot/scripts/',
        filename: "app.js",
    },
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules)/,
                loader: "babel-loader",
                options: {
                    presets: ["@babel/preset-env", "@babel/preset-react"]
                }
            }
        ]
    }
}