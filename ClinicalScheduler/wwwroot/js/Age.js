var dob = $('#dob').val();
var admitDate = $('#admitdttm').val();
$(document).ready(function () {
        getAge(dob);
});

function getAge(d) {
    var dtToday;
    d = new Date(d);
    dtToday = new Date();

    $('#age').text(Math.floor((dtToday - d) / (365.25 * 24 * 60 * 60 * 1000)));
}