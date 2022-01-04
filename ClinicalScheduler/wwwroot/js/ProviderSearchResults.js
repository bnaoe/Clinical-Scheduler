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
                "url": "/Shared/Search/GetAllProviders?firstName=" + firstName + "&lastName=" + lastName,
                "dataSrc": "providerList"
            },
            "columns": [
                { "data": "result.id", "visible": false },
                { "data": "result.firstName", "width": "20%" },
                { "data": "result.middleName", "width": "20%" },
                { "data": "result.lastName", "width": "20%" },
                { "data": "result.suffix", "Width": "5%"},
                { "data": "result.specialization", "width": "10%" },
                { "data": "result.locName", "width": "10%" },

                {
                    "data": { id: "result.id"},
                    "render": function (data) {
                        return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Scheduler/ProviderScheduleProfile/GetProviderDetails?id=${data.result.id}" class="btn btn-primary small mx-2">
                        <i class="bi bi-person-lines-fill"></i> Profile</a>
                </div></td>
                    `
                    },
                    "width": "15%"
                },
            ]
    });
    
   
}