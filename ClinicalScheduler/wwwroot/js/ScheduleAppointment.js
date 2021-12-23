var startDate;
var endDate;
var apptType;
var myStartDtTmpicker = document.getElementById("startDate");
var schApptId = $('#schApptId').val();

$(document).ready(function () {
    if (schApptId == 0) { changeStartDateTime() };
})

window.onload = function () {

    myStartDtTmpicker.addEventListener("change", function () {
        changeStartDateTime();
    });
}

function changeDateTime(d, h, m) {
    m = (Math.ceil(m / 15) * 15);
    if (m == 0) m = "00";
    if (m == 60) { m = "00"; ++h % 24; }

    var newValue = d + "T" + h + ":" + m;

    validateStartTime(h);

    return newValue;
}

function changeStartDateTime() {
    var [dates, hours, minutes] = myStartDtTmpicker.value.split(/[T:]/);

    myStartDtTmpicker.value = changeDateTime(dates, hours, minutes);

    changeEndDateTime(myStartDtTmpicker.value);
}

function changeEndDateTime(startDate) {
    document.getElementById("endDate").value = startDate

    var myEndDtTmpicker = document.getElementById("endDate");
    var selectedValue = $("#apptType option:selected").text();
    var [dates, hours, minutes] = myEndDtTmpicker.value.split(/[T:]/);

    if (selectedValue.includes("Initial"))
    {
        ++hours % 24;
        hours = pad(parseInt(hours))
    }
    else {
        if (parseInt(minutes) + 30 >= 60) {
            ++hours % 24;
            hours = pad(parseInt(hours))
            minutes = (parseInt(minutes) + 30) - 60;
            if (minutes == 0) {minutes="00"}
        } else { minutes = parseInt(minutes) + 30; }
    }
    
    myEndDtTmpicker.value = dates + "T" + hours + ":" + minutes;
    validateEndTime(h);

}

function validateStartTime(h) {
    if (parseInt(h) < parseInt($('#start').val())) {
        $("#startValid").prop("checked", false);
    } else {
        $("#startValid").prop("checked", true);
    }
}

function validateEndTime(h) {
    if (parseInt(h) < parseInt($('#end').val())) {
        $("#endValid").prop("checked", false);
    } else {
        $("#endValid").prop("checked", true);
    }
}

$("#apptType").change(function () {
    changeStartDateTime();
})

function pad(n) {
    return (n < 10) ? ("0" + n) : n;
}