$(function () {
    confirmDelete = function (id) {
        var result = confirm("Desea eliminar el registro?")
        if (result) {
            var empresaTable = $("#posicionoffpremisetable");
            var urlDelete = empresaTable.attr('data-urlDelete');

            $.ajax({
                url: urlDelete + '/'+id,
                type: 'DELETE',
                success: function (result) {
                    toastr.success("Eliminado con exito!")
                    $('#posicionoffpremisetable').DataTable().ajax.reload();
                },
                error: function (error) {
                }

            });
        }
        return result;
    }
    $(document).ready(function () {

        var empresaTable = $("#posicionoffpremisetable");
        var url = empresaTable.attr('data-url');
        var urlEdit = empresaTable.attr('data-urlEdit');
        var urlEditDetails = empresaTable.attr('data-urlEditDetails');
        var modal = $(".modal-dialog");
        modal.removeClass("modal-xl");
        modal.removeClass("modal-lg");
        modal.removeClass("modal-sm");

        empresaTable.DataTable({
            dom: "<'row'<'col-6 text-left'f><'col-6 text-right'l>><'row'<'col-sm-12'tr>><'row'<'col-sm-12'ip>>",
            scrollX: true,
            searching: true,
            orderable: true,
            columns: [
                { data: "empresa", title: "Empresa", autoWidth: true },
                { data: "vicePresidencia", title: "VicePresidencia", autoWidth: true },
                { data: "departamento", title: "Departamento", autoWidth: true },
                { data: "posicion", title: "Posición", autoWidth: true },
                {
                    data: null,
                    title: "Acciones",
                    render: function (data, type, row) {
                        var route = '<a class="modal-link" data-toggle="tooltip" title="Editar" href="' + urlEdit + '/' + row.id + '"><i class="fa fa-edit fa-2x"></i></a>' +
                            '<a class="modal-link" style="margin-left: 5px" data-toggle="tooltip" title="Empleados" href="' + urlEditDetails + '/' + row.id + '"><i class="fa fa-address-book-o  fa-2x"></i></a>' +
                            '<a  style="margin-left: 5px" data-toggle="tooltip" title="Eliminar" onclick="return confirmDelete('+ row.id +');"><i class="fa fa-trash  fa-2x"></i></a>';
                        return route;
                    }
                }
            ],
            columnDefs: [
                {
                    className: 'text-center',
                    targets: [4]
                }
            ],
            pageLength: 10,
            serverSide: true,
            ajax: {
                url: url,
                type: 'POST',
                data: function (data) {
                    var filtro = {
                        searchValue: data.search.value
                    };

                    data.extraData = JSON.stringify(filtro);
                    return data;
                }
            },
            processing: true,
            language: {
                processing: "Procesando...",
                lengthMenu: "Mostrar _MENU_ registros",
                zeroRecords: "No se encontraron resultados",
                info: "Mostrando pagina _PAGE_ de _PAGES_ de un total de _MAX_ registro(s)",
                emptyTable: "Ningún dato disponible en esta tabla",
                infoEmpty: "Mostrando registros del 0 al 0 de un total de 0 registros",
                infoFiltered: "(filtrado de un total de _MAX_ registros)",
                search: "Buscar:",
                infoThousands: ",",
                loadingRecords: "Cargando...",
                paginate: {
                    first: "Primero",
                    last: "Último",
                    next: "Siguiente",
                    previous: "Anterior"
                }
            }
        });
    })
})

