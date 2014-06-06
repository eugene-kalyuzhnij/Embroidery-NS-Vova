/*Open image in Gallery************************************************/


$('.gallery-border img').click(function () {

    //var clickedSrc = $(this).attr('src');
    var id = $(this).attr('id');
    $.ajax({
        url: 'Gallery/GetEmbroidery',
        data: { embroideryId: id },
        type: 'POST',
        success: function (result) {
            $('#open-image img').remove();
            $('#open-image').prepend('<img class="opened" src="" />');
            $('#open-image img').prop('src', result.imageString);
            $('#open-image-border').fadeIn('slow');
        },
        error: function (a, b, c) {
            alert('Could not upload embroidery from database');
        }
  
    });

    /*Show all comments********/
    
    UpdateComments(id);

    /**************************/

    /*Likes*******************/

    UpdateLikesCount(id);
    AddRemoveClick(id);


    $('#like-border').click(function (event) {

        var x = event.clientX;
        var y = event.clientY;

        var likes_users = $('#likes-users');
        likes_users.empty();
        likes_users.css('display', 'inherit');
        likes_users.css('left', x.toString() + 'px');
        likes_users.css('top', y.toString() + 'px');

        $.ajax({
            url: 'Gallery/GetLikesUsers',
            data: { embroideryId: id },
            dataType: 'json',
            type: 'post',
            success: function (result) {

                for (var i in result) {
                    var userList = likes_users.prepend('<div class="' + result[i].UserId + '"> '
                                           + result[i].UserName + '</div>');

                }

                $('#likes-users div').click(function () {
                    var userId = $(this).attr('class');
                    OtherUser(userId);
                });

                $('#open-image-border').click(function () {
                    DeleteLikesUserView();
                });
            }
        });

    });

    /*************************/

    /*Add Comment*/
    
    $('#send-comment').click(function () {
        var comment = $('#input-comment');
        if (comment.val() != "") {
            $.ajax({
                type: "post",
                url: "Gallery/AddComment",
                data: { EmbroideryId: id, comment: comment.val() },
                success: function () {
                    UpdateComments(id);
                    comment.val("");
                }
            });
        }
    });

    /***********/

});

$('#image-border').click(function () {
    $('#open-image-border').fadeOut('slow');
    $('#send-comment').unbind('click');
    $('#like-border').unbind('click');
});





function AddRemoveClick(embroideryId) {
    var add_remove_Like = $('#add-like-border');

    $.ajax({
        url: 'Gallery/CanAddLike',
        type: 'post',
        data: { embroideryId: embroideryId },
        success: function (canAdd) {

            if (canAdd == 'True') {
                add_remove_Like.unbind('click');
                add_remove_Like.empty();
                add_remove_Like.prepend('Add like');
                var handler = function () {
                    $.ajax({
                        url: 'Gallery/AddLike',
                        data: { embroideryId: embroideryId },
                        type: 'post',
                        success: function () {
                            UpdateLikesCount(embroideryId);
                            AddRemoveClick(embroideryId);
                        }
                    });
                }

                    add_remove_Like.bind('click', handler);
            }
            else {
                add_remove_Like.unbind('click');
                add_remove_Like.empty();
                add_remove_Like.prepend('Remove like');

                var handler = function () {
                    $.ajax({
                        url: 'Gallery/RemoveLike',
                        data: { embroideryId: embroideryId },
                        type: 'post',
                        success: function () {
                            UpdateLikesCount(embroideryId);
                            AddRemoveClick(embroideryId);
                        }
                    });
                }
                add_remove_Like.bind('click', handler);
            }
        }
    });
}

function UpdateLikesCount(embroideryId) {

    $.ajax({
        url: 'Gallery/GetLikesCount',
        type: 'post',
        dataType: 'json',
        data: { embroideryId: embroideryId },
        success: function (result) {
            var likes = $('#like-border');
            likes.empty();
            likes.prepend(result.toString());
        }
    })

}

function UpdateComments(embroideryId) {
    $.ajax({
        url: 'Gallery/GetComments',
        dataType: 'json',
        type: 'post',
        data: { EmbroideryId: embroideryId },
        success: function (result) {
            var all_comments = $('.comments');

            $('.comments div').remove();

            for (var i in result) {
                
                var comment = result[i].Comment;
                var str = comment.replace(/\n/g, '<br>');

                all_comments.prepend('<div class="comment-border">' + 
                                     '<div class="who-commented" id="' + result[i].UserId.toString() + '">' + result[i].UserName + '</div>' +
                                     '<div class="comment">' + str + '</div></div>');

                $('.who-commented').click(function () {
                    var id = $(this).attr('id');
                    OtherUser(id);
                });
            }

        },
        error: function (request, status, error) {
            alert("Error was occured");
        }
    });


}

