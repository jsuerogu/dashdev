// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$('form input:not([type="submit"])').keydown(function (e) {
    if (e.keyCode == 13) {
        var inputs = $(this).parents("form").eq(0).find(":input");
        if (inputs[inputs.index(this) + 1] != null) {
            inputs[inputs.index(this) + 1].focus();
        }
        e.preventDefault();
        return false;
    }
})

$('body').on('click', '.modal-link', function (e) {
    e.preventDefault();
    $('.modal-body').removeData();

    $(this).attr('data-target', '#modal-container');
    $(this).attr('data-toggle', 'modal');

    $.ajax({
        url: $(this).prop('href'),
        type: 'GET',
        success: function (result) {
            $('.modal-body').html(result);
            $('#preload').hide();
        },
        error: function (error) {
            $('#preload').hide();
        }

    });

    
});

$('body').on('submit', '.modal-link-submit', function (e) {
    e.preventDefault();

    var url = $(this).attr('data-action');
    var method = $(this).prop('method').toUpperCase();
    var data = $(this).serialize();
    var urlCallBack = $(this).prop('baseURI');

    $.ajax({
        url: url,
        type: method,
        data: data,
        success: function (result) {
            if (!result.startsWith('message:')) {
                $('.modal-body').removeData();
                $('.modal-body').html(result);
            } else {
                window.location.href = urlCallBack;
                toastr.success(result.replace('message:', ''), 'Actualizar ...')
            }

            $('#preload').hide();
        }

    });


});

$('.extra-fields').change(function (e) {
    var idTable = $(this).attr('data-table');
    var table = $('#table-' + idTable).DataTable();

    var column = table.column($(this).attr('data-column'));
    column.visible($(this).prop('checked'));
});

function toggleTop(id) {
    var target = $('#top-' + id);
    if (target.hasClass('show'))
        target.removeClass('show');
    else
        target.addClass('show');
};

function resumenTop(itemJsonSerialize, url, tipo) {
    var empresa = $("#empresa").val()
    var orden = $("#orden").val()
    var colaborador = $("#colaborador").val();

    var item = JSON.parse(itemJsonSerialize);
    var indicador = item.DatosClase;
    var departamento = urlEncode(item.Descripcion);

    window.location.href = url + '?empresa=' + empresa + '&orden=' + orden + '&indicador=' + indicador + '&departamento=' + departamento + '&tipo=' + tipo + '&colaborador=' + colaborador;
}

function toggleDropdownMenu(id) {

    var target = $('#dropdown-' + id);
    target.toggle();
};

function resumen(indicador, url) {
    var empresa = $("#empresa").val()
    var orden = $("#orden").val()
    var rangoHora = $("#hourRangeCheck").is(':checked');
    var colaborador = $("#colaborador").val();

    window.location.href = url + '?empresa=' + empresa + '&orden=' + orden + '&indicador=' + indicador + '&rangoHora=' + rangoHora.toString() + '&colaborador=' + colaborador;
}

$('.btn-collapse').on('click', function (e) {
    var target = document.getElementById(this.getAttribute('data-target').replace('#', ''));
    var collapse = $('#' + $(this).attr('aria-controls'));
    if (collapse.hasClass('show'))
        collapse.removeClass('show');
    else
        collapse.addClass('show');

    var url = this.getAttribute('data-url');
    if (target.getAttribute('data-loaded') === null) {
        cargarDataTable($('#formFilter'), target, url);
    }
})

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        var name = this.name.replace('Filtro.', '');
        if (!o[name]) {
            o[name] = this.value || '';
        }
    });
    return o;
};

function exportMail(url) {
    var form = $('#formFilter').serializeObject();
    $('#preload').show();

    $.ajax({
        url: url,
        type: 'POST',
        data: form
    }).fail(function (error) {
        alert(error.error);
    }).done(function (info) {
        alert(info.mensaje);
    }).always(function (data) {
        $('#preload').hide();
    });
}

function locatePopover(data) {
    $('.conten_popover').hide();
    $('.conten_popover-' + data).show();
};

