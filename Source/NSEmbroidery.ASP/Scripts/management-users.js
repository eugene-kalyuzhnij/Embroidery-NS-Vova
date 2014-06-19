

$('.management-users-list-delete').on('click', function () {
    var userId = $(this).attr('data-user-id');
    $.ajax({
        url: 'Management/DeleteUser',
        data: { userId: userId },
        type: 'post',
        success: function (result) {
            if (result.Result) {
                $('[data-user-id=' + userId + ']').parent().remove();
            }
            else alert('Operation faild');
        },
        error: function (a, b, c) {
            alert('ERROR WAS OCUURED');
        }

    });
});


$('.management-users-list-user').on('click', function () {
    var userId = $(this).attr('data-user-id');

    $.ajax({
        url: 'Management/UserContent',
        type: 'get',
        data: { userId: userId},
        cache:false,
        success: function(result){
            var content = $('#content');
            content.empty();
            content.html(result);
        },
        error: function (a, b, c) {
            alert('Some error was occurred');
        }
    });
});