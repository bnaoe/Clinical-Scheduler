var dataTable;
var userId;

$(document).ready(function () {
    userId = $('#userId').val();
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
            "destroy":true,
            "ajax": {
            "url": "/Scheduler/ProviderScheduleProfile/GetAll?id=" + userId,
                "dataSrc": "providerScheduleProfileList"
            },
            "columns": [
                { "data": "id", "visible": false },
                { "data": "location.name", "width": "20%" },
                {
                    "data": "startTime",
                    "render": function (data) {
                        newData = moment(data,["h:mm A"]).format('LT')
                        return newData;
                    },
                    "width": "5%"
                },
                {
                    "data": "endTime",
                    "render": function (data) {
                        newData = moment(data, ["h:mm A"]).format('LT')
                        return newData;
                    },
                    "width": "5%"
                },
                {
                    "data": "monday",
                    "render": function (data) {
                        if (data) {
                            return `<input type="checkbox" disabled checked/>`
                        }
                        else {
                            return `<input type="checkbox" disabled/>`
                        }
                    },
                    "Width": "5%",
                    "className": "text-center"
                },
                {
                    "data": "tuesday",
                    "render": function (data) {
                        if (data) {
                            return `<input type="checkbox" disabled checked/>`
                        }
                        else {
                            return `<input type="checkbox" disabled/>`
                        }
                    },
                    "Width": "5%",
                    "className": "text-center"
                },
                {
                    "data": "wednesday",
                    "render": function (data) {
                        if (data) {
                            return `<input type="checkbox" disabled checked/>`
                        }
                        else {
                            return `<input type="checkbox" disabled/>`
                        }
                    },
                    "Width": "10%",
                    "className": "text-center"
                },
                {
                    "data": "thursday",
                    "render": function (data) {
                        if (data) {
                            return `<input type="checkbox" disabled checked/>`
                        }
                        else {
                            return `<input type="checkbox" disabled/>`
                        }
                    },
                    "Width": "5%",
                    "className": "text-center"
                },
                {
                    "data": "friday",
                    "render": function (data) {
                        if (data) {
                            return `<input type="checkbox" disabled checked/>`
                        }
                        else {
                            return `<input type="checkbox" disabled/>`
                        }
                    },
                    "Width": "5%",
                    "className": "text-center"
                },
                {
                    "data": "saturday",
                    "render": function (data) {
                        if (data) {
                            return `<input type="checkbox" disabled checked/>`
                        }
                        else {
                            return `<input type="checkbox" disabled/>`
                        }
                    },
                    "Width": "5%",
                    "className": "text-center"
                },
                {
                    "data": "sunday",
                    "render": function (data) {
                        if (data) {
                            return `<input type="checkbox" disabled checked/>`
                        }
                        else {
                            return `<input type="checkbox" disabled/>`
                        }
                    },
                    "Width": "5%",
                    "className": "text-center"
                },
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
                    "width": "5%",
                    "className": "text-center"
                },

                {
                    "data": "id",
                    "render": function (data) {
                        return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Scheduler/ProviderScheduleProfile/Upsert?id=${data}&userId=${userId}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Delete('/Scheduler/ProviderScheduleProfile/Delete/${data}') class="btn btn-danger small mx-2">
                        <i class="bi bi-trash-fill"></i> Delete</a>
                </div></td>
                    `
                    },
                    "width": "25%"
                },
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