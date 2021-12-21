﻿var dataTable;
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
                        <a href="/Scheduler/ScheduleSearch/GetPatientDetails?id=${data}" class="btn btn-success small mx-2">
                        <i class="bi bi-calendar-plus"></i> Schedule</a>
                        <a onClick=GetEncounters(${data}) class="btn btn-info small mx-2">
                        <i class="bi bi-file-earmark-text"></i> Encounter</a>
                </div></td>
                    `
                    },
                    "width": "35%"
                },
            ]
    });
   
}


function encounterTbl_Header() {
    return '<table id="encounterTbl" class="table table-bordered table-striped" style="width:100%">' +
           ' <thead>' +
           '     <tr>' +
           '         <th>schApptId</th>' +
           '         <th>encntrId</th>' +
           '         <th>patientId</th>' +
           '         <th>scheduleProfileId</th>' +
           '         <th>FIN</th>' +
           '         <th>Start Date</th>' +
           '         <th>End Date</th>' +
           '         <th>Location</th>' +
           '         <th>First Name</th>' +
           '         <th>Provider Name</th>' +
           '         <th>Appt. Type</th>' +
           '         <th>Appt. Status</th>' +
           '         <th>Action</th>' +
           '     </tr>' +
           ' </thead>' +
        '</table>'
}

$html = encounterTbl_Header();

function GetEncounters(id) {
    Swal.fire({
        title: 'Encounters',
        heightAuto: false,
        html: $html,
        width: '2000px',
        height: '4000px',
        showCancelButton: true,
        showConfirmButton: false
    });
    $('#encounterTbl').DataTable({
        "destroy": true,
        "ajax": {
            "url": "/Shared/Search/GetAllEncounters?id=" + id,
            "dataSrc": "patientEncounterSchApptList"
        },
        "columns": [
            { "data": "result.schApptId", "visible":false },
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
            { "data": "result.firstName", "visible":false},
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
                "data": "id",
                "render": function (data) {
                    return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="" class="btn btn-info small mx-2">
                        <i class="bi bi-file-earmark-text"></i> chart</a>
                </div></td>
                    `
                },
                "width": "20%"
            },

        ]
    })
};