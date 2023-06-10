var gulp = require('gulp'),
    concat = require('gulp-concat'),
    autoprefixer = require('gulp-autoprefixer'),
    rename = require("gulp-rename"),
    sass = require('gulp-sass')(require('sass')),
    cleanCSS = require('gulp-clean-css'),
    browserSync = require('browser-sync').create();

function compileScss(done) {
    return gulp.src('./wwwroot/scss/style.scss')
        .pipe(sass({
            errorLogToConsole: true,
            outputStyle: 'expanded'
        }))
        .on('error', console.error.bind(console))
        .pipe(autoprefixer({
            cascade: false
        }))
        .pipe(cleanCSS())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('./wwwroot/css/'))
        .pipe(browserSync.stream())
        .on('end', done);
};

function compileScssBootstrap(done) {
    return gulp.src(['./node_modules/bootstrap/dist/css/bootstrap.css', './wwwroot/css/style.min.css'])
        .pipe(concat('style.min.css'))
        .pipe(gulp.dest('./wwwroot/css/'))
        .pipe(browserSync.stream())
        .on('end', done);
};

function watchFiles() {
    gulp.watch('./wwwroot/scss/**/*.scss', compileScss);
    gulp.watch(['./node_modules/bootstrap/dist/css/bootstrap.css', './wwwroot/scss/style.scss'], compileScssBootstrap);
    gulp.watch('./**/*.cshtml').on('change', browserSync.reload);
}

gulp.task('default', gulp.series(compileScss, compileScssBootstrap, watchFiles));