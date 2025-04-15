$(document).ready(function () {

    function loadProjects() {
        $.ajax({
            type: 'GET',
            url: '/Projects/GetProctWithCategory',
            success: function (response) {
                console.log("Projects Loaded:", response);

                // Eğer response içinde $values varsa, o kısmı kullanın
                var projects = response.$values || response;

                let tableBody = $('#projecttable');
                tableBody.empty();

                projects.forEach(function (project) {
                    var categoryList = project.categories && project.categories.length > 0
                        ? project.categories.join(", ")
                        : 'No Category';
                    var ourserviceList = project.ourservice && project.ourservice.length > 0
                        ? project.ourservice.join(", ")
                        :'No  ourService'
                    var row = `<tr>
            <td>${project.name}</td>
            <td>${project.description}</td>
            <td>${project.location}</td>
            <td>${project.tag}</td>
            <td>${categoryList}</td>
            <td>${ourserviceList}</td>
<td><img src="${project.imagePath}" style="width: 70px; height: 70px; object-fit: cover;" /></td>
            <td>
                <button class="btn btn-outline-success btn-sm edit-btn" data-id="${project.id}">Update</button>
                <button class="btn btn-outline-danger btn-sm delete-btn" data-id="${project.id}">Delete</button>
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

        var selectedCategories = $('#projectCategory').val();
        if (selectedCategories) {
            if (!Array.isArray(selectedCategories)) {
                selectedCategories = [selectedCategories];
            }
            selectedCategories.forEach(function (categoryId) {
                formData.append("CategoryIds", categoryId);
            });
        }

        var selectedOurServices = $('#ourserviceProjects').val();
        if (selectedOurServices) {
            if (!Array.isArray(selectedOurServices)) {
                selectedOurServices = [selectedOurServices];
            }
            selectedOurServices.forEach(function (ourserviceId) {
                formData.append("OurServiceIds", ourserviceId);
            });
        }

        var file = $('#projectImage')[0].files[0];
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
                loadProjects();
            },
            error: function (xhr) {
                console.log("Server Error:", xhr.responseText);
                alert('Error adding project: ' + xhr.responseText);
            }
        });
    });

    // Edit Project
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

                if (response.categories && response.categories.$values) {
                    var catIds = response.categories.$values.map(c => c.id);
                    $('#updateProjectCategory').val(catIds);
                }

                if (response.ourservice && response.ourservice.$values) {
                    var serviceIds = response.ourservice.$values.map(s => s.id);
                    $('#updateProjectOurService').val(serviceIds);
                }

                $('#updateProjectImagePreview').attr('src', response.imagePath);
                $('#updateModal').modal('show');
            },
            error: function (xhr) {
                console.log("Error fetching project:", xhr.responseText);
            }
        });
    });

    // Update Project
    $('#updateProjectForm').submit(function (event) {
        event.preventDefault();

        var projectId = $('#updateProjectId').val();

        var formData = new FormData();
        formData.append("Id", projectId);
        formData.append("Name", $('#updateProjectName').val());
        formData.append("Description", $('#updateProjectDescription').val());
        formData.append("Location", $('#updateProjectLocation').val());
        formData.append("Tag", $('#updateProjectTag').val());

        var selectedCategories = $('#updateProjectCategory').val();
        if (selectedCategories) {
            selectedCategories.forEach(function (categoryId) {
                formData.append("CategoryIds", categoryId);
            });
        }

        var selectedOurServices = $('#updateProjectOurService').val();
        if (selectedOurServices) {
            selectedOurServices.forEach(function (ourserviceId) {
                formData.append("OurServiceIds", ourserviceId);
            });
        }

        var file = $('#updateProjectImage')[0].files[0];
        if (file) {
            formData.append("formFile", file);
        }

        $.ajax({
            type: 'PUT',
            url: '/Projects/UpdateProject/' + projectId,
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                alert('Project updated successfully!');
                $('#updateModal').modal('hide');
                loadProjects();
            },
            error: function (xhr) {
                console.log("Error updating project:", xhr.responseText);
                alert('Error updating project: ' + xhr.responseText);
            }
        });
    });

    // Delete Project
    $(document).on('click', '.delete-btn', function () {
        var projectId = $(this).data('id');
        if (confirm("Are you sure you want to delete this project?")) {
            $.ajax({
                type: 'DELETE',
                url: '/Projects/DeleteProject/' + projectId,
                success: function (response) {
                    alert('Project deleted successfully!');
                    loadProjects();
                },
                error: function (xhr) {
                    console.log("Error deleting project:", xhr.responseText);
                    alert('Error deleting project: ' + xhr.responseText);
                }
            });
        }
    });

});
