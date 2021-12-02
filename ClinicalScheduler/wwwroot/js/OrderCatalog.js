var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/OrderCatalog/GetAll",
            "dataSrc": "orderCatalogList"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "description", "width": "20%" },
            { "data": "codeValue.name", "width": "20%" },
            {
                "data": "orderCatalogList.isDeleted",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked/>`
                    }
                    else {
                        return `<input type="checkbox" disabled/>`
                    }
                },
                "width": "10%",
                "className": "text-center"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Admin/OrderCatalog/Upsert?id=${data}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Delete('/Admin/OrderCatalog/Delete/${data}') class="btn btn-danger small mx-2">
                        <i class="bi bi-trash-fill"></i> Delete</a>
                </div></td>
                    `
                },
                "width":"20%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                    
                }
            })
        }
    })
}
