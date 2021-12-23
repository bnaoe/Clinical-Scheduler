var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/ApplicationUser/GetAll",
            "dataSrc": "userList"
        },
        "columns": [
            { "data": "result.firstName", "width": "15%" },
            { "data": "result.middleName", "width": "15%" },
            { "data": "result.lastName", "width": "15%" },
            { "data": "result.email", "width": "15%" },
            { "data": "result.location.name", "width": "15%" },
            { "data": "result.role", "width": "15%" },
            {
                "data": {
                    id: "result.id", lockoutEnd: "result.lockoutEnd"
                },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.result.lockoutEnd).getTime();
                    if (lockout > today) {
                        //user is currently locked
                        return `
                            <div class="text-center">
                                <a onclick=LockUnlock('${data.result.id}') class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-lock-open"></i>  Unlock
                                </a>
                            </div>
                           `;
                    }
                    else {
                        return `
                            <div class="text-center">
                                <a onclick=LockUnlock('${data.result.id}') class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-lock"></i>  Lock
                                </a>
                            </div>
                           `;
                    }

                }, "width": "10%"
            }
        ]
    });
}
function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/ApplicationUser/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });

}
