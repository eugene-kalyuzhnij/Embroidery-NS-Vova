﻿
$('div').on('click', 'div#home-my-gallery', function () {
    Home.loadPage(0);
    // window.location.hash = 'Gallery';
    Embroidery.DisposeOpenImage();
    window.history.pushState({ page: 0 }, $(this).text(), window.location);

});


$('div').on('click', 'div#home-users', function () {
    Home.loadPage(1);
    //  window.location.hash = "Users";
    Embroidery.DisposeOpenImage();
    window.history.pushState({ page: 1 }, $(this).text(), window.location);
});


$('div').on('click', 'div#home-add-embroidery', function () {
    Home.loadPage(2);
    // window.location.hash = 'AddEmbroidery';
    Embroidery.DisposeOpenImage();
    window.history.pushState({ page: 2 }, $(this).text(), window.location);
});


$('div').on('click', 'div#home-management', function () {

    var content = $('#content');
    var switchLoader = $('#switch-loader');

    $.ajax({
        url: 'Management/Index',
        type: 'get',
        data: {},
        cache: false,
        beforeSend: function () {
            Embroidery.CancelImageLoading();
        },
        success: function (result) {

            content.css('opacity', '1');
            switchLoader.empty();
            content.empty();
            content.html(result);
        },
        error: function () {

            alert('Sorry. Some error was occurred');
        }
    });
});



$("div").on('click', 'div.image-commented', function () {
    var id = $(this).attr('data-embroideryId');

    Embroidery.OpenImage(id);
   
});

$("div").on('click', 'div.last-who-commented', function () {
    var userId = $(this).attr('data-userId');
    Embroidery.OtherUser(userId);
});

$("div").on('click', 'div.image-like', function () {
    var id = $(this).attr('data-embroideryId');
    Embroidery.OpenImage(id);
});

$("div").on('click', 'div.last-who-liked', function () {
    var userId = $(this).attr('data-userId');
    Embroidery.OtherUser(userId);
});


window.addEventListener('popstate', function (event) {
    if (event.state != null && event.state.hasOwnProperty('page')) {
        if (event.state.page != null && event.state.page != undefined) {
            Home.loadPage(event.state.page);
        }
    }
    else {
        this.location.reload();
    }
    
});




$(document).bind("ajaxSend", function () {
    Home.ShowLoading();
}).bind("ajaxComplete", function () {
    Home.HideLoading();
});

