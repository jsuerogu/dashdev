$(function () {
    $(document).ready(function () {

        var ausentismotable = $("#ausentismotable");
        var url = ausentismotable.attr('data-url');
        var urlEdit = ausentismotable.attr('data-urlEdit');
        var modal = $(".modal-dialog");
        modal.removeClass("modal-xl");
        modal.removeClass("modal-lg");
        modal.removeClass("modal-sm");

        ausentismotable.DataTable({
            dom: "<'row'<'col-6 text-left'f><'col-6 text-right'l>><'row'<'col-sm-12'tr>><'row'<'col-sm-12'ip>>",
            scrollX: true,
            searching: true,
            orderable: true,
            columns: [
                { data: "aucod", title: "Código", autoWidth: true },
                { data: "audes", title: "Descripción", autoWidth: true },
                {
                    data: "aujus",
                    title: "Justificada",
                    render: function (cellData) {
                        var data = cellData ? 'Si' : 'No';
                        return '<div>' + data + '</div>';
                    }
                },
                {
                    data: "autel",
                    title: "Teletrabajo",
                    render: function (cellData) {
                        var data = cellData ? 'Si' : 'No';
                        return '<div>' + data + '</div>';
                    }
                },
                {
                    data: "riesgo",
                    title: "Riesgo",
                    render: function (cellData) {
                        var data = cellData ? 'Si' : 'No';
                        return '<div>' + data + '</div>';
                    }
                },
                {
                    data: "cuarentena",
                    title: "Cuarentena",
                    render: function (cellData) {
                        var data = cellData ? 'Si' : 'No';
                        return '<div>' + data + '</div>';
                    }
                },
                {
                    data: null,
                    title: "Acciones",
                    render: function (data, type, row) {
                        var route = '<a class="modal-link" data-toggle="tooltip" title="Editar" href="' + urlEdit + '/' + row.id + '"><i class="fa fa-edit fa-2x"></i></a></span>';
                        return route;
                    }
                }
            ],
            columnDefs: [
                {
                    className: 'text-center',
                    targets: [6]
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

