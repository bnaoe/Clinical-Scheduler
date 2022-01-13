var dataTable;
var lastName;
var firstName;
var apptDT;
var loc;

var date = new Date();
var day = date.getDate();
var month = date.getMonth() + 1;
var year = date.getFullYear();


if (month < 10) month = "0" + month;
if (day < 10) day = "0" + day;

var today = year + "-" + month + "-" + day;

document.getElementById("apptDT").value = today;

$(document).ready(function () {
    lastName = $('#lastName').val();
    firstName = $('#firstName').val();
    apptDT = $('#apptDT').val();
    loc = $('#loc').val();
    loadDataTable();
})

$('#find').click(function () {
    lastName = $('#lastName').val();
    firstName = $('#firstName').val();
    apptDT = $('#apptDT').val();
    loc = $('#loc').val();
    loadDataTable()
});


function loadDataTable() {
    dataTable =$('#appointmentTbl').DataTable({
        "destroy": true,
        "order": [[6, "asc"]],
        "ajax": {
            "url": "/Shared/Search/GetAllAppointments?locId=" + loc + "&apptDT=" + apptDT + "&firstName=" + firstName + "&lastName=" + lastName,
            "dataSrc": "dashboardApptList"
        },
        "columns": [
            { "data": "result.schApptId", "visible": false },
            { "data": "result.id", "visible": false },
            { "data": "result.patientId", "visible": false },
            { "data": "result.providerScheduleProfileId", "visible": false },
            { "data": "result.fin", "visible": false },
           
            {
                "data": "result.start_date",
                "render": function (data) {
                    var newDate = new Date(data);
                    return moment(newDate).format("MM/DD/YYYY hh:mm A");
                },
                "visible": false
            },
            {
                "data": {
                    start_date: "result.start_date", end_date: "result.end_date"
                },
                "render": function (data) {
                    var sDate = new Date(data.result.start_date)
                    var eDate = new Date(data.result.end_date)
                    return moment(sDate).format("MM/DD/YYYY hh:mm A") + ' - ' + moment(eDate).format("hh:mm A")
                },
                "width": "20%"
            },
            { "data": "result.ptName", "width": "15%" },
            { "data": "result.provName", "width": "15%" },
            { "data": "result.apptType.name", "width": "10%" },
            { "data": "result.apptStatus.name", "width": "10%" },
            {
                "data": {
                    schApptId: "result.schApptId", id: "result.id", patientId: "result.patientId",
                    providerScheduleProfileId: "result.providerScheduleProfileId", name: "result.apptStatus.name"
                },
                "render": function (data) {
                    var apptStatus = data.result.apptStatus.name
                    if (apptStatus == "Confirmed" || apptStatus == "Open") {
                        return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Scheduler/ScheduleAppointment/Upsert?schApptId=${data.result.schApptId}&enctrId=${data.result.id}&patientId=${data.result.patientId}&providerScheduleProfileId=${data.result.providerScheduleProfileId}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Cancel('/Scheduler/ScheduleAppointment/CancelAppt?schApptId=${data.result.schApptId}') class="btn btn-danger small mx-2">
                        <i class="bi bi-x-octagon"></i> Cancel Appointment</a>
                        <a href="/Provider/Chart/EncounterSchAppt?enctrId=${data.result.id}" class="btn btn-info small mx-2">
                        <i class="bi bi-file-earmark-text"></i> Chart</a>
                        </div></td>
                        `
                    } else if (apptStatus == "Admitted") {
                        return `
                        <td><div class="w-100 btn-group" role="group">
                         <a onClick=Cancel('/Scheduler/ScheduleAppointment/CancelAppt?schApptId=${data.result.schApptId}') class="btn btn-danger small mx-2">
                        <i class="bi bi-x-octagon"></i> Cancel Appointment</a>
                        <a onClick=Discharge('/Scheduler/ScheduleAppointment/DischEncounter?schApptId=${data.result.schApptId}&encntrId=${data.result.id}') class="btn btn-warning small mx-2">
                        <i class="bi bi-box-arrow-left"></i> Discharge</a>
                        <a href="/Provider/Chart/EncounterSchAppt?enctrId=${data.result.id}" class="btn btn-info small mx-2">
                        <i class="bi bi-file-earmark-text"></i> Chart</a>
                        </div></td>
                        `
                    }
                    else { return null }
                },
                "width": "20%"
            },

        ]
    })
}

function Cancel(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "This will cancel the appointment permanently",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
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

function Discharge(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Patient appointment will be discharged at same date/time the appointment ends",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
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