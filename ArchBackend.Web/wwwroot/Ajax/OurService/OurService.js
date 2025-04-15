$(document).ready(function () {
    function loadOurServices() {
        $.ajax({
            type: "GET",
            url: "/Services/GetOurServices",
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


    // Add new service
    $('#ourserviceForm').submit(function (e) {
        e.preventDefault();

        var ourservice = {
            Name: $('#ourserviceName').val(),
        };
        $.ajax({
            url: '/Services/AddOurServiceAsync',
            type: 'POST',
            data: JSON.stringify(ourservice),
            contentType: 'application/json',
            success: function (response) {
                console.log('Add Service successful', response);
                alert('Add OurService Succesfull'),
                    location.reload();
            },
            error: function (xhr) {
                console.error('Error:', xhr.responseText);
            }
        });
    });


    //Delete Service
    $(document).on('click', '.delete-btn', function () {
        var ourServiceId = $(this).data('id');

        if (confirm('Are you sure you want to delete this service?')) {
            $.ajax({
                url: '/Services/DeleteOurServiceAsync/' + ourServiceId,
                type: 'DELETE',
                success: function (response) {
                    alert('Deleted Our Service');
                    location.reload();

                },
                error: function (xhr) {
                    console.log('Error:', xhr.responseText);
                    alert('Error Deleting Our Service: ' + xhr.responseText);
                    location.reload();
                },
            });
        }
    });



    //Update Service

    $(document).on('click', '.edit-btn', function () {
        var ourServiceId = $(this).data('id');
        $.ajax({
            type: 'GET',
            url: '/Services/GetOurServiceById/' + ourServiceId,
            success: function (ourservice) {
                $('#updateOurServiceId').val(ourservice.id);
                $('#updateOurServiceName').val(ourservice.name);
                $('#updateOurServiceModal').modal('show');
            },
            error: function (xhr) {
                console.error("Error Fetching Our Service", xhr.responseText);
            }
        });
    });
        

    $('#updateOurServiceForm').submit(function (event) {
        event.preventDefault();

        var ourServiceUpdate = {
            id: $('#updateOurServiceId').val(),
            name: $('#updateOurServiceName').val()
        };

        $.ajax({
            type: "PUT",
            url: "/Services/UpdateServiceAsync/",
            contentType: 'application/json',
            data: JSON.stringify(ourServiceUpdate),
            success: function (response) {
                alert("Updated OurService Successfully");
                location.reload();
            },
            error: function (xhr) {
                console.log("Error updating OurService: " + xhr.responseText);
                alert("Error updating OurService: " + xhr.responseText);
            }
        });
    });
        
});

