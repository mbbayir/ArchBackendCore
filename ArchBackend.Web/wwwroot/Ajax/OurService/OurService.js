$(document).ready(function () {
    // Load services and display them in the table
    function loadOurServices() {
        $.ajax({
            type: "GET",
            url: "/Services/GetServices",
            success: function (ourservices) {
                let tableBody = $('#ourservicetable');
                tableBody.empty();

                // Append each service to the table
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

    loadOurServices(); // Initial load of services

    // Add new service
    $('#ourserviceForm').submit(function (e) {
        e.preventDefault();

        var selectedCategoryId = parseInt($('#ourServiceCategory').val());

        var ourservice = {
            Name: $('#ourserviceName').val(),
            OurServiceCategories: [
                { categoryId: selectedCategoryId }
            ]
        };
        $.ajax({
            url: '/Services/AddOurServiceAsync',
            type: 'POST',
            data: JSON.stringify(ourservice),
            contentType: 'application/json',
            success: function (response) {
                console.log('Add Service successful', response);
                location.reload(); // Refresh the page to show updated list
            },
            error: function (xhr) {
                console.error('Error:', xhr.responseText);
            }
        });
    });

    // Delete service
    $(document).on('click', '.delete-btn', function () {
        var ourServiceId = $(this).data('id'); // Get the service ID from the button
    
        if (confirm('Are you sure you want to delete this service?')) {
            $.ajax({
                url: '/Services/DeleteOurServiceAsync/' + ourServiceId, // Append ID to the URL
                type: 'DELETE', // Use DELETE HTTP method
                success: function (response) {
                    console.log('Deleted Our Service', response);
                    loadOurServices(); // Reload the list of services
                },
                error: function (xhr) {
                    console.log('Error:', xhr.responseText);
                }
            });
        }
    });
    
});

// Update service

$(document).on('click', '.edit-btn', function () {
    var ourServiceId = $(this).data('id'); // Get the service ID from the button
    $.ajax({
        url: '/Services/DeleteOurServiceAsync/' + ourServiceId,
        type: 'GET',
        success: function (ourservice) {
            $('#updateOurServiceId').val(ourservice.id);
            $('#updateOurServiceName').val(ourservice.name);
            $('#updateOurServiceModal').modal('show'); // Show the modal
        },
        error: function (xhr) {
            console.log('Error:', xhr.responseText);
        }
    });
}
);