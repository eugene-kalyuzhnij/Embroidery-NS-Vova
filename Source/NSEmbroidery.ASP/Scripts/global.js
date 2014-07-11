
$(document).ready(function () {
    $("#log-off").click(function () {
        $.post('Account/LogOff', null, function (result) {
            $('*').html(result);
        });
    });

    $('#current-user').click(function () {
        $.ajax({
            url: 'Account/EditProfile',
            type: 'post',
            success: function (result) {
                var content = $('#content');
                content.empty();
                content.html(result);

            },
            error: function () {
                alert('error was occured');
            }

        });

    });


    $('#home-button').on('click', function () {
        location.reload();
    });

    $('#register').on('click', function () {
        $.get("Register", {}, function (result) {
            $(document.body).html(result);
        }).fail(function (a, b, c) {
            alert('Sorry. Some error was occurred. Try again later');
        });
    });

    $('#log-in').on('click', function () {
        $.get("Login", {}, function (result) {
            $(document.body).html(result);
        }).fail(function () {
            alert('Sorry. Some error was occurred. Try again later');
        });
        
    });

});