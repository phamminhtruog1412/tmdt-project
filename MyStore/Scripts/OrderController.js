var order = {
    init: function () {
        order.changS();
        order.deleteO();
    },

    deleteO: function() {
        $('.delete-Order').off('click').on('click', function (e) {
            if (confirm('Bạn có muốn xóa bản ghi này?')) {
                e.preventDefault();
                var btn = $(this);
                var id = btn.data('cateid');
                $.ajax({
                    url: '/Admin/Order/Delete',
                    data: { id: id },
                    dateType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.Status == true) {
                            OnSuccess = $('#row_' + id + '').remove();
                        }
                    }
                });
            }
        });
    },

    changS: function () {
        $('.btn-Sta').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            var val = btn.text();
            if (val == "Đã giao hàng") {
                $('#message-text').val("");
                btn.attr({ 'data-toggle': 'modal', 'data-target': '#cancelNoteModal' });
                $('#btnHoanThanh').off('click').on('click', function (f) {
                    f.preventDefault();
                    $.ajax({
                        url: '/Admin/Order/ChangeStatus',
                        data: { id: id },
                        dataType: 'json',
                        type: 'POST',
                        success: function () {
                            btn.text('Đã hủy');
                            btn.removeAttr('data-toggle', 'data-target');
                        }
                    });

                    var note = $('#message-text').val();
                    $.ajax({
                        url: '/Admin/Order/Update',
                        data: {
                            cancelNote: note,
                            id: id
                        },
                        dataType: 'json',
                        type: 'POST',
                        success: function (res) {
                            $('#td_' + id + '').html(note);
                        }
                    });
                });
            }
            else {
                $.ajax({
                    url: '/Admin/Order/ChangeStatus',
                    data: { id: id },
                    dataType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.Status == 0) {
                            btn.text('Đang xử lý');
                        } else if (res.Status == 1) {
                            btn.text('Đã giao hàng');
                        }
                    }
                });

                $.ajax({
                    url: '/Admin/Order/Update',
                    data: {
                        cancelNote: "",
                        id: id
                    },
                    dataType: 'json',
                    type: 'POST',
                    success: function (res) {
                        $('#td_' + id + '').html("");
                    }
                });
            }

        });
    }





    //changS: function () {
    //    $('.btn-Sta').off('click').on('click', function (e) {
    //        e.preventDefault();
    //        var btn = $(this);
    //        var id = btn.data('id');
    //        $.ajax({
    //            url: '/Admin/Order/ChangeStatus',
    //            data: { id: id },
    //            dataType: 'json',
    //            type: 'POST',
    //            success: function (res) {
    //                if (res.Status == 0) {
    //                    btn.text('Đang xử lý');
    //                } else if(res.Status == 1){
    //                    btn.text('Đã giao hàng');
    //                } else if (res.Status == 2) {
    //                    btn.text('Đã hủy');
    //                }
    //            }
    //        });
    //    });
    //}
}

order.init();