// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('li').hover(function () {
        $(this).find('.genre-dropdown').addClass('active');
    }, function () {
        $(this).find('.genre-dropdown').removeClass('active');
    });

    $('.genre').click(function () {
        var selectedGenre = $(this).text();
        alert("Bạn đã chọn thể loại " + selectedGenre);
        // You can perform further actions here, like filtering movies by genre, etc.
    });
});