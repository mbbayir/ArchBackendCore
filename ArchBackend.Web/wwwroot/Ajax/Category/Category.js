$(document).ready(function() {
    function loadCategories() {
        $.ajax({
            type: 'GET',
            url: '/Category/GetCategories',
            success: function(categories) {
                let tableBody = $('#categorytable');
                tableBody.empty();
                categories.forEach(function(category) {
                    var row = `
                        <tr>
                            <td>${category.name}</td>
                            <td>
                                <button class="btn btn-sm btn-warning edit-btn" data-id="${category.id}">Edit</button>
                                <button class="btn btn-sm btn-danger delete-btn" data-id="${category.id}">Delete</button>
                            </td>
                        </tr>`;
                    tableBody.append(row);

                });
            },
            error: function(xhr) {
                console.error("Error Fetching Process", xhr.responseText);
            }
        });
    }
    loadCategories();
    $('#categoryForm').submit(function (event) {
        event.preventDefault();

        var category = {
            name: $('#categoryName').val()
        };

        $.ajax({
            type: 'POST',
            url: '/Category/AddCategory',
            contentType: 'application/json',
            data: JSON.stringify(category),
            success: function (response) {
                alert('Category added successfully!');
                $('#categoryForm')[0].reset();
                location.reload();

            },
            error: function (xhr) { // This line was missing a comma in your original code
                console.log("Server Error:", xhr.responseText);
                alert('Error adding category: ' + xhr.responseText);
            }
        });
    });
    
    // Delete Category
    $(document).on('click', '.delete-btn', function () {
        var categoryId = $(this).data('id');
        if (confirm("Are you sure you want to delete this category?")) {
            $.ajax({
                url: '/Category/DeleteCategory/' + categoryId,
                type: 'DELETE',
                success: function (response) {
                    alert('Category deleted successfully!');
                    location.reload(); 
                },
                error: function (xhr) {
                    console.log("Error deleting category:", xhr.responseText);
                    alert('Error deleting category: ' + xhr.responseText);
                },
            });
        }
    });    
    // Update Category
    $(document).on('click', '.edit-btn', function () {
        var categoryId = $(this).data('id');
        $.ajax({
            type: 'GET',
            url: '/Category/GetCategoryById/' + categoryId,
            success: function (category) {
                $('#updateCategoryId').val(category.id);
                $('#updateCategoryName').val(category.name);
                $('#updateModal').modal('show');
            },
            error: function (xhr) {
                console.log("Error fetching category:", xhr.responseText);
                alert('Error fetching category: ' + xhr.responseText);
            }
        });
    });
    
    $('#updateCategoryForm').submit(function (event) {
        event.preventDefault();
    
        var updatedCategory = {
            id: $('#updateCategoryId').val(),
                name: $('#updateCategoryName').val()
            };
    
            $.ajax({
                type: 'PUT',
                url: '/Category/UpdateCategory/'+ updatedCategory.id,
                contentType: 'application/json',
                data: JSON.stringify(updatedCategory),
                success: function (response) {
                    alert('Category updated successfully!');
                    location.reload();
                },
                error: function (xhr) {
                    console.log("Error updating category:", xhr.responseText);
                    alert('Error updating category: ' + xhr.responseText);
                }
            });
        });
    
});
