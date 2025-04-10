$(document).ready(function () {

    function loadProjects() {
        $.ajax({
            type: 'GET',
            url: '/Projects/GetProjects',
            success: function (projects) {
                console.log("Projects Loaded:", projects);
                let tableBody = $('#projecttable');
                tableBody.empty();

                projects.forEach(function (project) {
                    var row = `<tr>
                        <td>${project.name}</td>
                        <td>${project.description}</td>
                        <td>${project.location}</td>
                        <td>${project.tag}</td>
                        <td><img src="${project.imagePath}" width="100"/></td>
                                        <td>${project.category ? project.category.name : 'No Category'}</td> <!-- Show category name -->

                        <td>
                            <button class="btn btn-outline-secondary btn-sm edit-btn" data-id="${project.id}">Update</button>
                        </td>

                    </tr>`;
                    tableBody.append(row);
                });
            },
            error: function (xhr) {
                console.log("Error fetching projects:", xhr.responseText);
            }
        });
    }
    loadProjects();


    // Add Project
    $('#projectForm').submit(function (event) {
        event.preventDefault();

        var formData = new FormData();
        formData.append("Name", $('#projectName').val());
        formData.append("Description", $('#projectDescription').val());
        formData.append("Location", $('#projectLocation').val());
        formData.append("Tag", $('#projectTag').val());
        formData.append("CategoryId", $('#projectCategory').val());  
        var file = $('#projectImage')[0].files[0];
        $('#projectCategory').val(project.categoryId); 

        if (file) {
            formData.append("formFile", file);
        }

        $.ajax({
            type: 'POST',
            url: '/Projects/AddProject',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                alert('Project added successfully!');
                $('#projectForm')[0].reset();
                location.reload();
            },
            error: function (xhr) {
                console.log("Server Error:", xhr.responseText);
                alert('Error adding project: ' + xhr.responseText);
            }
        });
    });


//Update Button
    $(document).on('click', '.edit-btn', function () {
        var projectId = $(this).data('id');

        $.ajax({
            type: 'GET',
            url: '/Projects/GetProjectById/' + projectId,
            success: function (response) {
                $('#updateProjectId').val(response.id);
                $('#updateProjectName').val(response.name);
                $('#updateProjectDescription').val(response.description);
                $('#updateProjectLocation').val(response.location);
                $('#updateProjectTag').val(response.tag);
                $('#updateProjectCategory').val(response.categoryId); 
                $('#updateProjectImagePreview').attr('src', response.imagePath);
                $('#updateModal').modal('show');
            },
            error: function (xhr) {
                console.log("Error fetching project:", xhr.responseText);
            }
        });
        
    });

    $('#updateProjectForm').submit(function (event) {
    event.preventDefault();
    console.log("Update form submitted");

    var projectId = $('#updateProjectId').val();  

    var formData = new FormData();
    formData.append("Id", projectId);
    formData.append("Name", $('#updateProjectName').val());
    formData.append("Description", $('#updateProjectDescription').val());
    formData.append("Location", $('#updateProjectLocation').val());
    formData.append("Tag", $('#updateProjectTag').val());

    var file = $('#updateProjectImage')[0].files[0];
    if (file) {
        formData.append("formFile", file);  
    }
    console.log($('#updateProjectImage')[0].files[0]);
    $.ajax({
        type: 'PUT',
        url: '/Projects/UpdateProject/' + projectId, 
        data: formData,
        contentType: false, // Necessary for FormData
        processData: false, // Necessary for FormData
        success: function (response) {
            alert('Project updated successfully!');
            $('#updateModal').modal('hide');
            location.reload();                                                   
        },
        error: function (xhr) {
            console.log("Error updating project:", xhr.responseText);
            alert('Error updating project: ' + xhr.responseText);
        }
    });
});

    
 ///Delete Button
    $(document).on('click', '.delete-btn', function () {
        var projectId = $(this).data('id');
        if (confirm("Are you sure you want to delete this project?")) {
            $.ajax({
                type: 'DELETE',
                url: '/Projects/DeleteProject/' + projectId,
                success: function (response) {
                    alert('Project deleted successfully!');
                    location.reload();
                },
                error: function (xhr) {
                    console.log("Error deleting project:", xhr.responseText);
                    alert('Error deleting project: ' + xhr.responseText);
                },
            });
        }
    });

});
