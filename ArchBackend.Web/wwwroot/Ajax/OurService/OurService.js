
$(document).ready(function () {
    function loadOurServices() {
        $.ajax({
            type: "GET",
            url: "/Services/GetServices",
            success: function (ourservices) {
                let tableBody = $('#ourservicetable');
                tableBody.empty();

                ourservices.forEach(function (ourservice) {
                    var row = `
                        <tr>
                            <td>${ourservice.name}</td>
                            <td>
                                <button class="btn btn-sm btn-warning edit-btn" data-id="${ourservice.id}">Edit</button>
                                <button class="btn btn-sm btn-danger delete-btn" data-id="${ourservice.id}">Delete</button>
                            </td>
                        </tr>`;
                    tableBody.append(row);
                });
            },
            error: function (xhr) {
                console.error("Error Fetching Process", xhr.responseText);
            }
        });
    }


    loadOurServices();

        $('#ourserviceForm').submit(function (event) {
            event.preventDefault();

            var ourservice = {
                Name: $('#ourserviceName').val()
            };

            $.ajax({
                type: 'POST',
                url: '/Services/AddOurServiceAsync',
                contentType: 'application/json',
                data: JSON.stringify(ourservice),
                success: function (response) {
                    alert('OurService Added Successfully!');
                    $('#ourserviceForm')[0].reset();
                    window.location.href='/Services/Index'
                },
                error: function (xhr) {
                    console.log('Error:', xhr.responseText);
                    alert('Failed to add OurService.');
                }
            });
        });

});


