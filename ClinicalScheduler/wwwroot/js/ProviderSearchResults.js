var dataTable;
var lastName;
var firstName;
var birthDate;


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
                { "data": "id", "visible": false },
                { "data": "firstName", "width": "20%" },
                { "data": "middleName", "width": "20%" },
                { "data": "lastName", "width": "20%" },
                { "data": "suffix", "Width": "5%"},
                { "data": "specialization", "width": "10%" },
                { "data": "location.name", "width": "10%" },

                {
                    "data": "id",
                    "render": function (data) {
                        return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Scheduler/ProviderScheduleProfile/GetProviderDetails?id=${data}" class="btn btn-primary small mx-2">
                        <i class="bi bi-person-lines-fill"></i> Profile</a>
                </div></td>
                    `
                    },
                    "width": "15%"
                },
            ]
    });
    
   
}