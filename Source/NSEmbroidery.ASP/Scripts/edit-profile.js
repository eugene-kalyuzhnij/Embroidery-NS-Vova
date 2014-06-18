

$('#change-password-button').on('click', function () {
    var currentPass = $('#current-password').val();
    var newPass = $('#new-password').val();

    $.ajax({
        url: 'Account/ChangePassword',
        type: 'post',
        data: { currentPassword: currentPass, newPassword: newPass },
        success: function (result) {
            debugger
            if (result.Result) {
                var name_border = $('#change-password-border');
                name_border.empty();
                name_border.prepend('<div class="label"> Password has been changed</div>');
            }
            else alert('Operation was failed');
        },
        error: function () {
            alert("Some error has been occured");
        }
    });
});


$('#change-name-button').on('click', function () {
    var firstName = $('#first-name').val();
    var lastName = $('#last-name').val();

    $.ajax({
        url: 'Account/ChangeName',
        data: { newFirstName: firstName, newLastName: lastName },
        type: 'post',
        success: function (result) {
            if (result.Result) {
                var name_border = $('#change-name-border');
                name_border.empty();
                name_border.prepend('<div class="label"> Name has been changed </div>');
                $('#current-user').html('Hello, ' + firstName);
            }
            else alert('Operation was failed');
        },
        error: function (error) {
            alert("Could not change your name. Try again later");
        }

    });
});


/*
$('#change-email-button').on('click', function () {
    debugger
    var newEmail = $('#new-email').val();
    debugger
    $.ajax({
        url: 'Account/ChangeEmail',
        data: { newEmail: newEmail },
        type: 'post',
        success: function (result) {
            debugger
            if (result.Result) {
                var name_border = $('#change-email-border');
                name_border.empty();
                name_border.prepend('<div class="label"> Email has been changed</div>');
            }
            else alert('Operation was failed');
        },
        error: function (error) {
            debugger
            alert('Could not change your email. Try again later');
        }
    });
});
*/

