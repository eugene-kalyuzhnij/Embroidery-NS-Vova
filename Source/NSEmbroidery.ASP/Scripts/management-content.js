


$('#user-content-embroideries div.user-content-delete').on('click', function (event) {
    var embroideryId = $(this).attr('data-embroidery-id');

    $.ajax({
        url: 'Management/DeleteEmbroidery',
        data: { embroideryId: embroideryId},
        type: 'POST',
        success: function (result) {
            if (result.Result) {
                $('[data-embroidery-id=' + embroideryId + ']').parent().remove();
            }
            else alert('Operation faild');
        },
        error: function (a, b, c) {
            alert('ERROR WAS OCUURED');
        }

    });

});

$('#user-content-comments div.user-content-delete').on('click', function (event) {
    var commentId = $(this).attr('data-comment-id');
    $.ajax({
        url: 'Management/DeleteComment',
        data: { commentId: commentId },
        type: 'POST',
        success: function (result) {
            if (result.Result) {
                $('[data-comment-id=' + commentId + ']').parent().remove();
            }
            else alert('Operation faild');
        },
        error: function (a, b, c) {
            alert('ERROR WAS OCUURED');
        }

    });

});



$('.user-content-item img').each(function (event) {
    $(this).on('click', function () {
        var imageId = $(this).attr('data-embroidery-id');
        Gallery.Embroidery.OpenImage(imageId);
    });
});

