$(document).ready(function () {
    $('#projectForm').submit(function (event) {
        event.preventDefault();

        var formData = new FormData();
        formData.append("Name", $('#projectName').val());
        formData.append("Description", $('#projectDescription').val());
        formData.append("Location", $('#projectLocation').val());
        formData.append("Tag", $('#projectTag').val());
        formData.append("ProjectUrl", $('#projectUrl').val());

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
                console.log("Server Response:", response);
                alert('Project added successfully!');
                $('#projectForm')[0].reset();
            },
            error: function (xhr) {
                console.log("Server Error:", xhr.responseText);
                alert('Error adding project: ' + xhr.responseText);
            }
        });
    });

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
    });
});
