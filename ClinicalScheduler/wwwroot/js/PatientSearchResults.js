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
                { "data": "id", "width": "20%" },
                { "data": "firstName", "width": "50%" },
                { "data": "lastName", "width": "30%" },

            ]
    });
   
}