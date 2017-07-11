/// <binding />

var gulp = require('gulp');
var rename = require('gulp-rename');
var cssnano = require('gulp-cssnano');
var sass = require('gulp-sass');

//SASS compiler task
gulp.task('sass', function () {
    return gulp.src('sass/styles.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(rename("style.css"))
        .pipe(cssnano())
        .pipe(gulp.dest('client-src/content/css'));
});

//SASS watch
gulp.task('sass:watch',
  function() {
    gulp.watch('sass/*.scss', ['sass']);
  });