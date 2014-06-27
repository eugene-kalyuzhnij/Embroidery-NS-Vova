
var Gallery = {
    centeredContentInFrames: function () {
        $('.gallery-border-content > img').each(function (index, element) {
            var image = $(this);

            var width = parseInt(image.attr('width'));
            var height = parseInt(image.attr('height'));

            if (width > height) {
                image.css('width', '100%');
                var frame_height = 150;//  <--------------  height of the image's frame here.

                var image_percent = height * 100 / frame_height; // image's height in percent
                var margin = (100 - image_percent) / 2;

                image.css('margin-top', margin.toString());
            }
            else {
            }


        });
    }
}


$('.gallery-border-content').click(function () {

    var id = $(this).attr('data-embroidery-id');
    Gallery.Embroidery.OpenImage(id);
});


Gallery.centeredContentInFrames();
