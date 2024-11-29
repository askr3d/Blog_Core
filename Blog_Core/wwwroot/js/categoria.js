let dataTable;

$(document).ready(() => {
    cargarDatatable();
});

const cargarDatatable = () => {
    dataTable = $("#tblCategorias").DataTable({
        ajax: {
            url: "/Admin/Categorias/GetAll",
            type: "GET",
            datatype: "json",
        },
        columns: [
            { data: "id", width: "5%" },
            { data: "nombre", width: "50%" },
            { data: "orden", width: "20%" },
            {
                data: "id",
                render: (data) => {
                    return `
                        <div class="text-center d-flex">
                            <a href="/Admin/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer;">
                                <i class="fa-regular fa-pen-to-square"></i> Editar
                            </a>
                            &nbsp
                            <a onclick="Delete('/Admin/Categorias/Delete/${data}')" class="btn btn-danger text-white" style="cursor:pointer;">
                                <i class="fa-solid fa-trash"></i> Eliminar
                            </a>
                        </div>
                    `
                },
                width: "30%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}


const Delete = (url) => {
    Swal.fire({
        icon: "warning",
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
    }).then(result => {
        if (result.isConfirmed) {
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
            });
        }
    });
}