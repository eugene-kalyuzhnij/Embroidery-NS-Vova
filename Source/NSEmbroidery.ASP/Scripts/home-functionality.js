var Home = {

    loadPage: function (pageNumber) {

        switch (pageNumber) {
            case 0: {

                var url = 'Gallery/Index';
                var type = 'post';
                var hashName = "Gallery";

                Home.updateContent(url, hashName, type, null, pageNumber);
                break;
            }
            case 1: {
                var url = 'Users/Index';
                var type = 'post';
                var hashName = "Users";

                Home.updateContent(url, hashName, type, null, pageNumber);
                break;
            }
            case 2: {
                var url = 'AddEmbroidery/Index';
                var type = 'get';
                var hashName = "AddEmbroidery";

                Home.updateContent(url, hashName, type, null, pageNumber);
                break;
            }
        }

    },


    updateContent: function (url, hashName, type, data, pageNumber) {
        if (data == null) data = {};

        $.ajax({
            url: url,
            type: type,
            data: data,
            success: function (result) {              
                var content = $('#content');
                content.empty();
                content.html(result);

            },
            error: function () {
                alert('Sorry. Some error was occurred');
            }
        });
    }


}
