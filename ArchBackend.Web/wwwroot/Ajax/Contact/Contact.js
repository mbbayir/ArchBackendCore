//$('#contactForm').submit(function (e) {
//    e.preventDefault();

//    var formData = $(this).serialize();

//    $.ajax({
//        type: 'POST',
//        url: '/Contact/Contact',
//        data: formData,
//        success: function () {
//            $('#resultMessage').text('Thank you! Your message has been sent.').css('color', 'green');
//            $('#contactForm')[0].reset();
//        },
//        error: function () {
//            $('#resultMessage').text('An error occurred. Please try again.').css('color', 'red');
//        }
//    });
//});
$(document).ready(function () {
    $('#contactForm').submit(function (e) {
        e.preventDefault(); 

        var formData = $(this).serialize(); 

        $.ajax({
            url: $(this).attr('action'), 
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('.contact__msg').show().text('Your message was sent successfully.');
                    $('#contactForm')[0].reset(); 
                } else {
                    alert('There was an error sending your message. Please try again.');
                }
            },
            error: function () {
                alert('Error occurred. Please try again.');
            }
        });
    });
});