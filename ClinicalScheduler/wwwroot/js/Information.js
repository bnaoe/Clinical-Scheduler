function How() {
    Swal.fire({
        html:
            '<div class="align-left">' +
            '<h3><u>How to Use the Website</u></h3>' +
            '<ul>' +
            '<li>To login choose any account from the list of accounts below.</li>' +
            '<li>Admin users can register new users and admin accounts.</li>' +
            '<li>Registrar users can resgiter patients, create clincial appointment templates, schedule appointments and only view medical records/charts.</li>' +
            '<li>Clinical users cannot register patients and make appointments, but can modify medical records/charts.</li>' +
            '<li>Menu definitions</li>' +
            '<ul>' +
            '<li>Patient &nbsp; - &nbsp; Patient Registration, Patient Search, Appointment Scheduling, Encounter/Appointment Search, & Chart Access.</li>' +
            '<li>Scheduler Tools &nbsp; - &nbsp; Appointment Dashboard to admit/discharge/cancel appointment, Chart access, & Provider scheduling template build.</li>' +
            '<li>Management Tools &nbsp; - &nbsp; CRUD management for system required default values, locations, insurance, orders, & user account management.</li>' +
            '<p style="color:blue">System default value maintainance CRUD operations were disabled.</p>' +
            '</ul>' +
            '</li>' +

            '</br>' +

            '<h4><u>Login/User Management Overview:</u></h4>' +
            '<ul>' +
            '<li>Please login using credentials below. Each account would have different roles. See role definitions below in parenthesis.' +
            '<ul>' +
            '<li>Admin Users (Has access to everything) &nbsp; - &nbsp; <i style="color:dodgerblue">administration@scheduler.com (Admin Password: Admin123$)</i></li>' +
            '<li><u>For the rest of the accounts below password is &nbsp; - &nbsp; <i style="color:dodgerblue">(User123$)</i></u></li>' +
            '<ul>' +
            '<li>Registration Users (Has access to register patients, scheduling appointments, chart view only) &nbsp; - &nbsp; <i style="color:dodgerblue">registration@scheduler.com</i></li>' +
            '<li>Clinical Users (Physicians, RNs, MAs, & NPs can modify patient charts but cannot register patients)' +
            '<ul>' +
            '<li> <i style="color:dodgerblue">Providermain@scheduler.com</i></li>' +
            '<li> <i style="color:dodgerblue">Providerwest@scheduler.com</i></li>' +
            '<li> <i style="color:dodgerblue">Providersouth@scheduler.com</i></li>' +
            '<li> <i style="color:dodgerblue">rn@scheduler.com</i></li>' +
            '<li> <i style="color:dodgerblue">np@scheduler.com</i></li>' +
            '<li> <i style="color:dodgerblue">ma@scheduler.com</i></li>' +
            '<li> <i style="color:dodgerblue">registration@scheduler.com</i></li>' +
            '</ul>' +
            '</ul>' +
            '</ul>' +
            '<li>To test account creation, please login as administration@scheduler.com then click on the User Access to create an account. &nbsp;</li>' +
            '</ul>' +
            '<h4><u> Test Patients with appointments:</u></h4>' +
            '<ul>' +
            '<li>  <i style="color:dodgerblue">Anne Thomas</i></li>' +
            '<li>  <i style="color:dodgerblue">John Thomas</i></li>' +
            '<li>  <i style="color:dodgerblue">or search for any patients with last name zzTest</i></li>' +
            '</ul>' +
            '<h4><u>Clinical Locations:</u></h4>' +
            '<ul>' +
            '<li>  <i style="color:dodgerblue">Main Medical Center</i></li>' +
            '<li>  <i style="color:dodgerblue">West Medical Center</i></li>' +
            '<li>  <i style="color:dodgerblue">South Medical Center</i></li>' +
            '</ul>' +
            '<h4><u>Physicians to use for scheduling</u></h4>' +
            '<ul>' +
            '<li>  <i style="color:dodgerblue">First Name: Physician Last Name: Main Default Clinic Loscation: Main Medical Center</i></li>' +
            '<li>  <i style="color:dodgerblue">First Name: Physician Last Name: West Default Clinic Location: West Medcial Center</i></li>' +
            '<li>  <i style="color:dodgerblue">First Name: Physician Last Name: South Default Clinic Location: South Medical Center</i></li>' +
            '</ul>' +
            '<h4><u>Clinical orders examples</u></h4>' +
            '<ul>' +
            '<li>  <i style="color:dodgerblue">CMP</i></li>' +
            '<li>  <i style="color:dodgerblue">BMP</i></li>' +
            '<li>  <i style="color:dodgerblue">Urine Test</i></li>' +
            '<li>  <i style="color:dodgerblue">Acetaminophen</i></li>' +

            '</ul>' +

            '</ul>' +
            '</ul>' +
            '</div>',


        showCloseButton: true,
        showCancelButton: true,
        focusConfirm: false,
        customClass: 'swal-wide',
        confirmButtonText:
            '<i class="bi bi-hand-thumbs-up"></i>',
        confirmButtonAriaLabel: 'Thumbs up, great!',
        cancelButtonText:
            '<i class="bi bi-hand-thumbs-down"></i>',
        cancelButtonAriaLabel: 'Thumbs down'
    });


}


function Contact() {
    Swal.fire({
        html:
            '<div class="align-left">' +
            '<h3><u>How to get in touch with me</u></h3>' +
            '</br>' +
            '<h4>Developer: Brian Naoe</h4>' +
            '<a href="mailto:mybpn.projects@gmail.com = Feedback&body = Message">' +
                'Send feedback to: Mybpn.Projects@gmail.com'+
            '</a>' +
            '</div>' +
            '<div class="align-left">' +
            '<a href="https://www.linkedin.com/in/brian-paulo-naoe">' +
            'My Linkedin profile' +
            '</a>' +
            '</div>' +
            '<div class="align-left">' +
            '</br>' +
            '</br>' +
            '<h4><u>Github repositories</u></h4>' + 
            '<a href="https://github.com/bnaoe">' +
            'Github profile' +
            '</a>' +
            '</br>' +
            '<h4><u>Github repository for this project</u></h4>' +
            '<a href="https://github.com/bnaoe/Clinical-Scheduler">' +
            'Github EMR Clinical Scheduler' +
            '</a>' +
            
            '</div>',
        showCloseButton: true,
        showCancelButton: true,
        focusConfirm: false,
        customClass: 'swal-wide',
        confirmButtonText:
            '<i class="bi bi-hand-thumbs-up"></i>',
        confirmButtonAriaLabel: 'Thumbs up, great!',
        cancelButtonText:
            '<i class="bi bi-hand-thumbs-down"></i>',
        cancelButtonAriaLabel: 'Thumbs down'
    });


}