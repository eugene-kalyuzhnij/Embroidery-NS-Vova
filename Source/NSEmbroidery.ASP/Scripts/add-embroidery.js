
var image = null;
var allowAdd = false;

var AddEmbroidery = {

    createEmbroidery: function () {

        var cellsCount = $('#cells-count').val();
        var coefficient = $('#resolutions').val();
        var colors = this.getColors();
        var symbols = this.getSymbols();
        var symColor = this.getSymbolColor();
        var grid = $('#grid').prop("checked");

        if (image == null) {
            alert("Open image first");
            return;
        }

        if (!AddEmbroidery.isInt(cellsCount)) {
            alert("Wrong count of cells parametr");
            return;
        }

        if (coefficient == '0' || coefficient == undefined ) {
            alert("Choose resolution");
            return;
        }
        if (colors.length == 0) {
            alert("Create palette first");
            return;
        }
        if (symbols.length > 0)
            if (symbols.length < colors.length) {
                alert('Not enought symbols');
                return;
            }

        $.ajax({
            type: "POST",
            timeout: 30000,
            url: "AddEmbroidery/CreateEmbroidery",
            data: { img: image, coefficient: coefficient, cellsCount: cellsCount, colors: colors.join(), symbols: symbols.join(), symbolColor: symColor, grid: grid },
            beforeSend: function () {
                var preview = $('#preview');
                preview.empty();
                var wait_text = preview.prepend('<label class="wait-text">Please wait...</label>');
            },
            success: function (result) {
                $('#preview').empty();
                $('#preview').prepend('<img src="">');
                $('#preview img').attr('src', "data:image/jpeg;base64," + result.imageString);
                allowAdd = true;
            },
            error: function () {
                $('.wait-text').remove();
                alert('Error was occured. Maybe image is too large. Try choose lower resolution or put lower cells count. :(');
            }
        });

    },

    getSymbols: function () {
        var symbols = [];

        $('.symbol').each(function () {
            var currentVal = $(this).val();

            if (currentVal != "")
                symbols.push(currentVal);
        });

        return symbols;
    },

    getSymbolColor: function () {
        return $('#symbols-color').css('background-color');
    },

    getColors: function () {
        var colors = [];

        $('.choosed').each(function () {

            var current = $(this);
            var color = current.css('background-color');
            colors.push(color);
        });

        return colors
    },

    createPalette: function () {
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
    },

    isInt: function (value) {
        if ((parseFloat(value) == parseInt(value)) && !isNaN(value)) {
            return true;
        } else {
            return false;
        }
    },

    readUrlForPreview: function (input) {
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
    },

    resolutionsText: function (text) {
        var resolutions = $('#resolutions');
        resolutions.empty();
        var item = [];
        item.push('<option value="0">' + text + '</option>');
        resolutions.html(item.join(' '));
    }

}



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


$("#browse").change(function () {
    AddEmbroidery.readUrlForPreview(this);
    AddEmbroidery.resolutionsText('--Choose resolution--');

    allowAdd = false;
});

$('#create-embroidery-button').click(function () {
    AddEmbroidery.createEmbroidery();
});


$('#add-embroidery-button').click(function () {

    if (allowAdd) {
        var src = $('#preview img').attr('src');
        var allowePublic = $('#allowed').prop('checked');
        var name = $('#input-name').val();
        if (name == "") {
            alert("Input name first");
            return;
        }

        $.ajax({
            type: "POST",
            url: "AddEmbroidery/AddEmbroidery",
            data: { img: src, allowePublic: allowePublic, name: name },
            beforeSend: function () {
                $('#add-embroidery-loading').css('display', 'block');
            },
            success: function (result) {
                $('#add-embroidery-loading').css('display', 'none');
                $('#content').html(result);
            },
            error: function () {
                alert('error was occured');
            }
        });
    }
    else {
        alert("Create embroidery first");
    }
});


$('#cells-count').change(function () {
    AddEmbroidery.resolutionsText('--Choose resolution--');
});


