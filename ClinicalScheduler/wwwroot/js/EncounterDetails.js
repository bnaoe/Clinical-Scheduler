var dataTable;
var encntrId = $('#encntrId').val();
var patientId = $('#patientId').val();

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
            { "data": "result.orderDetails", "width": "20%"},
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
                "data": "result.isActive",
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
                    id: "result.id", encounterId: "result.encounterId", orderStatusName: "result.orderStatusName"
                },
                "render": function (data) {
                    var orderStatus = data.result.orderStatusName;
                    if (orderStatus == "Ordered" || orderStatus=="On-hold") {
                        return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Provider/Order/Upsert?orderId=${data.result.id}&encntrId=${data.result.encounterId}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        </div></td>
                            `;
                    } else {
                        return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Provider/Order/Preview?orderId=${data.result.id}&encntrId=${data.result.encounterId}" class="btn btn-success small mx-2">
                        <i class="bi bi-file-earmark-pdf"></i> Preview</a>
                        </div></td>
                        `;
                    }
                },
                "width": "5%"
            }
        ]
    });
    dataTable = $('#tblPatientPrescriptions').DataTable({
        "responsive": true,
        "ajax": {
            "url": "/Shared/Search/GetAllPrescriptions?patientId=" + patientId,
            "dataSrc": "prescriptionList"
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
                "data": "result.isActive",
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
            }
        ]
    });
    encntrdxdataTable = $('#tblEnctrDiagnosisData').DataTable({
        "responsive": false,
        "ajax": {
            "url": "/Shared/Search/GetAllDiagnosisEncntr?encntrId=" + encntrId,
            "dataSrc": "encounterDiagnosisList"
        },
        "columns": [
            {
                "className": 'dt-control',
                "data": null,
                "defaultContent": ''
            },
            { "data": "result.id", "visible": false },
            { "data": "result.description", "wdith": "75%" },
            {
                "data": "result.activeDtTm",
                "render": function (data) {
                    newData = moment(data).format('MM/DD/YYYY h:mm a')
                    return newData;
                },
                "width": "15%"
            },
            {
                "data": "result.isActive",
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
                    id: "result.id", encounterId: "result.encounterId", isActive: "result.isActive"
                },
                "render": function (data) {
                    var isActive = data.result.isActive;
                    if (isActive == true) {
                        return `
                        <td><div class="w-100 btn-group" role="group">
                        <a href="/Provider/Diagnosis/Upsert?diagnosisId=${data.result.id}&encntrId=${data.result.encounterId}" class="btn btn-primary small mx-2">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                        </div></td>
                            `;
                    } else {
                        return null;
                    }
                },
                "width": "5%"
            }
        ]
    });
    patientdxdataTable = $('#tblPatientDiagnosisData').DataTable({
        scrollY: '50vh',
        scrollCollapse: false,
        "responsive": true,
        "ajax": {
            "url": "/Shared/Search/GetAllDiagnosisPatient?patientId=" + patientId,
            "dataSrc": "patientDiagnosisList"
        },
        "columns": [
            {
                "className": 'dt-control',
                "data": null,
                "defaultContent": ''
            },
            { "data": "result.id", "visible": false },
            { "data": "result.description", "wdith": "75%" },
            {
                "data": "result.activeDtTm",
                "render": function (data) {
                    newData = moment(data).format('MM/DD/YYYY h:mm a')
                    return newData;
                },
                "width": "15%"
            },
            {
                "data": "result.isActive",
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
            }
        ]
    });

    // FOR OPENING AND CLOSING THE ACCORDION
    $('#tblEnctrDiagnosisData tbody').on('click', 'td.dt-control', function () {
        var tr = $(this).closest('tr');
        var row = encntrdxdataTable.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            $('div.slider', row.child()).slideUp(function () {
                row.child.hide();
                tr.removeClass('shown');
            });
        }
        else {
            // Open this row
            
            row.child(format(row.data()), 'no-padding').show();
            tr.addClass('shown');

            $('div.slider', row.child()).slideDown();
        }
    });

    // FOR OPENING AND CLOSING THE ACCORDION
    $('#tblPatientDiagnosisData tbody').on('click', 'td.dt-control', function () {
        var tr = $(this).closest('tr');
        var row = patientdxdataTable.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            $('div.slider', row.child()).slideUp(function () {
                row.child.hide();
                tr.removeClass('shown');
            });
        }
        else {
            // Open this row

            row.child(format(row.data()), 'no-padding').show();
            tr.addClass('shown');

            $('div.slider', row.child()).slideDown();
        }
    });

    // Handle click on "Expand All" button
    $('#btn-show-all-children').on('click', function () {
        // Enumerate all rows
        dataTable.rows().every(function () {
            // If row has details collapsed
            if (!this.child.isShown()) {
                // Open this row
                this.child(format(this.data())).show();
                $(this.node()).addClass('shown');
            }
        });
    });

    // Handle click on "Collapse All" button
    $('#btn-hide-all-children').on('click', function () {
        // Enumerate all rows
        dataTable.rows().every(function () {
            // If row has details expanded
            if (this.child.isShown()) {
                // Collapse row details
                this.child.hide();
                $(this.node()).removeClass('shown');
            }
        });
    });

}


