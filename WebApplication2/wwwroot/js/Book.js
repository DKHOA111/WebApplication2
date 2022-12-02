
var dtable;
$(document).ready(function () {
    dtable = $('#myTable').DataTable({

        "ajax": { "url": "/Admin/Book/AllBooks" },
        "columns": [
            { "data": "name" },
            { "data": 'author' },
            { "data": 'description' },
            { "data": "price" },
            { "data": "category.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Admin/Book/CreateUpdate?id=${data}"><i class="bi bi-pencil-square"></i></a> 
                            
                 <a onclick=RemoveBook("/Admin/Book/Delete/${data}")><i class="bi bi-trash"></i></a>
     `}
            }
        ]
    });
});
function RemoveBook(url) {

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',

    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dtable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })
};