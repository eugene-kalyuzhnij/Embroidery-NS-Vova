
$('.gallery-border img').click(function () {

    var id = $(this).attr('data-embroidery-id');
    Gallery.Embroidery.OpenImage(id);
});