/* Formatting function for row details - modify as you need */
function format(d) {
    return '<div class="slider">' +
        '<table class="cell-border" cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
        '<tr>' +
        '<td>Provider Name:</td>' +
        '<td>' + d.result.lastName + ', ' + d.result.firstName + ' ' + d.result.middleName + ' ' + d.result.suffix + '</td>' +
        '</tr>' +
        '<tr>' +
        '<td>Diagnosis added from Enctr:</td>' +
        '<td>' + d.result.fin + '</td>' +
        '</tr>' +
        '<tr>' +
        '<td>End Date/Time:</td>' +
        '<td>' + moment(d.result.endDtTm).format('MM/DD/YYYY h:mm a') + '</td>' +
        '</tr>' +
        '<tr>' +
        '<td>Extra info:</td>' +
        '<td> End date and time will be null if diagnosis is active.</td>' +
        '</tr>' +
        '</table>'
        '</div>';
}

document.getElementById('last_doc').onclick = function last_doc () {
    this.__toggle = !this.__toggle;
    var target = document.getElementById('hidden_lastdoc');
    if (this.__toggle) {
        target.style.height = target.scrollHeight + "px";
        this.firstChild.nodeValue = "Hide Last Note";

        if ($('#hidden_dx').outerHeight() != 0) {
            $('#patientDx').trigger('click');
        }
        if ($('#hidden_pres').outerHeight() != 0) {
            $('#prescriptions').trigger('click');
        }
    }   
    else {
        target.style.height = 0;
        this.firstChild.nodeValue = "Show Last Note";
    }
}

document.getElementById('patientDx').onclick = function () {
    this.__toggle = !this.__toggle;
    var target = document.getElementById('hidden_dx');
    if (this.__toggle) {
        target.style.height = target.scrollHeight + "px";
        this.firstChild.nodeValue = "Hide Complete Diagnosis";

        if ($('#hidden_pres').outerHeight() != 0) {
            $('#prescriptions').trigger('click');
        }
        if ($('#hidden_lastdoc').outerHeight() > 20) {
            var h = $('#hidden_lastdoc').outerHeight();
            var c = h;
            $('#last_doc').trigger('click');
        }
    }
    else {
        target.style.height = 0;
        this.firstChild.nodeValue = "Show Complete Diagnosis";
    }
}

document.getElementById('prescriptions').onclick = function () {
    this.__toggle = !this.__toggle;
    var target = document.getElementById('hidden_pres');
    if (this.__toggle) {
        target.style.height = target.scrollHeight + "px";
        this.firstChild.nodeValue = "Hide Prescriptions";
        if ($('#hidden_dx').outerHeight() != 0) {
            $('#patientDx').trigger('click');
        }
        if ($('#hidden_lastdoc').outerHeight() > 20) {
            var h = $('#hidden_lastdoc').outerHeight();
            var c = h;
            $('#last_doc').trigger('click');
        }
    }
    else {
        target.style.height = 0;
        this.firstChild.nodeValue = "Show Prescriptions";
    }
}


