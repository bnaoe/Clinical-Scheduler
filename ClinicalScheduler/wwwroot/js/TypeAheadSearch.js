
$("#selectIns").typeahead({
    minLength: 1,
    highlight: true,
    source: function (request, response) {
        $.ajax({
            url: "/Shared/Search/GetInsuranceList/",
            dataSrc: "insuranceList",
            data: { "name": request },
            type: "GET",
            contentType: "json",
            success: function (data) {
                items = [];
                map = {};
                $.each(data.insuranceList, function (i, item) {
                    var id = data.insuranceList[i].id;
                    var name = data.insuranceList[i].name;
                    map[name] = { id: id, name: name };
                    items.push(name);

                });
                response(items);
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
        if ($('#selectIns').val() == '') {
            $("#updateInsId").val('0');
        }
    },
    updater: function (item) {
        //If simultaneously want to update value somewhere else

        $("#updateInsId").val(map[item].id);
        return item;
    }
});


$("#selectOrd").typeahead({
    minLength: 1,
    highlight: true,
    source: function (request, response) {
        $.ajax({
            url: "/Shared/Search/GetOrderList/",
            dataSrc: "orderList",
            data: { "name": request },
            type: "GET",
            contentType: "json",
            success: function (data) {
                items = [];
                map = {};
                $.each(data.orderList, function (i, item) {
                    var id = data.orderList[i].id;
                    var name = data.orderList[i].name;
                    map[name] = { id: id, name: name };
                    items.push(name);

                });
                response(items);
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
        if ($('#selectOrd').val() == '') {
            $("#updateOrdId").val('0');
        }
    },
    updater: function (item) {
        //If simultaneously want to update value somewhere else

        $("#updateOrdId").val(map[item].id);
        return item;
    }
});

$("#selectDx").typeahead({
    minLength: 1,
    highlight: true,
    limit: 10,
    source: function (request, response) {
        $.ajax({
            url: "/Shared/Search/GetDxList/",
            dataSrc: "diagnosisList",
            data: { "description": request },
            type: "GET",
            contentType: "json",
            success: function (data) {
                items = [];
                map = {};
                $.each(data.diagnosisList, function (i, item) {
                    var id = data.diagnosisList[i].id;
                    var description = data.diagnosisList[i].description;
                    map[description] = { id: id, description: description };
                    items.push(description);

                });
                response(items);
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
        if ($('#selectDx').val() == '') {
            $("#updateDxId").val('0');
        }
    },
    updater: function (item) {
        //If simultaneously want to update value somewhere else

        $("#updateDxId").val(map[item].id);
        return item;
    }
});