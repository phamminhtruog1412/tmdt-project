var category = {
    init: function () {
        category.deleteC();
    },

    deleteC: function () {
        $('.linkDelete').off('click').on('click', function (e) {
            
            if (confirm('Bạn có muốn xóa bản ghi này?')) {
                e.preventDefault();
                var link = $(this);
                var id = link.data('cateid');
                $.ajax({
                    url: '/Admin/ProductCategory/Delete',
                    data: { id: id },
                    dataType: 'json',
                    type: 'POST',
                    confirm: 'Bạn có muốn xóa bản ghi này?',
                    success: function (res) {
                        if (res.Status == true) {
                            OnSuccess = $('#row_' + id + '').remove();
                        } else {
                            alert('Danh mục có chứa nhiều sản phẩm. Xóa thất bại.');
                        }
                    }
                });
            }
        });
    }
}

category.init();
