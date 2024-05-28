jQuery.noConflict()(function ($) {
    $(document).ready(function () {
        loadDataTable();
    })
})

var dataTable;

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Nurses/GetAllOC",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "room.rNo", "width": "10%" },
            { "data": "nurses.name", "width": "20%" },
            { "data": "sTime", "width": "23%" },
            { "data": "eTime", "width": "23%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Nurses/AddOnCall/${data}" class="btn btn-primary" style="cursor:pointer; width:40px">
                                    <i class="fas fa-edit" style="color:white"></i>
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Nurses/DeleteOC/${data}") class="btn btn-danger" style="cursor:pointer; width:40px">
                                    <i class="fas fa-trash-alt" style="color:white"></i>
                                </a>
                            </div>`;
                },
                "width": "14%"
            }
        ],
        "language": {
            "emptyTable": "No Record Found!"
        }
    })
}

function Delete(url) {
    swal({
        title: "ARE YOU SURE YOU WANT TO DELETE IT?",
        text: "YOU WILL NOT BE ABLE TO RESTORE IT!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#BD6B55",
        confirmButtonText: "YES, DELETE IT",
        closeOnConfirm: true
    },
        function () {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    )
}