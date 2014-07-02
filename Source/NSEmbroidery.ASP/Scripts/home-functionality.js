var Home = {

    loadPage: function (pageNumber) {

        switch (pageNumber) {
            case 0: {

                var url = 'Gallery/Index';
                var type = 'post';
                var hashName = "Gallery";

                Home.updateContent(url, hashName, type, null);
                break;
            }
            case 1: {
                var url = 'Users/Index';
                var type = 'post';
                var hashName = "Users";

                Home.updateContent(url, hashName, type, null);
                break;
            }
            case 2: {
                var url = 'AddEmbroidery/Index';
                var type = 'get';
                var hashName = "AddEmbroidery";

                Home.updateContent(url, hashName, type, null);
                break;
            }
        }

    },


    updateContent: function (url, hashName, type, data) {
        if (data == null) data = {};
        var switchLoader = $('#switch-loader');
        var content = $('#content');
        $.ajax({
            url: url,
            type: type,
            data: data,
            beforeSend: function () {
                content.css('opacity', '0.5');
                switchLoader.empty();
                switchLoader.prepend('<img src="Images/ajax-loader.gif"></img>');
            },
            success: function (result) {
                content.css('opacity', '1');
                switchLoader.empty();
                content.empty();
                content.html(result);
            },
            error: function () {
                content.css('opacity', '1');
                switchLoader.empty();
                alert('Sorry. Some error was occurred');
            }
        });
    }


}
