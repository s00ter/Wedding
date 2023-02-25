// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const showRombus = () => {
    console.log($('.preloader-wrapper'))
    $('.preloader-wrapper').fadeOut();
    $('.preloader-wrapper').removeAttr('hidden')
}

const hideRombus = () => {
    $('.preloader-wrapper').fadeIn();
    $('.preloader-wrapper').attr('hidden', 'hidden')
}