function AddCommentClick(embroideryId) {
    var comment = $('#input-comment');
    if (comment.val() != "") {
        $.ajax({
            type: "post",
            url: "Gallery/AddComment",
            data: { EmbroideryId: embroideryId, comment: comment.val() },
            success: function () {
                UpdateComments(id);
                comment.val("");
            }
        });
    }
}

function DeleteLikesUserView() {
    $('#likes-users').css('display', 'none');
    $('#open-image-border').unbind('click');
}




function OtherUser(userId) {
    debugger
    $.ajax({
        url: 'Users/OtherUser',
        data: { userId: userId },
        type: 'post',
        success: function (result) {

            var content = $('#content');
            content.empty();

            content.html(result);
        }
    });
}


/**********************************************************************/



var image = null;

/*Preview after uploading image*************************************/

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#preview').css('display', 'block');
            $('#preview img').remove();
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

/*****************************************************************/

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
            url: 'AddEmbroidery/GetResolutions',
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

/*Add embroidery request***********************************************************/

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



function CreateEmbroidery() {
    
    var cellsCount = $('#cells-count').val();
    var coefficient = $('#resolutions').val();
    var colors = getColors();
    var symbols = getSymbols();
    var symColor = getSymbolColor();
    var grid = $('#grid').prop("checked");



    
    if (image == null) {
        alert("Open image first");
        return;
    }
    
    if (!is_int(cellsCount)) {
        alert("Wrong count of cells parametr");
        return;
    }

    if (coefficient == undefined) {
        alert("Choose resolution");
        return;
    }

    if (colors == undefined) {
        alert("Create palette first");
        return;
    }

    $.ajax({
        type: "POST",
        timeout:30000,
        url: "AddEmbroidery/CreateEmbroidery",
        data: { img: image, coefficient: coefficient, cellsCount: cellsCount, colors: colors.join(), symbols: symbols.join(), symbolColor: symColor, grid: grid },
        beforeSend: function () {
            $('#preview').empty();
            $('#preview').prepend('<text>Wait a little bit...</text>');
        },
        success: function (result) {
            $('#preview').empty();
            $('#preview').prepend('<img src="">');
            $('#preview img').attr('src', "data:image/jpeg;base64," + result.imageString);
                
        },
        error: function () {
            alert('Error was occured. result imagr might be too large. Try choose lower resolution or put lower cells count. :(');
        }
    });
  
}



$('#create-embroidery-button').click(function () {
    CreateEmbroidery();
});


$('#add-embroidery-button').click(function () {

    var src = $('#preview img').attr('src');


    $.ajax({
        type: "POST",
        url: "AddEmbroidery/AddEmbroidery",
        data: { img: src },
        success: function (result) {
            $('#content').html(result);
        }
    });
});

/**********************************************************************************/


/*Symbols**********************************************/

$('#add-symbols').click(function () {
    var colors = getColors();

    for (var i = 0; i < colors.length; i++) {
        var str_i = (i + 1).toString();
        $('#symbols').append('<input class="symbol" type="text" value="' + str_i + '"  id="text' + str_i + '" />');
    }
});


function getSymbols() {
    var symbols = [];

    $('.symbol').each(function () {
        symbols.push($(this).val());
    });

    return symbols;
}


function getSymbolColor() {
    return $('#symbols-color').css('background-color');
}


$('#symbols-color').click(function (event) {
    var symbol_color_border = $('#symbol-color-palette');

    var primes = ["#000000", "#ffffff", "#ff0000", "#00ff00", "#0000ff"];
    $('.symbol-color-box').each(function (i, elem) {
        $(this).css('background-color', primes[i]);
        $(this).click(function () {
            $(this).unbind('click');
            $('#symbols-color').css('background-color', $(this).css('background-color'));
            symbol_color_border.css('display', 'none');
        });
    });

    symbol_color_border.css('left', event.clientX.toString() + 'px');
    symbol_color_border.css('top', event.clientY.toString() + 'px');
    symbol_color_border.css('display', 'inherit');
});


