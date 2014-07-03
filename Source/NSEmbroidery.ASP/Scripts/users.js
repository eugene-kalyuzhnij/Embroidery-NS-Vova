

$('.users-list-user').click(function () {
    var userId = $(this).attr('data-user-id');
    var content = $('#content');
    var switchLoader = $('#switch-loader');

    $.ajax({
        url: 'Users/OtherUser',
        type: 'post',
        data: { userId: userId },
        beforeSend: function(){
            content.css('opacity', '0.5');
            switchLoader.empty();
            switchLoader.prepend('<img src="Images/ajax-loader.gif"></img>');
        },
        success: function(result){
            content.css('opacity', '1');
            switchLoader.empty();
            content.empty();
            content.html(result);
        },
        error: function () {
            content.css('opacity', '1');
            switchLoader.empty();
            alert('error was occcurred');
        }

    });

});

