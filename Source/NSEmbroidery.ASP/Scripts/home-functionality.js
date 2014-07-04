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
        loading.css('display', 'inherit');
       
    },

    HideLoading: function () {
        var loading = $('#loading-area');
        loading.css('display', 'none');
    }

}
