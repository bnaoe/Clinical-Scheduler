var dataTable;
var encntrId = $('#encntrId').val();

$(document).ready(function () {
    loadDataTable();
});



function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "responsive": true,
        "ajax": {
            "url": "/Shared/Search/GetAllDocuments?encntrId=" + encntrId,
            "dataSrc": "encounterDocList"
        },
        "columns": [
            { "data": "result.id", "visible": false },
            {
                "data": "result.createDateTime",
                "render": function (data) {
                    newData = moment(data).format('MM/DD/YYYY h:mm a')
                    return newData;
                },
                "width": "15%"
            },
            /*{
                "data": "result.modifiedDateTime",
                "render": function (data) {
                    newData = moment(data).format('MM/DD/YYYY h:mm a')
                    return newData;
                },
                "width": "15%"
            },*/
            {
                "data": {
                    lastName: "result.lastName", firstName: "result.firstName", middleName: "result.middleName",
                    suffix: "result.suffix"
                },
                "render": function (data) {
                    return data.result.lastName + ', ' + data.result.firstName + ' ' + data.result.middleName + ' ' + data.result.suffix
                },
                "width": "15%"
            },
            { "data": "result.title", "width": "20%" },
            { "data": "result.docType.name", "width": "15%" },
            { "data": "result.docStatus.name", "width": "15%" },
            {
                "data": "result.inError",
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
                "data": {
                    id: "result.id", encounterId: "result.encounterId"
                },
                "render": function (data) {
                    return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Provider/Document/Upsert?docId=${data.result.id}&encntrId=${data.result.encounterId}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a  class="btn btn-danger small mx-2">
                        <i class="bi bi-journal-arrow-up"></i> Delete</a>
                </div></td>
                    `
                },
                "width": "15%"
            }
        ]
    });
}

