



/*Palette creator******************************************/

var colors = ['gray', 'green', 'white', 'black', 'blue', 'yellow', 'violet', 'aqua', 'azure', 'brown'];

for (var i = 0; i < 10; i++) {
    var element = "#" + i.toString();
    var elem = $('#' + i.toString());
    elem.css('background-color', colors[i]);
}

/********************************************************/



/*Open image in Gallery************************************************/
$('.gallery-border img').click(function () {

    var clickedSrc = $(this).attr('src');
    $('#open-image img').remove();
    $('#open-image').prepend('<img class="opened" src="" />');
    $('#open-image img').prop('src', clickedSrc);
    $('#open-image-border').fadeIn('slow');

});


$('#open-image').click(function () {
    $('#open-image-border').fadeOut('slow');
});
/**********************************************************************/



var image = null;

/*Preview after upload image*************************************/

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#preview').css('display', 'block');
            $('#preview').prepend('<img id="blah" src="" />');
            image = e.target.result;
            $('#preview img').attr('src', image);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#browse").change(function () {
    readURL(this);
});

/***************************************************************/

function is_int(value) {
    if ((parseFloat(value) == parseInt(value)) && !isNaN(value)) {
        return true;
    } else {
        return false;
    }
}



$('#resolutions').click(function () {

    var cellsCount = $('#cells-count').val();

    if(!is_int(cellsCount))
    {
        alert('Count of cells has to be number');
        return;
    }

    $.ajax({
        type: "POST",
        url: 'Profile/GetResolutions',
        data: { cells: cellsCount, img: image }

    }).done(function () {
        alert("done");
    });

});