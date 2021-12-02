var dataTable;
var lastName;
var firstName;
var birthDate;


$(document).ready(function () {
   
    loadDataTable();

})

$('#find').click(function () {
    lastName = $('#lastName').val();
    firstName = $('#firstName').val();
    birthDate = $('#birthDate').val();
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
            "destroy":true,
            "ajax": {
                "url": "/Shared/Search/GetAllPatients?firstName=" + firstName + "&lastName=" + lastName+ "&birthDate=" + birthDate,
                "dataSrc": "patientList"
            },
            "columns": [
                { "data": "id", "visible": false },
                { "data": "firstName", "width": "20%" },
                { "data": "lastName", "width": "20%" },
                {
                    "data": "birthDate",
                    "render": function (data) {
                        var newDate = new Date(data);
                        return moment(newDate).format("YYYY-MM-DD");
                    },
                    "width": "15%"
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
                    "width": "10%",
                    "className": "text-center"
                },
                {
                    "data": "id",
                    "render": function (data) {
                        return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Scheduler/Patient/Upsert?id=${data}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="" class="btn btn-success small mx-2">
                        <i class="bi bi-calendar-plus"></i> Schedule</a>
                        <a href="" class="btn btn-info small mx-2">
                        <i class="bi bi-file-earmark-text"></i> Chart</a>
                </div></td>
                    `
                    },
                    "width": "35%"
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