/******************************************************/

/*Palette********************************************************/

$.fn.colorPalette = function (options) {
    this.addClass("color-palette");
    this.colors = {};
    this.bgColor = options.bgColor || "#e8e8e8";
    this.css({
        "background-color": this.bgColor
    });

    var greys = ["#000000", "#222222", "#333333", "#444444", "#666666", "#888888", "#aaaaaa", "#cccccc", "#eeeeee", "#ffffff"];

    for (var i = 0; i < greys.length; i++) {
        var grey = greys[i];
        var colorSwatch = $("<div class='color-swatch' data-color='" + grey + "'></div>");
        colorSwatch.css({ "background-color": grey });

        this.colors[grey] = colorSwatch;
        this.append(colorSwatch);
    }
    this.append("<div class='clear'/>");
    this.append("<div class='break'/>");

    var primes = ["#990001", "#ff0000", "#ff9900", "#ffff00", "#00ff00", "#00ffff", "#4a86e8", "#0000ff", "#9900ff", "#ff00ff"];

    for (var i = 0; i < primes.length; i++) {
        var prime = primes[i];
        var colorSwatch = $("<div class='color-swatch' data-color='" + prime + "'></div>");
        colorSwatch.css({ "background-color": prime });

        this.colors[prime] = colorSwatch;
        this.append(colorSwatch);
    }
    this.append("<div class='clear'/>");
    this.append("<div class='break'/>");

    var otherColors = ["#E6B8AF", "#F4CCCC", "#FCE5CD", "#FFF2CC", "#D9EAD3", "#D0E0E3", "#C9DAF8", "#CFE2F3", "#D9D2E9", "#EAD1DC", "#DD7E6B", "#EA9999", "#F9CB9C", "#FFE599", "#B6D7A8", "#A2C4C9", "#A4C2F4", "#9FC5E8", "#B4A7D6", "#D5A6BD", "#CC4125", "#E06666", "#F6B26B", "#FFD966", "#93C47D", "#76A5AF", "#6D9EEB", "#6FA8DC", "#8E7CC3", "#C27BA0", "#A61C00", "#CC0000", "#E69138", "#F1C232", "#6AA84F", "#45818E", "#3C78D8", "#3D85C6", "#674EA7", "#A64D79", "#85200C", "#990000", "#B45F06", "#BF9000", "#38761D", "#134F5C", "#1155CC", "#0B5394", "#351C75", "#741B47", "#5B0F00", "#660000", "#783F04", "#7F6000", "#274E13", "#0C343D", "#1C4587", "#073763", "#20124D", "#4C1130"];

    for (var i = 0; i < otherColors.length; i++) {
        var otherColor = otherColors[i]
        var colorSwatch = $("<div class='color-swatch' data-color='" + otherColor + "'></div>");
        colorSwatch.css({ "background-color": otherColor });

        this.colors[otherColor] = colorSwatch;
        this.append(colorSwatch);
    }

    for (var color in this.colors) {
        if (color == "#900") { console.log("yup") }
        var swatch = this.colors[color];

        swatch.click(function () {
            $(this).siblings().removeClass("selected");
            $(this).addClass("selected");

            var color = $(this).data("color");

            options.colorChange(color);

            var choosedPalette = $('#choosed-palette-border');
            var newDiv = $('<div class="choosed"></div>');
            choosedPalette.prepend(newDiv);

            newDiv.css('background-color', color);

            newDiv.click(function () {
                $(this).remove();
            });
        });
    }

    this.append("<div class='clear'/>");

    this.changeSelected = function (color) {
        var swatch = this.colors[color];
        if (swatch) {
            swatch.siblings().removeClass("selected");
            swatch.addClass("selected");
            options.colorChange(color);
        }
    };

    return this;
}

var colorPalette = $(".palette-window").colorPalette({
    colorChange: function (color) {
        $(".result").css("background", color);
    },
    bgColor: "#000"
});

colorPalette.changeSelected("#ff9900");




$('#add-palette').click(function (event) {
    var palette = $('.palette');
    palette.css('position', 'absolute');
    palette.css('top', event.clientY);
    palette.css('left', event.clientX);

    palette.css('display', 'block');

});



function getColors() {
    var colors = [];

    $('.choosed').each(function () {

        var current = $(this);
        var color = current.css('background-color');
        colors.push(color);
    });

    return colors
}




/*********************************************************************/

