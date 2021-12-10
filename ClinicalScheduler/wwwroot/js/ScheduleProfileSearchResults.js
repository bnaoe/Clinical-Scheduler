var dataTable;
var loc;
var lastName;
var firstName;

$(document).ready(function () {
    loadDataTable();
})

$('#find').click(function () {
    lastName = $('#lastName').val();
    firstName = $('#firstName').val();
    loc = $('#loc').val();
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
            "destroy":true,
            "ajax": {
                "url": "/Scheduler/ScheduleSearch/GetAllScheduleProfiles?location=" + loc + "&firstName=" + firstName + "&lastName=" + lastName,
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
                { "data": "applicationUser.firstName", "width": "15%" },
                { "data": "applicationUser.lastName", "width": "15%" },
                { "data": "applicationUser.specialization", "width": "20%" }
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