var languageInputValues = {};
var initialLanguageCodes = {};

// Initial language codes'ları topla
function initializeLanguageCodes() {
    $('#LanguageId option').each(function () {
        var languageId = $(this).val();
        var languageCode = $(this).data('language-code');
        if (languageId && languageCode) {
            initialLanguageCodes[languageId] = languageCode;
        }
    });
}

// Input alanlarını oluştur/güncelle
function updateInputFields() {
    var selectedData = $('#LanguageId').select2('data');
    var inputContainer = $('#languageInputs');

    // Mevcut input değerlerini kaydet
    $('.language-code-inputs').each(function () {
        var languageCode = $(this).data('language-code');
        var value = $(this).val();
        if (languageCode && value) {
            languageInputValues[languageCode] = value;
        }
    });

    inputContainer.empty();

    if (selectedData && selectedData.length > 0) {
        selectedData.forEach(function (item) {
            if (!item) return;

            var languageCode = item.languageCode || initialLanguageCodes[item.id];
            if (!languageCode) return;

            var inputValue = languageInputValues[languageCode] || '';

            var inputHTML = `
            <div class="col-md-12 mb-3">
                <div class="form-group">
                    <label for="Name_${languageCode}" class="form-label">Name in <b class="text-danger"> ${languageCode}</b></label>
                    <input type="text" 
                           id="Name_${languageCode}" 
                           name="Name_${languageCode}"
                           data-language-code="${languageCode}" 
                           class="form-control language-code-inputs" 
                           placeholder="Name in ${item.text}" 
                           value="${inputValue}" 
                           required/>
                </div>
            </div>`;
            inputContainer.append(inputHTML);
        });
    }
}

// Initial setup
initializeLanguageCodes();

$("#LanguageId").select2({
    ajax: {
        url: "/Language/GetLanguageSelect",
        dataType: 'json',
        delay: 250,
        data: function (params) {
            var query = params.term || '';
            return {
                q: query,
                page: params.page || 1
            };
        },
        processResults: function (data, params) {
            params.page = params.page || 1;
            
            // Ajax ile gelen her yeni dil için language code'u kaydet
            if (data.items) {
                data.items.forEach(function(item) {
                    if (item.id && item.languageCode) {
                        initialLanguageCodes[item.id] = item.languageCode;
                    }
                });
            }

            return {
                results: $.map(data.items, function (item) {
                    if (item.languageCode != "en") {
                        return {
                            id: item.id,
                            text: item.name,
                            languageCode: item.languageCode
                        };
                    }
                }),
                pagination: {
                    more: ((params.page * 10) < data.total_count)
                }
            };
        },
        cache: true
    },
    theme: "bootstrap-5",
    width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
    placeholder: $(this).data('placeholder'),
    allowClear: true
}).on("change", function (e) {
    updateInputFields();
});

// Input değişikliklerini dinle
$(document).on('input', '.language-code-inputs', function() {
    var languageCode = $(this).data('language-code');
    var value = $(this).val();
    if (languageCode && value !== undefined) {
        languageInputValues[languageCode] = value;
    }
});

// Sayfa yüklendiğinde mevcut seçimleri göster
// $(document).ready(function() {
//     updateInputFields();
// });