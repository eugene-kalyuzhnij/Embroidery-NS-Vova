﻿var Embroidery =
        {

            OpenImage: function (id) {
                    $.ajax({
                    url: 'Gallery/GetEmbroidery',
                    data: { embroideryId: id },
                    type: 'POST',
                    success: function (result) {
                        $('body').css('overflow', 'hidden');

                        $('#open-image').empty();
                        $('#open-image').prepend('<img class="opened" src="" />');
                        $('#open-image img').prop('src', result.imageString);

                        if (result.alloweChangePublic) {

                            var allowOther = $('#allowe-other');
                            allowOther.empty();
                            allowOther.prepend('<input type="checkbox" id="allowe-other-check"/><label>Allowed for other users</label>');

                            if (result.allowePublic) {
                                $('#allowe-other input').prop('checked', true);
                            }
                            else $('#allowe-other input').prop('checked', false);


                            Embroidery.ChangeEmbroideryAllow(id);
                        }


                        Embroidery.UpdateComments(id);
                        Embroidery.UpdateLikesCount(id);
                        Embroidery.AddRemoveClick(id);
                        Embroidery.UsersLikes(id);
                        Embroidery.BindSendComment(id);


                        if (Embroidery.IsThereNextEmbroidery(id)) {

                            $('#open-image-border').prepend('<div id="open-image-next" class="move-to-image-button">NEXT</div>');

                            var userId = $('#gallery').attr('data-user-id');

                            if (userId != undefined) {

                                var next = $('#open-image-next');
                                next.on('click', function () {
                                    next.unbind('click');
                                    Embroidery.NextEmbroidery(id, userId);
                                });



                            }
                        }

                        if (Embroidery.IsTherePrevEmbroidery(id)) {
                            $('#open-image-border').prepend('<div id="open-image-prev" class="move-to-image-button">PREV</div>');

                            var userId = $('#gallery').attr('data-user-id');

                            if (userId != undefined) {

                                var prev = $('#open-image-prev');
                                prev.on('click', function () {
                                    prev.unbind('click');
                                    Embroidery.PrevEmbroidery(id, userId);
                                });
                            }
                        }


                            $('#open-image-border').fadeIn('slow');

                            $('#image-border').on('click', function () {
                                Embroidery.DisposeOpenImage();
                            });
                        

                    },
                    error: function (a, b, c) {

                        alert('Could not upload embroidery from database');

                    }

                });
            },

            ChangeEmbroideryAllow: function (id) {
                $('#allowe-other-check').on('click', function () {
                    var checked = $(this).prop('checked');
                    $.ajax({
                        url: 'Gallery/ChangeEmbroideryAllow',
                        type: 'post',
                        global:false,
                        data: { embroideryId: id, newAllow: checked },
                        success: function (r, event) {
                            
                            if (!r.result) {
                                alert('You can not do this operation');
                                return;
                            }
                            if (checked)
                                $('div[data-embroidery-id="' + id.toString() + '"]').prepend('<div class="shared">SHARED</div>');
                            else
                                $('div[data-embroidery-id="' + id.toString() + '"] > div.shared').remove();
                        },
                        error: function () {
                            alert('Was not able to change allow');
                        }
                    });
                });
            },

            AddRemoveClick: function (embroideryId) {
                var add_remove_Like = $('#add-like-border');

                $.ajax({
                    url: 'Gallery/CanAddLike',
                    type: 'post',
                    global:false,
                    data: { embroideryId: embroideryId },
                    success: function (canAdd) {

                        if (canAdd == 'True') {
                            add_remove_Like.unbind('click');
                            add_remove_Like.empty();
                            add_remove_Like.prepend('Add like');
                            var handler = function () {
                                $.ajax({
                                    url: 'Gallery/AddLike',
                                    data: { embroideryId: embroideryId },
                                    type: 'post',
                                    global:false,
                                    success: function () {
                                        Embroidery.UpdateLikesCount(embroideryId);
                                        Embroidery.AddRemoveClick(embroideryId);
                                    }
                                });
                            }

                            add_remove_Like.bind('click', handler);
                        }
                        else {
                            add_remove_Like.unbind('click');
                            add_remove_Like.empty();
                            add_remove_Like.prepend('Remove like');

                            var handler = function () {
                                $.ajax({
                                    url: 'Gallery/RemoveLike',
                                    data: { embroideryId: embroideryId },
                                    type: 'post',
                                    global:false,
                                    success: function () {
                                        Embroidery.UpdateLikesCount(embroideryId);
                                        Embroidery.AddRemoveClick(embroideryId);
                                    }
                                });
                            }
                            add_remove_Like.bind('click', handler);
                        }
                    }
                });
            },

            DisposeOpenImage: function (fadeOut) {

                if (fadeOut == undefined) fadeOut = true;

                if (fadeOut) {
                    $('#open-image-border').fadeOut('slow');
                    $('body').css('overflow', 'auto');
                }
                $('#send-comment').unbind('click');
                $('#like-border').unbind('click');
                $('#allowe-other-check').unbind('click');
                $('#image-border').unbind('click');
                $('#open-image-next').unbind('click');
                $('#open-image-prev').unbind('click');
                $('#open-image-prev').remove();
                $('#open-image-next').remove();
            },

            CancelImageLoading: function () {
                $('#open-image-border').attr('data-loading', 'false');
            },

            SetImageLoading: function () {
                $('#open-image-border').attr('data-loading', 'true');
            },

            IsLoadingImage: function () {
                var result = $('#open-image-border').attr('data-loading');
                return result;
            },

            UpdateLikesCount: function (embroideryId) {

                $.ajax({
                    url: 'Gallery/GetLikesCount',
                    type: 'post',
                    dataType: 'json',
                    global:false,
                    data: { embroideryId: embroideryId },
                    success: function (result) {
                        var likes = $('#like-border');
                        likes.empty();
                        likes.prepend(result.toString());
                    }
                })
            },

            UpdateComments: function (embroideryId) {
                $.ajax({
                    url: 'Gallery/GetComments',
                    dataType: 'json',
                    type: 'post',
                    global:false,
                    data: { EmbroideryId: embroideryId },
                    success: function (result) {
                        var all_comments = $('.comments');

                        $('.comments div').remove();

                        for (var i in result) {

                            var comment = result[i].Comment;
                            var str = comment.replace(/\n/g, '<br />');

                            all_comments.prepend('<div class="comment-border">' +
                                                 '<div class="who-commented" data-user-id="' + result[i].UserId.toString() + '">' + result[i].UserName + '</div>' +
                                                 '<div class="comment"><div class="comment-text">' + str + '</div>' +
                                                 '<div class="comment-date">' + result[i].DateCreated.toString() + '</div></div></div>');

                            $('.who-commented').click(function () {
                                Embroidery.DisposeOpenImage();
                                var id = $(this).attr('data-user-id');
                                Embroidery.OtherUser(id);
                            });
                        }

                    },
                    error: function (request, status, error) {
                        alert("Error was occured");
                    }
                });

            },

            AddComment: function (embroideryId) {
                var comment = $('#input-comment');
                if (comment.val() != "") {
                    $.ajax({
                        type: "post",
                        global:false,
                        url: "Gallery/AddComment",
                        data: { EmbroideryId: embroideryId, comment: comment.val() },
                        success: function () {
                            Embroidery.UpdateComments(id);
                            comment.val("");
                        }
                    });
                }
            },

            HideUsersLikes: function (embroideryId) {
                $('#likes-users').css('display', 'none');
                $('#open-image-border').unbind('click');
                
            },

            OtherUser: function (userId) {
                $.ajax({
                    url: 'Users/OtherUser',
                    data: { userId: userId },
                    type: 'post',
                    success: function (result) {
                        Embroidery.DisposeOpenImage();
                        var content = $('#content');
                        content.empty();

                        content.html(result);
                    }
                });
            },

            ShowUsersLikes: function (embroideryId, x, y) {
                var likes_users = $('#likes-users');

                if (likes_users.css('display') == 'none') {

                    likes_users.empty();
                    likes_users.css('display', 'inherit');
                    likes_users.css('left', x.toString() + 'px');
                    likes_users.css('top', y.toString() + 'px');

                    $.ajax({
                        url: 'Gallery/GetLikesUsers',
                        data: { embroideryId: embroideryId },
                        dataType: 'json',
                        type: 'post',
                        global: false,
                        success: function (result) {
                            for (var i in result) {
                                var userList = likes_users.prepend('<div data-user-id="' + result[i].UserId + '"> '
                                                       + result[i].UserName + '</div>');
                            }

                            $('#likes-users div').click(function () {
                                var userId = $(this).attr('data-user-id');
                                Embroidery.OtherUser(userId);
                            });


                            $('#open-image-border').click(function () {
                                Embroidery.HideUsersLikes(embroideryId);
                            });
                        }
                    });
                }
            },

            UsersLikes: function (embroideryId) {

                $('#like-border').click(function (event) {
                    Embroidery.ShowUsersLikes(embroideryId, event.clientX, event.clientY);
                });
            },

            BindSendComment: function (id) {
                $('#send-comment').click(function () {
                    var comment = $('#input-comment');
                    if (comment.val() != "") {
                        $.ajax({
                            type: "post",
                            url: "Gallery/AddComment",
                            global:false,
                            data: { EmbroideryId: id, comment: comment.val() },
                            success: function () {
                                Embroidery.UpdateComments(id);
                                comment.val("");
                            }
                        });
                    }
                });
            },

            NextEmbroidery: function (id, userId) {

                var currentEmbroidery = $('.gallery-border > [data-embroidery-id=' + id.toString() + ']').attr('data-embroidery-current');
                var nextEmbroidery = parseInt(currentEmbroidery) + 1;
                var idNextEmbroidery = $('.gallery-border > [data-embroidery-current=' + nextEmbroidery.toString() + ']').attr('data-embroidery-id');
                if (idNextEmbroidery != undefined) {

                    Embroidery.DisposeOpenImage(false);
                    Embroidery.OpenImage(idNextEmbroidery);
                }
            },

            PrevEmbroidery: function (id, userId) {

                var currentEmbroidery = $('.gallery-border > [data-embroidery-id=' + id.toString() + ']').attr('data-embroidery-current');
                var prevEmbroidery = parseInt(currentEmbroidery) - 1;
                var idPrevEmbroidery = $('.gallery-border > [data-embroidery-current=' + prevEmbroidery.toString() + ']').attr('data-embroidery-id');
                if (idPrevEmbroidery != undefined) {

                    Embroidery.DisposeOpenImage(false);
                    Embroidery.OpenImage(idPrevEmbroidery);
                }

            },

            IsTherePrevEmbroidery: function (embroideryId) {
                var currentEmbroidery = $('.gallery-border > [data-embroidery-id=' + embroideryId.toString() + ']').attr('data-embroidery-current');
                var prevEmbroidery = parseInt(currentEmbroidery) - 1;
                var idPrevEmbroidery = $('.gallery-border > [data-embroidery-current=' + prevEmbroidery.toString() + ']').attr('data-embroidery-id');
                if (idPrevEmbroidery != undefined) {
                    return true;
                }
                return false;
            },

            IsThereNextEmbroidery: function (embroideryId) {
                var currentEmbroidery = $('.gallery-border > [data-embroidery-id=' + embroideryId.toString() + ']').attr('data-embroidery-current');
                var nextEmbroidery = parseInt(currentEmbroidery) + 1;
                var idNextEmbroidery = $('.gallery-border > [data-embroidery-current=' + nextEmbroidery.toString() + ']').attr('data-embroidery-id');
                if (idNextEmbroidery != undefined) {
                    return true;
                }
                return false;
            }

        }

