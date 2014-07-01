
var Gallery = {
    centeredContentInFrames: function () {
        $('.gallery-border-content > img').each(function (index, element) {
            var image = $(this);

            var width = parseInt(image.attr('data-width'));
            var height = parseInt(image.attr('data-height'));

            if (width >= height) {
                image.css('width', '100%');
                var frame_height = 145;//  <--------------  height of the image's frame here.

                var image_percent = height * 100.0 / frame_height; // image's height in percent
                var margin = (100.0 - image_percent) / 2.0;
                image.css('margin-top', margin.toString() + '%');

                //alert('top = ' + margin.toString() + '; top+bottom = ' + (100.0 - image_percent).toString());
            }
            else {
                //do nothing. Default csses values will center image
            }


        });
    }
}


$('.gallery-border-content').click(function () {

    var id = $(this).attr('data-embroidery-id');
    Embroidery.OpenImage(id);
});


Gallery.centeredContentInFrames();
