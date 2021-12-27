
$("#select").typeahead({
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
                    $('#select').val('');
                    $('#ins').text('Not Found');
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
        if ($('#select').val() == '') {
            $("#update").val('0');
        }
    },
    updater: function (item) {
        //If simultaneously want to update value somewhere else

        $("#update").val(map[item].id);
        return item;
    }
});