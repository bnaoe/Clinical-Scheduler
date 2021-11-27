var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/CodeValue/GetAll",
            "dataSrc": "codeValueList"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "codeSet.name", "width": "15%" },
            { "data": "name", "width": "15%" },
            { "data": "description", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Admin/CodeValue/Upsert?id=${data}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Delete('/Admin/CodeValue/Delete/${data}') class="btn btn-danger small mx-2">
                        <i class="bi bi-trash-fill"></i> Delete</a>
                </div></td>
                    `
                },
                "width":"15%"
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