$('#resolutions').click(function (event) {
    var resolutions = $(this);
    var current_selected_val = $('#resolutions :selected').text();
    if (current_selected_val != '--Choose resolution--')
        return;

    var cellsCount = $('#cells-count').val();

    if (!AddEmbroidery.isInt(cellsCount) || parseInt(cellsCount) <= 1) {
        alert('Count of cells has to be number more than 1');
        return;
    }
    if (image == null) {
        alert('Open image first');
        return;
    }
    var response = null;

    $.ajax({
        type: "POST",
        global:false,
        url: 'AddEmbroidery/GetResolutions',
        data: { cells: cellsCount, img: image },
        beforeSend: function () {
            AddEmbroidery.resolutionsText('--Please wait--');
        },
        success: function (result) {
            if (result.IsValidCells) {
                var items = [];
                var i = 0;
                $.each(result.Resolutions, function () {
                    if(i == 0)
                        items.push("<option value=" + this.Value + " selected>" + this.Text + "</option>");
                    else
                        items.push("<option value=" + this.Value + ">" + this.Text + "</option>");
                    i++;
                });
                resolutions.html(items.join(' '));
            }
            else {
                alert('Input less cells than imagen\'s resolution');
                AddEmbroidery.resolutionsText('--Choose resolution--');
            }
        },
        error: function () {
            alert('Sorry, but some error was occurred. Try again');
            AddEmbroidery.resolutionsText('--Choose resolution--');
        }
    });

});


/*Symbols**********************************************/

$('#add-symbols').click(function () {
    var colors = AddEmbroidery.getColors();
    var symbols = $('#symbols');
    symbols.empty();

    for (var i = 0; i < colors.length; i++) {
        if (i < 9) {
            var str_i = (i + 1).toString();
            symbols.append('<input class="symbol" type="text" value="' + str_i + '"  id="text' + str_i + '" />');
        }
        else {
            symbols.append('<input class="symbol" type="text" value="" />');
        }

    }
    symbols.append('<input id="remove-symbols-button" type="button" value="Remove symbols" />');
    $('#remove-symbols-button').click(function () {
        symbols.empty();
        $(this).unbind('click');
    });

});



$('#symbols-color').css('background-color', '#000000');


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

    var otherColors = ["#e6b8af", "#f4cccc", "#fce5cd", "#fff2cc", "#d9ead3", "#d0e0e3", "#c9daf8", "#cfe2f3", "#d9d2e9", "#ead1dc", "#dd7e6b", "#ea9999", "#f9cb9c", "#ffe599", "#b6d7a8", "#a2c4c9", "#a4c2f4", "#9fc5e8", "#b4a7d6", "#d5a6bd", "#cc4125", "#e06666", "#f6b26b", "#ffd966", "#93C47D", "#76A5AF", "#6D9EEB", "#6FA8DC", "#8E7CC3", "#C27BA0", "#A61C00", "#CC0000", "#E69138", "#F1C232", "#6AA84F", "#45818E", "#3C78D8", "#3D85C6", "#674EA7", "#A64D79", "#85200C", "#990000", "#B45F06", "#BF9000", "#38761D", "#134F5C", "#1155CC", "#0B5394", "#351C75", "#741B47", "#5B0F00", "#660000", "#783F04", "#7F6000", "#274E13", "#0C343D", "#1C4587", "#073763", "#20124D", "#4C1130"];

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
            var color = $(this).data("color");
            var colors = AddEmbroidery.getColors();

            var colorExist = false;

            for (var i in colors) {
                if (colors[i].toLowerCase() == color.toLowerCase()) {
                    colorExist = true;
                    break;
                }
            }


            if (!colorExist) {
                $(this).siblings().removeClass("selected");
                $(this).addClass("selected");

                options.colorChange(color);

                var choosedPalette = $('#choosed-palette-border');
                var newDiv = $('<div class="choosed"></div>');
                choosedPalette.prepend(newDiv);

                newDiv.css('background-color', color);

                newDiv.click(function () {
                    $(this).remove();
                });
            }
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


/*********************************************************************/