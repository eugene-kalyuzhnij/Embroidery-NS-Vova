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
        if (data == null) data = { };
        var switchLoader = $('#switch-loader');
        var content = $('#content');
        $.ajax({
            url: url,
            type: type,
            data: data,
            beforeSend: function () {
                Embroidery.CancelImageLoading();
            },
            success: function (result) {
                content.empty();
                content.html(result);
            },
            error: function () {
                alert('Sorry. Some error was occurred');
            }
        });
    },

    ShowLoading: function () {

        var loading = $('#loading-area');

        var loading_image = $('#loading-area > img');
        var width = loading_image.attr('width');
        var height = loading_image.attr('height');

        loading_image.css('margin-top', '-' + (height / 2).toString() + 'px');
        loading_image.css('margin-left', '-' + (width / 2).toString() + 'px');

        loading.css('display', 'inherit');

       
        
    },

    HideLoading: function () {
        var loading = $('#loading-area');
        loading.css('display', 'none');
    }

}
