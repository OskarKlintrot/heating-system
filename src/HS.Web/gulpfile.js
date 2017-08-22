/// <binding BeforeBuild='default' ProjectOpened='watch' />
'use strict'

var gulp = require('gulp')
var browserify = require('browserify')
var source = require('vinyl-source-stream')
var tsify = require('tsify')
var uglify = require('gulp-uglify')
var sourcemaps = require('gulp-sourcemaps')
var buffer = require('vinyl-buffer')
var del = require('del')
var sass = require('gulp-sass')

gulp.task('clean-typescript', function () {
    return del([
        'Assets/scripts/**/*'
    ])
})

gulp.task('clean-sass', function () {
    return del([
        'Assets/css/**/*'
    ])
})

gulp.task('sass', ['clean-sass'], function () {
    return gulp.src([
            'scss/main.scss',
            'scss/materialize.light.scss',
            'scss/materialize.complete.scss',
            'scss/offline.scss'
        ])
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(sourcemaps.write('./'))
        .pipe(gulp.dest('Assets/css'))
})

gulp.task('typescript', ['clean-typescript'], function () {
    return browserify({
        basedir: '.',
        debug: true,
        entries: ['scripts/main.ts', 'scripts/offline.min.js'],
        cache: {},
        packageCache: {}
    })
        .plugin(tsify)
        .bundle()
        .pipe(source('bundle.min.js'))
        .pipe(buffer())
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(uglify())
        .pipe(sourcemaps.write('./'))
        .pipe(gulp.dest('Assets/scripts'))
})

gulp.task('default', ['typescript', 'sass'])

gulp.task('watch', function () {
    gulp.watch('scripts/**/*.ts', ['typescript'])
    gulp.watch('scripts/**/*.js', ['typescript'])
    gulp.watch('scss/**/*.scss', ['sass'])
})
