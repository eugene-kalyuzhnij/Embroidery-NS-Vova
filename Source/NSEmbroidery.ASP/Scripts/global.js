
$(document).ready(function () {
    $("#log-off").click(function () {
        $.post('Account/LogOff', null, function (result) {
            location.reload();
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


});