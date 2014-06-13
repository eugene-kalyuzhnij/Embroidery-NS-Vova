
/*Log in - log out************************************************/
$('#current-user').on('click', function () {
    debugger
    $.ajax({
        url: 'Account/EditProfile',
        type: 'post',
        success: function (result) {
            var content = $('#content');
            content.empty();
            content.html(result);
            debugger
            $('#change-password-button').on('click', function () {
                alert('click');
                var currentPass = $('#current-password').val();
                var newPass = $('#new-password').val();

                $.ajax({
                    url: 'Account/ChangePassword',
                    type: 'post',
                    data: { currentPassword: currentPass, newPassword: newPass },
                    success: function (result) {
                        alert('Password has been changed');
                    },
                    error: function () {
                        alert("Some error has been occured");
                    }
                });
            });
        },
        error: function () {
            alert('error was occured');
        }

    });

});

/*****************************************************************/