﻿
$("#selectIns").typeahead({
    minLength: 1,
    highlight: true,
    source: function (request, response) {
        $.ajax({
            url: "/Admin/Insurance/GetList/",
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


$("#selectord").typeahead({
    minLength: 1,
    highlight: true,
    source: function (request, response) {
        $.ajax({
            url: "/Admin/Insurance/GetList/",
            dataSrc: "insuranceList",
            data: { "name": request },
            type: "GET",
            contentType: "json",
            success: function (data) {
                if (data.insuranceList.length == 0) {
                    $('#selectord').val('');
                    $('#ord').text('Not Found');
                }
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
        if ($('#selectord').val() == '') {
            $("#updateordid").val('0');
        }
    },
    updater: function (item) {
        //If simultaneously want to update value somewhere else

        $("#updateordid").val(map[item].id);
        return item;
    }
});