function translationFormData(id) {
    var translationsData = [];
    $('input.language-code-inputs').each(function () {
        var translation = {
            Id: id,
            LanguageCode: $(this).data('language-code'),
            Name: $(this).val(),
        };
        translationsData.push(translation);
    });

    $.ajax({
        type: "POST",
        url: "/Category/Translation",
        data: JSON.stringify(translationsData),
        contentType: "application/json",
        success: function (response) {
            if (response.success) {
                console.log("Çeviri:" + response.message);
                window.location.href = "/Category/Index";
            }
            else {
                iziToast.warning({ timeout: 1500, title: 'Error!', message: response.message });
            }
        }
    });
}