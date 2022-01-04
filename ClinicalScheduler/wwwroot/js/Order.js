$("#save").click(function (e) {
    var button_text = $('#save').text();
    if (button_text == "Create") {

        event.preventDefault();
        var orderCatalogId = $('#updateOrdId').val();
        var encntrId = $('#encntrId').val();

        $.ajax({
            url: "/Provider/Order/OrderExists?orderCatalogId=" + orderCatalogId + "&encntrId=" + encntrId,
            dataSrc: "orderExists",
            type: "GET",
            contentType: "json",
            success: function (data) {
                if (data.orderExists != null) {
                    swal.fire({
                        title: 'Are you sure?',
                        html: "<strong style='color:red;'>The item being ordered already exists for this encounter. Do you want to continue placing the order?</strong>",
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, place the order'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $('#orderSubmit').submit();
                            toastr.success(data.message);
                        } else {
                            Swal.fire('Trasaction cancelled.');                        }
                    })
                }
                else {
                    $('#orderSubmit').submit();
                }

            }
        });
    }
});
