




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

