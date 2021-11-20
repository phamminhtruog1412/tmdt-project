var deleteP = function () {
        $('.deleteProduct').off('click').on('click', function (e) {
            if (confirm('Bạn có muốn xóa bản ghi này?'))
            {
                e.preventDefault();
                var btn = $(this);
                var id = btn.data('proid');
                $.ajax({
                    url: '/Admin/Product/Delete',
                    data: { id: id },
                    type: 'POST',
                    dataType: 'json',
                    success: function (res) {
                        if(res.Status)
                        {
                            $('#row_' + id + '').remove();
                        }
                        else
                        {
                            alert('Sản phẩm còn tồn trong kho. Xóa thất bại.');
                        }
                    }
                });
            }
        });
    }

deleteP();