function cargarDataTable(form, target, url) {

    var id = target.getAttribute('data-departamentoId');
    var table = $('#table-' + id);


    table.dataTable({
        dom: "<'row'<'col-6 text-left'f><'col-6 text-right'l>><'row'<'col-sm-12'tr>><'row'<'col-sm-12'ip>>",
        scrollX: true,
        searching: true,
        orderable: true,
        columns: [
            {
                data: 'indicador.indicador', title: "Indicador",
                render: function (cellData) {
                    return '<div class="semaphore ' + cellData + '" style="margin-left:50%"></div>';
                }
            },
            {
                data: 'indicador.tipo', title: 'Tipo', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'departamento', title: 'Departamento', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'codigo', title: 'Código', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'nombre', title: 'Nombre', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'posicion', title: 'Posición', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'horario', title: 'Horario', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'entrada', title: 'Entrada', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'salida', title: 'Salida', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'cantHoras', title: 'Ctd.Horas', render: function (cellData) {
                    return cellData;
                }
            },            
            {
                data: 'horasextras', title: 'Horas Extras', render: function (cellData) {
                    return cellData;
                }
            },            
            {
                data: 'nombresupervisor', title: 'Supervisor', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'correosupervisor', title: 'Correo supervisor', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'telefono', title: 'Teléfono', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'correo', title: 'Correo', render: function (cellData) {
                    return cellData;
                }
            },
            {
                data: 'fecha', title: 'Fecha', render: function (cellData) {
                    return cellData;
                }
            }
        ],
        columnDefs: [
            { targets: 0, orderable: false },
            { targets: [11, 12, 13, 14], visible: false }
        ],
        pageLength: 100,
        serverSide: true,
        ajax: {
            url: url,
            type: 'POST',
            data: function (data) {
                var filtro = form.serializeObject();

                if (filtro['Departamento'] !== target.getAttribute('data-filtro')) {
                    filtro['Departamento'] = target.getAttribute('data-filtro');
                }
                filtro.orderColumn = data.order[0].column;
                filtro.orderDirection = data.order[0].dir;
                filtro.searchValue = data.search.value
                data.extraData = JSON.stringify(filtro)
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

    target.setAttribute('data-loaded', "");
}

function urlEncode(val) {
    var url = val.replaceAll(" ", "%20");
    return url;
}

function cargarVicePresidencias(dropDown, empresa, url) {
    $('#preload').show();
    $.getJSON(url + '?empresa=' + empresa, function (response) {
        $(dropDown).html(response);
        $('#preload').hide();
    });
}


function cargarDepartamentos(dropDown, empresa, orden, url) {
    $('#preload').show();
    $.getJSON(url + '?empresa=' + empresa + '&orden=' + orden, function (response) {
        $(dropDown).html(response);
        $('#preload').hide();
    });
}

function cargarPosiciones(dropDown, empresa, orden, departamento, url) {
    $('#preload').show();
    $.getJSON(url + '?empresa=' + empresa + '&orden=' + orden + '&departamento=' + departamento, function (response) {
        $(dropDown).html(response);
        $('#preload').hide();
    });
}

function cargarHorasExtrasGrafico(url) {
    var empresa = $("#empresa").val();
    var vicepresidencia = "";
    if ($("#orden")) {
        vicepresidencia = $("#orden").val();
    }

    $.getJSON(url + '?empresa=' + empresa + '&orden=' + vicepresidencia , function (response) {
        configurarHorasExtrasGrafico("#horas-extras-barchar-0", response);
    })
};

function configurarHorasExtrasGrafico(grafico, result) {

    var options = {
        legend: {
            position: 'top'
        },
        fill: {
            type: 'solid',
            opacity: 1
        },
        colors: ['#BF8F61', '#00468B'],
        series: [{
            name: 'Horas Extras Plan',
            data: [result.horasExtrasPlanificadas]
        }, {
            name: 'Horas Extras Real',
            data: [result.horasExtras]
        }],
        chart: {
            type: 'bar',
            height: 430
        },
        plotOptions: {
            bar: {
                horizontal: true,
                dataLabels: {
                    position: 'top',
                },
            }
        },
        dataLabels: {
            enabled: true,
            offsetX: -6,
            style: {
                fontSize: '12px',
                colors: ['#fff']
            },
            formatter: function (val, opts) {
                return val.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ",");
            },
        },
        stroke: {
            show: true,
            width: 1,
            colors: ['#fff']
        },
        tooltip: {
            shared: true,
            intersect: false,
            y: {
                formatter: function (value, { series, seriesIndex, dataPointIndex, w }) {
                    return value.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ",");
                }
            }
        },
        xaxis: {
            categories: [result.mesAno],
        },
    };

    var horasExtrasCharEl = document.querySelector(grafico);

    if (typeof horasExtrasCharEl !== undefined && horasExtrasCharEl !== null) {
        var horasExtrasChar = new ApexCharts(horasExtrasCharEl, options);
        horasExtrasChar.render();
        horasExtrasChar.updateSeries([{
            name: 'Horas Extras Plan',
            data: [result.horasExtrasPlanificadas]
        }, {
            name: 'Horas Extras Real',
            data: [result.horasExtras]
            }]);
    }
};

function cargarDataGraficoDonut(url) {
    var empresa = $("#empresa").val();
    var rangoHora = $("#hourRangeCheck").is(':checked');
    var vicepresidencia = "";
    if ($("#orden")) {
        vicepresidencia = $("#orden").val();
    }
    var colaborador = $("#colaborador").val();

    $.getJSON(url + '?empresa=' + empresa + '&orden=' + vicepresidencia + '&rangoHora=' + rangoHora.toString() + '&colaborador=' + colaborador, function (response) {
        configurarGraficoDonut("#donut-chart-0", response.presentesHoy, response.ausentesHoy);
    })
};

function configurarGraficoDonut(grafico, presentes, ausentes) {

    var flatPicker = $('.flat-picker'),
        isRtl = $('html').attr('data-textdirection') === 'rtl',
        chartColors = {
            column: {
                series1: '#826af9',
                series2: '#d2b0ff',
                bg: '#f8d3ff'
            },
            success: {
                shade_100: '#7eefc7',
                shade_200: '#06774f'
            },
            donut: {
                series1: '#004C3B',
                series2: '#00d4bd',
                series3: '#826bf8',
                series4: '#2b9bf4',
                series5: '#BF8F61'
            },
            area: {
                series3: '#BF8F61', // 'rgba(15, 101, 174, 0.5)',
                series2: '#004C3B', // 'rgba(0, 76, 59, 0.5)',
                series1: '#FFF'
            }
        };
    var donutChartEl = document.querySelector(grafico);
    var donutChartConfig = {
            chart: {
                height: 285,
                type: 'donut'
            },
            legend: {
                show: true,
                position: 'top'
            },
            labels: ['A tiempo', 'Inasistencia'],
            series: [presentes, ausentes],
            colors: [
                chartColors.donut.series1,
                chartColors.donut.series5,
                chartColors.donut.series3,
                chartColors.donut.series2
            ],
            dataLabels: {
                enabled: true,
                formatter: function (val, opt) {
                    return parseInt(val) + '%';
                }
            },
            plotOptions: {
                pie: {
                    donut: {
                        labels: {
                            show: true,
                            name: {
                                fontSize: '2rem',
                            },
                            value: {
                                fontSize: '1rem',
                                formatter: function (val) {
                                    return parseInt(val) + '%';
                                }
                            },
                            total: {
                                show: true,
                                fontSize: '1.5rem',
                                label: 'A tiempo',
                                formatter: function (val, opt) {
                                    return parseInt(presentes) + '%';
                                }
                            }
                        }
                    }
                }
            },
            responsive: [
                {
                    breakpoint: 992,
                    options: {
                        chart: {
                            height: 285
                        }
                    }
                },
                {
                    breakpoint: 576,
                    options: {
                        chart: {
                            height: 285
                        },
                        plotOptions: {
                            pie: {
                                donut: {
                                    labels: {
                                        show: true,
                                        name: {
                                            fontSize: '1.5rem'
                                        },
                                        value: {
                                            fontSize: '1rem'
                                        },
                                        total: {
                                            fontSize: '1.5rem'
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            ]
        };

    if (typeof donutChartEl !== undefined && donutChartEl !== null) {
        var donutChart = new ApexCharts(donutChartEl, donutChartConfig);
        donutChart.render();
        donutChart.updateSeries([presentes, ausentes]);
    }
};

function configurarGraficoArea(url) {
    $('#preload').show();
    var empresa = $("#empresa").val();

    var vicepresidencia = "";
    if ($("#orden")) {
        vicepresidencia = $("#orden").val();
    }

    $.getJSON(url + '?empresa=' + empresa + '&orden=' + vicepresidencia, function (response) {
        data = response;

        var primeraMitad = Math.round(response.fechas.length / 2);

        var tardanzaData = response.tardanzas.map(item => item.total)
        var inasistenciaData = response.inasistencias.map(item => item.total)
        var config = {
            position: 'top',
            colors: ['#00468B', '#BF8F61', '#FFF'],
            series: [
                {
                    name: "Tardanzas",
                    data: [tardanzaData.slice(0, primeraMitad),
                    tardanzaData.slice(primeraMitad + 1)]
                },
                {
                    name: "Inasistencias",
                    data: [inasistenciaData.slice(0, primeraMitad),
                    inasistenciaData.slice(primeraMitad + 1)]
                }
            ],
            type: 'area',
            curve: 'straight',
            dataLabels: {
                enabled: true,
                style: {
                    colors: ['#FFF']
                }
            },
            categories: {
                data: [
                    response.fechas.slice(0, primeraMitad),
                    response.fechas.slice(primeraMitad + 1)
                ],
                type: 'datetime'
            },
            tooltip: 'dd/MM/yyyy',
            fill: {
                type: 'solid',
                opacity: 1
            }
        };

        var options = {
            legend: {
                position: config.position
            },
            colors: config.colors,
            series: [{
                name: config.series[0].name,
                data: config.series[0].data[0]
            },
            {
                name: config.series[1].name,
                data: config.series[1].data[0]
            }],
            chart: {
                height: 350,
                type: config.type
            },
            dataLabels: config.dataLabels,
            stroke: {
                curve: config.curve
            },
            xaxis: {
                type: config.categories.type,
                categories: config.categories.data[0]
            },
            tooltip: {
                x: {
                    format: config.tooltip
                },
            },
            fill: {
                opacity: config.fill.opacity,
                type: config.fill.type
            },
            noData: {
                text: "Loading...",
                align: 'center',
                verticalAlign: 'middle',
                offsetX: 0,
                offsetY: 0,
                style: {
                    color: "#000000",
                    fontSize: '14px',
                    fontFamily: "Helvetica"
                }
            }
        };

        var charElement = document.querySelector("#line-area-chart-0");
        if (typeof charElement !== undefined && charElement !== null) {
            var chart = new ApexCharts(charElement, options);
            chart.render();
            chart.updateSeries([{
                name: config.series[0].name,
                data: config.series[0].data[0]
            },
            {
                name: config.series[1].name,
                data: config.series[1].data[0]
            }]);
        }

        var options2 = {
            legend: {
                position: config.position
            },
            colors: config.colors,
            series: [{
                name: config.series[0].name,
                data: config.series[0].data[1]
            },
            {
                name: config.series[1].name,
                data: config.series[1].data[1]
            }],
            chart: {
                height: 350,
                type: config.type
            },
            dataLabels: config.dataLabels,
            stroke: {
                curve: config.curve
            },
            xaxis: {
                type: config.categories.type,
                categories: config.categories.data[1]
            },
            tooltip: {
                x: {
                    format: config.tooltip
                },
            },
            fill: {
                opacity: config.fill.opacity,
                type: config.fill.type
            }

        };

        var char2Element= document.querySelector("#line-area-chart-1");
        if (typeof char2Element !== undefined && char2Element !== null) {
            var chart2 = new ApexCharts(char2Element, options2);
            chart2.render();
            chart2.updateSeries([{
                name: config.series[0].name,
                data: config.series[0].data[1]
            },
            {
                name: config.series[1].name,
                data: config.series[1].data[1]
            }]);
        }
   
        $('#preload').hide();
    });
};

function configurarGraficoAreaMobile(url) {
    $('#preload').show();
    var empresa = $("#empresa").val();

    var vicepresidencia = "";
    if ($("#orden")) {
        vicepresidencia = $("#orden").val();
    }

    $.getJSON(url + '?empresa=' + empresa + '&orden=' + vicepresidencia, function (response) {
        data = response;

        var primeraMitad = Math.round(response.fechas.length / 4);

        var tardanzaData = response.tardanzas.map(item => item.total)
        var inasistenciaData = response.inasistencias.map(item => item.total)
        var config = {
            position: 'top',
            colors: ['#00468B', '#BF8F61', '#FFF'],
            series: [
                {
                    name: "Tardanzas",
                    data: [tardanzaData.slice(0, primeraMitad),
                    tardanzaData.slice(primeraMitad, primeraMitad * 2),
                    tardanzaData.slice(primeraMitad * 2, primeraMitad * 3),
                    tardanzaData.slice(primeraMitad * 3)
                    ]
                },
                {
                    name: "Inasistencias",
                    data: [inasistenciaData.slice(0, primeraMitad),
                    inasistenciaData.slice(primeraMitad, primeraMitad * 2),
                    inasistenciaData.slice(primeraMitad * 2, primeraMitad * 3),
                    inasistenciaData.slice(primeraMitad * 3)
                    ]
                }
            ],
            type: 'area',
            curve: 'straight',
            dataLabels: {
                enabled: true,
                style: {
                    colors: ['#FFF']
                }
            },
            categories: {
                data: [
                    response.fechas.slice(0, primeraMitad),
                    response.fechas.slice(primeraMitad, primeraMitad * 2),
                    response.fechas.slice(primeraMitad * 2, primeraMitad * 3),
                    response.fechas.slice(primeraMitad * 3)
                ],
                type: 'datetime'
            },
            tooltip: 'dd/MM/yyyy',
            fill: {
                type: 'solid',
                opacity: 1
            }
        };

        var options = {
            legend: {
                position: config.position
            },
            colors: config.colors,
            series: [{
                name: config.series[0].name,
                data: config.series[0].data[0]
            },
            {
                name: config.series[1].name,
                data: config.series[1].data[0]
            }],
            chart: {
                height: 350,
                type: config.type
            },
            dataLabels: config.dataLabels,
            stroke: {
                curve: config.curve
            },
            xaxis: {
                type: config.categories.type,
                categories: config.categories.data[0]
            },
            tooltip: {
                x: {
                    format: config.tooltip
                },
            },
            fill: {
                opacity: config.fill.opacity,
                type: config.fill.type
            }
        };

        var chart = new ApexCharts(document.querySelector("#line-area-chart-0"), options);
        chart.render();

        var options2 = {
            legend: {
                position: config.position
            },
            colors: config.colors,
            series: [{
                name: config.series[0].name,
                data: config.series[0].data[1]
            },
            {
                name: config.series[1].name,
                data: config.series[1].data[1]
            }],
            chart: {
                height: 350,
                type: config.type
            },
            dataLabels: config.dataLabels,
            stroke: {
                curve: config.curve
            },
            xaxis: {
                type: config.categories.type,
                categories: config.categories.data[1]
            },
            tooltip: {
                x: {
                    format: config.tooltip
                },
            },
            fill: {
                opacity: config.fill.opacity,
                type: config.fill.type
            }

        };

        var chart2 = new ApexCharts(document.querySelector("#line-area-chart-1"), options2);
        chart2.render();

        var options3 = {
            legend: {
                position: config.position
            },
            colors: config.colors,
            series: [{
                name: config.series[0].name,
                data: config.series[0].data[2]
            },
            {
                name: config.series[1].name,
                data: config.series[1].data[2]
            }],
            chart: {
                height: 350,
                type: config.type
            },
            dataLabels: config.dataLabels,
            stroke: {
                curve: config.curve
            },
            xaxis: {
                type: config.categories.type,
                categories: config.categories.data[2]
            },
            tooltip: {
                x: {
                    format: config.tooltip
                },
            },
            fill: {
                opacity: config.fill.opacity,
                type: config.fill.type
            }

        };

        var chart3 = new ApexCharts(document.querySelector("#line-area-chart-2"), options3);
        chart3.render();

        var options4 = {
            legend: {
                position: config.position
            },
            colors: config.colors,
            series: [{
                name: config.series[0].name,
                data: config.series[0].data[3]
            },
            {
                name: config.series[1].name,
                data: config.series[1].data[3]
            }],
            chart: {
                height: 350,
                type: config.type
            },
            dataLabels: config.dataLabels,
            stroke: {
                curve: config.curve
            },
            xaxis: {
                type: config.categories.type,
                categories: config.categories.data[3]
            },
            tooltip: {
                x: {
                    format: config.tooltip
                },
            },
            fill: {
                opacity: config.fill.opacity,
                type: config.fill.type
            }

        };

        var chart4 = new ApexCharts(document.querySelector("#line-area-chart-3"), options4);
        chart4.render();
        $('#preload').hide();
    });
};
