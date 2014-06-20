
$('div').on('click', 'div#home-my-gallery', function () {
    $.ajax({
        url: 'Gallery/Index',
        type: 'post',
        data: {},
        success: function (result) {
            var content = $('#content');
            content.empty();
            content.html(result);
        },
        error: function () {
            alert('Sorry. Some error was occurred');
        }
    });
});


$('div').on('click', 'div#home-users', function () {
    $.ajax({
        url: 'Users/Index',
        type: 'post',
        data: {},
        success: function (result) {
            var content = $('#content');
            content.empty();
            content.html(result);
        }
    });
});


$('div').on('click', 'div#home-add-embroidery', function () {
    $.ajax({
        url: 'AddEmbroidery/Index',
        type: 'get',
        data: {},
        success: function (result) {
            var content = $('#content');
            content.empty();
            content.html(result);
        },
        error: function () {
            alert('Sorry. Some error was occurred');
        }
    });
});


$('div').on('click', 'div#home-management', function () {
    $.ajax({
        url: 'Management/Index',
        type: 'get',
        data: {},
        cache: false,
        success: function (result) {
            var content = $('#content');
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
    Gallery.Embroidery.OpenImage(id);
});

$("div").on('click', 'div.last-who-commented', function () {
    var userId = $(this).attr('data-userId');
    Gallery.Embroidery.OtherUser(userId);
});


$("div").on('click', 'div.image-like', function () {
    var id = $(this).attr('data-embroideryId');
    Gallery.Embroidery.OpenImage(id);
});

$("div").on('click', 'div.last-who-liked', function () {
    var userId = $(this).attr('data-userId');
    Gallery.Embroidery.OtherUser(userId);
});