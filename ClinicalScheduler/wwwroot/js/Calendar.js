var pId;
var pSchedProfileId;
var providerId;
var start;
var end
var blockedSched = new Array();

$(document).ready(function () {

    pId = $('#pId').val();
    pSchedProfileId = $('#pSchedProfileId').val();
    providerId = $('#providerId').val();
    start = $('#start').val();
    end = $('#end').val();

    if (!$('#sun').prop('checked')) {
        blockedSched.push(0)
    }
    if (!$('#mon').prop('checked')) {
        blockedSched.push(1)
    }
    if (!$('#tue').prop('checked')) {
        blockedSched.push(2)
    }
    if (!$('#wed').prop('checked')) {
        blockedSched.push(3)
    }
    if (!$('#thu').prop('checked')) {
        blockedSched.push(4)
    }
    if (!$('#fri').prop('checked')) {
        blockedSched.push(5)
    }
    if (!$('#sat').prop('checked')) {
        blockedSched.push(6)
    }
    loadCalendar();
})

function loadCalendar() {
       
        var format = scheduler.date.date_to_str("%H:%i");

        // default background image is 44px height, set hour size in order to align the timescale with the background
        scheduler.config.hour_size_px = 88; 
        scheduler.templates.hour_scale = function (date) {
            var step = 15;
            var html = "";
            for (var i = 0; i < 60 / step; i++) {
                html += "<div style='height:22px;line-height:22px;'>" + format(date) + "</div>";
                date = scheduler.date.add(date, step, "minute");
            }
            return html;
        }
        
        scheduler.config.readonly = true;
        scheduler.config.limit_time_select = true;
        scheduler.config.first_hour = start;
        scheduler.config.last_hour = end;
        scheduler.config.max_month_events = 3;


        scheduler.config.xml_date = "%Y-%m-%d %H:%i";
        scheduler.init('scheduler_here', new Date(), "week");
        scheduler.load("/Scheduler/ScheduleAppointment/GetProviderAppointments?providerId=" + providerId, "json");
    //marks and blocks dates
    // Setting up holidays
        scheduler.addMarkedTimespan({
        days: blockedSched,
        zones: "fullday",       // marks the entire day
        type: "dhx_time_block",
        css: "blocked" // the name of applied CSS class
    });
    scheduler.updateView();
};


