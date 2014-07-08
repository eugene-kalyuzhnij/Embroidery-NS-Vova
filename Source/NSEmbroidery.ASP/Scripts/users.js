

$('.users-list-user').click(function () {
    var userId = $(this).attr('data-user-id');

    var content = $('#content');

    $.ajax({
        url: 'Users/OtherUser',
        type: 'post',
        data: { userId: userId },
        success: function(result){
            content.empty();
            content.html(result);
        },
        error: function () {
            alert('error was occcurred');
        }

    });

});

