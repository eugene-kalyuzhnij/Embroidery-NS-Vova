



/*Careting and choosing palette***************************/
{
    var colors = ['gray', 'green', 'rgb(0, 100, 0)', 'black', 'blue', 'yellow', 'violet', 'aqua', 'azure', 'brown'];

    for (var i = 0; i < 10; i++) {
        var element = "#" + i.toString();
        var elem = $('#' + i.toString());
        elem.css('background-color', colors[i]);
    }


    $('.palette-box').click( function (event) {
        var choosedPalette = $('#choosed-palette-border');
        var newDiv = $('<div class="choosed"></div>');
        choosedPalette.prepend(newDiv);
        
        var color = $(this).css('background-color');

        newDiv.css('background-color', color);
            
    });

}
/*********************************************************/



/*Open image in Gallery************************************************/
{
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
}
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

/****************************************************************/

/*Chossing Resolution****************************************************/

function is_int(value) {
    if ((parseFloat(value) == parseInt(value)) && !isNaN(value)) {
        return true;
    } else {
        return false;
    }
}


$('#cells-count').change(function () {
    $('#resolutions').click(function (event) {
        event.preventDefault();

        var cellsCount = $('#cells-count').val();

        if (!is_int(cellsCount)) {
            alert('Count of cells has to be number');
            return;
        }
        var response = null;

        $.ajax({
            type: "POST",
            url: 'Profile/GetResolutions',
            data: { cells: cellsCount, img: image },
            success: function (result) {
                var items = [];
                $.each(result, function () {
                    items.push("<option value=" + this.Value + ">" + this.Text + "</option>");
                });
                $("#resolutions").html(items.join(' '));
            }
        });

        $('#resolutions').unbind();
    });
});

/************************************************************************/


$.cssHooks.backgroundColor = {
    get: function (elem) {
        if (elem.currentStyle)
            var bg = elem.currentStyle["background-color"];
        else if (window.getComputedStyle)
            var bg = document.defaultView.getComputedStyle(elem,
                null).getPropertyValue("background-color");
        if (bg.search("rgb") == -1)
            return bg;
        else {
            bg = bg.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
            function hex(x) {
                return ("0" + parseInt(x).toString(16)).slice(-2);
            }
            return "#" + hex(bg[1]) + hex(bg[2]) + hex(bg[3]);
        }
    }
}


function getColors() {
    var colors = [];

    $('.choosed').each(function () {
        
        var current = $(this);
        var color = current.css('background-color');
        colors.push(color);
    });
    return colors
}



    function CreateEmbroidery() {

        cellsCount = $('#cells-count').val();
        coefficient = $('#resolutions').val();
        colors = getColors();

        $.ajax({
            contentType: "application/json",
            type: "POST",
            url:"Profile/CreateEmbroidery",
            data: JSON.stringify({ img: image, coefficient: coefficient, cellsCount:cellsCount, colors: colors }),
            success: function (result) {
                $('#preview').prepend('<img src="">');
                $('#preview img').attr('src', "data:image/png;base64," + result.imageString);
            }
        });
        
    }


    $('#embroidery-button').click(function () {
        CreateEmbroidery();
    });


