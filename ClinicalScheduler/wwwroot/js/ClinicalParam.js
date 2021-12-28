var dob = $('#dob').val();
var admitDate = $('#admitdttm').val();
$(document).ready(function () {
    if ($('#age').val() == '0') {
        getAge(dob);
    }
    showbmi();
});

$(function () {
    $('#systolic').change(function () {
        if ($('#systolic').val() >= 121) {
            $("#systolic").css("color", "Red");
        }
        else {  $("#systolic").css("color", "Black"); }
    }).triggerHandler('change');
});

$(function () {
    $('#diastolic').change(function () {
        if ($('#diastolic').val() >= 81) {
            $("#diastolic").css("color", "Red");
        }
        else { $("#diastolic").css("color", "Black"); }
    }).triggerHandler('change');
});

$(function () {
    $('#pulse').change(function () {
        if ($('#pulse').val() <= 59 || $('#pulse').val() >= 101) {
            $("#pulse").css("color", "Red");
        }
        else { $("#pulse").css("color", "Black"); }
    }).triggerHandler('change');
});

$(function () {
    $('#O2').change(function () {
        if ($('#O2').val() <= 94) {
            $("#O2").css("color", "Red");
        }
        else { $("#O2").css("color", "Black"); }
    }).triggerHandler('change');
});

$(function () {
    $('#temp').change(function () {
        if ($('#temp').val() >= 100.5) {
            $("#temp").css("color", "Red");
        }
        else { $("#temp").css("color", "Black"); }
    }).triggerHandler('change');
});

$(function () {
    $('#heightFt').change(function () {
        showbmi();
    }).triggerHandler('change');
});

$(function () {
    $('#heightIn').change(function () {
        showbmi();
    }).triggerHandler('change');
});

$(function () {
    $('#weight').change(function () {
        showbmi();
    }).triggerHandler('change');
});
function getAge(d) {
    var admitdttm;
    d = new Date(d);
    if (admitdttm != '') {
        admitdttm = new Date(admitDate);
    } else {
        admitdttm = new Date();
    }
    $('#age').val(Math.floor((admitdttm - d) / (365.25 * 24 * 60 * 60 * 1000)));
}

function convertHeight(ft) {
    return ft * 12;
}


    // calculate total height
function showbmi() {
    var weight = $('#weight').val() * 1;
    var height_ft = $('#heightFt').val() * 1;
    var height_in = $('#heightIn').val() * 1;
    var height = convertHeight(height_ft) + height_in;
    var female_h = (height * height) / 30;
    var male_h = (height * height) / 28;
    //var gender = $('input[name="gender"]:checked').val();
    var bmi = "?";
    bmi = (Math.round((weight * 703) / (height * height)));

    /*if (gender == "female") {
        bmi = (Math.round((weight * 703) / (height * height)));
        if (isNaN(bmi)) bmi = "?";
    } else {
        bmi = (Math.round((weight * 703) / (height * height)));
        if (isNaN(bmi)) bmi = "?";
    }*/

    var bmi_msg = "?";
    if (bmi < 15) {

        bmi_msg = "Very severely underweight";

    } else if (bmi <= 16) {
        bmi_msg = "Severely underweight";

    } else if (bmi <= 18.4) {

        bmi_msg = "Underweight";

    } else if (bmi <= 24.9) {
        bmi_msg = "Normal";

    } else if (bmi <= 29.9) {
        bmi_msg = "Overweight";

    } else if (bmi <= 34.9) {
        bmi_msg = "Obese Class I (Moderately obese)";

    } else if (bmi <= 39.9) {
        bmi_msg = "Obese Class II (Severely obese)";

    } else if (bmi="?") {
        bmi_msg = "";

    }
    $("#bmi").val(bmi);
    $("#bmiresult").text(bmi_msg);
    return bmi;

}
//bmi infos