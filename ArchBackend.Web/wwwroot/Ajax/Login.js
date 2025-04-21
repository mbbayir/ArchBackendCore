$('#signInForm').submit(function (e) {
    e.preventDefault();

    var formData = $('#signInForm').serialize();
    var token = $('input[name="__RequestVerificationToken"]').val();

    $.ajax({
        type: 'POST',
        url: '/Account/Login',
        data: formData,
        dataType: 'json',
        headers: {
            'RequestVerificationToken': token
        },
        success: function (response) {
            if (!response.success) {
                iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
            } else {
                iziToast.success({ timeout: 1500, icon: 'fas fa-check', title: 'You are being redirected...', message: response.message });
                setTimeout(function () {
                    window.location = response.data;
                }, 1500);
            }
        },
        error: function (xhr, status, error) {
            iziToast.error({ timeout: 1500, icon: 'fas fa-times', title: 'Error!', message: 'An error occurred while processing your request.' });
        }
    });
});
