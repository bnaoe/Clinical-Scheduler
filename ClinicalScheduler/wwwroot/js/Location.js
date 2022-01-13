var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "order": [[0, "asc"]],
        "ajax": {
            "url": "/Admin/Location/GetAll",
            "dataSrc": "locationList"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "street", "width": "50%" },
            { "data": "city", "width": "10%" },
            { "data": "state", "width": "5%" },
            { "data": "zip", "width": "5%" },
            {
                "data": "isDeleted",
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
                        <a href="/Admin/Location/Upsert?id=${data}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Delete('/Admin/Location/Delete?id=${data}') class="btn btn-danger small mx-2">
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
        text: "The record will not be deleted from the database but will be inactivated and can be reversed.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, inactivate it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: url,
                data: { '__RequestVerificationToken': $('[name=__RequestVerificationToken]').val() },
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
