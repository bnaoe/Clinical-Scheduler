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
    loadDataTable();

});


function loadDataTable() {
   dataTable = $('#appointmentTbl').DataTable({
        "destroy": true,
        "ajax": {
            "url": "/Shared/Search/GetAllAppointments?locId=" + loc + "&apptDT=" + apptDT + "&firstName=" + firstName + "&lastName=" + lastName,
            "dataSrc": "dashboardApptList"
        },
        "columns": [
            { "data": "result.schApptId", "visible": false },
            { "data": "result.id", "visible": false },
            { "data": "result.patientId", "visible": false },
            { "data": "result.providerScheduleProfileId", "visible": false },
            { "data": "result.fin", "width": "10%" },
            {
                "data": "result.start_date",
                "render": function (data) {
                    var newDate = new Date(data);
                    return moment(newDate).format("YYYY-MM-DD hh:mm");
                },
                "width": "10%"
            },

            {
                "data": "result.end_date",
                "render": function (data) {
                    var newDate = new Date(data);
                    return moment(newDate).format("YYYY-MM-DD hh:mm");
                },
                "width": "10%"
            },
            { "data": "result.name", "width": "10%" },
            { "data": "result.firstName", "visible": false },
            {
                "data": "result.lastName",
                "render": function (data, type, row, meta) {
                    return row.result.lastName + ', ' + row.result.firstName
                },
                "width": "10%"
            },
            { "data": "result.apptType.name", "width": "10%" },
            { "data": "result.apptStatus.name", "width": "10%" },
            {
                "data": {
                    schApptId: "result.schApptId", id: "result.id", patientId: "result.patientId",
                    providerScheduleProfileId: "result.providerScheduleProfileId"
                },
                "render": function (data) {
                    return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Scheduler/ScheduleAppointment/Upsert?schApptId=${data.result.schApptId}&enctrId=${data.result.id}&patientId=${data.result.patientId}&providerScheduleProfileId=${data.result.providerScheduleProfileId}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="/Provider/Chart/EncounterSchAppt?enctrId=${data.result.id}" class="btn btn-info small mx-2">
                        <i class="bi bi-file-earmark-text"></i> Chart</a>
                </div></td>
                    `
                },
                "width": "20%"
            },

        ]
    })
}