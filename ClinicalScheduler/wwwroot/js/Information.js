function How() {
    Swal.fire({
        html:
            '<div class="align-left">' +
            '<h3><u>How to Use the Website</u></h3>' +
            '<ul>' +
            '<li>To login choose any account from the list of accounts below.</li>' +
            '<li>Admin users can register new employee and admin accounts.</li>' +
            '<li>Users can add items to the cart and place orders.</li>' +
            '<li>In order to place an order and make payments, you can use any test credit card number supported by stripe.' +
            '<ul>' +
            '<li>Test credit card account 4242 4242 4242 4242, any valid date , 424 CVV.</li>' +
            '</ul>' +
            '<li>Once the order is placed, you can login as the Admin User and Manage orders and see the flow of application.</li>' +
            '<li>Menu definitions</li>' +
            '<ul>' +
            '<li>Content Management &nbsp; - &nbsp; Product, book cover type, & book category management.</li>' +
            '<li>Company &nbsp; - &nbsp; Customer company affliation management.</li>' +
            '<li>User &nbsp; - &nbsp; Customer & user account management.</li>' +
            '<li>Order Management &nbsp; - &nbsp; Customer orders & order list.</li>' +
            '</ul>' +
            '</li>' +

            '</br>' +

            '<h4><u>Login/User Management Overview:</u></h4>' +
            '<ul>' +
            '<li>Please use login credentials below. Each login would have different roles. See role definitions below in parenthesis. \n <i style="color:dodgerblue">(Password: Admin123$) </i>' +
            '<ul>' +
            '<li>Admin User (Has access to everything) &nbsp; - &nbsp; <i style="color:dodgerblue">admin@gmail.com</i></li>' +
            '<li>Employee User (Does not have access to Content Management) &nbsp; - &nbsp; <i style="color:dodgerblue">employee@gmail.com</i></li>' +
            '<li>Individual Customer User (Can only place orders and only has access to its own order list)&nbsp; - &nbsp; <i style="color:dodgerblue">individual@gmail.com</i></li>' +
            '<li>Authorized Company Customer User&nbsp; - &nbsp; <i style="color:dodgerblue">authcompany@gmail.com</i></li>' +
            '<li>Non Authorized Company Customer User&nbsp; - &nbsp; <i style="color:dodgerblue">nonauthcompany@gmail.com</i></li>' +
            '</ul>' +
            '</li>' +
            '<li>To test account creation, please login as the Admin then click on the User Menu to create an account. &nbsp;</li>' +

            '</ul>' +
            '</ul>' +
            'You can use <b>bold text</b>, ' +
            '<a href="//sweetalert2.github.io">links</a> ' +
            'and other HTML tags' +
            '</div>',


        showCloseButton: true,
        showCancelButton: true,
        focusConfirm: false,
        customClass: 'swal-wide',
        confirmButtonText:
            '<i class="fa fa-thumbs-up"></i> Great!',
        confirmButtonAriaLabel: 'Thumbs up, great!',
        cancelButtonText:
            '<i class="fa fa-thumbs-down"></i>',
        cancelButtonAriaLabel: 'Thumbs down'
    });


}