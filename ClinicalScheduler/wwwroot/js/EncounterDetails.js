var dataTable;
var encntrId = $('#encntrId').val();

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblDocumentData').DataTable({
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
                        <a  class="btn btn-success small mx-2">
                        <i class="bi bi-file-earmark-pdf"></i> Preview</a>
                </div></td>
                    `
                },
                "width": "15%"
            }
        ]
    });
    dataTable = $('#tblOrderData').DataTable({
        "responsive": true,
        "ajax": {
            "url": "/Shared/Search/GetAllOrders?encntrId=" + encntrId,
            "dataSrc": "encounterOrderList"
        },
        "columns": [
            { "data": "result.id", "visible": false },
            { "data": "result.orderTypeName", "width": "10%" },
            {
                "data": "result.orderingDtTm",
                "render": function (data) {
                    newData = moment(data).format('MM/DD/YYYY h:mm a')
                    return newData;
                },
                "width": "15%"
            },
            { "data": "result.name", "width": "20%" },
            { "data": "result.orderDetails", "width": "20%" },
            {
                "data": {
                    lastName: "result.lastName", firstName: "result.firstName", middleName: "result.middleName",
                    suffix: "result.suffix"
                },
                "render": function (data) {
                    return data.result.lastName + ', ' + data.result.firstName + ' ' + data.result.middleName + ' ' + data.result.suffix
                },
                "width": "20%"
            },
            { "data": "result.orderStatusName", "width": "10%" },
            {
                "data": "result.inActive",
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
                        <a href="/Provider/Order/Upsert?orderId=${data.result.id}&encntrId=${data.result.encounterId}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        <a  class="btn btn-success small mx-2">
                        <i class="bi bi-file-earmark-pdf"></i> Preview</a>
                </div></td>
                    `
                },
                "width": "5%"
            }
        ]
    });

}

document.getElementById('last_doc').onclick = function () {
    this.__toggle = !this.__toggle;
    var target = document.getElementById('hidden_lastdoc');
    if (this.__toggle) {
        target.style.height = target.scrollHeight + "px";
        this.firstChild.nodeValue = "Hide last document";
    }
    else {
        target.style.height = 0;
        this.firstChild.nodeValue = "Show last document";
    }
}

