var pId;
var pSchedProfileId;
var providerId;
var start;
var end

$(document).ready(function () {

    pId = $('#pId').val();
    pSchedProfileId = $('#pSchedProfileId').val();
    providerId = $('#providerId').val();
    start = $('#start').val();
    end = $('#end').val();
    loadCalendar();
})

function loadCalendar() {
    scheduler.config.readonly = true;
    scheduler.config.limit_time_select = true;
    scheduler.config.first_hour = start;
    scheduler.config.last_hour = end;
    scheduler.config.max_month_events = 3;
    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.init('scheduler_here', new Date(), "week");
    scheduler.load("/Scheduler/ScheduleAppointment/GetProviderAppointments?providerId=" + providerId,"json");

}

