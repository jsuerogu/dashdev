/*=========================================================================================
    File Name: chart-apex.js
    Description: Apexchart Examples
    ----------------------------------------------------------------------------------------
    Item Name: Vuexy  - Vuejs, HTML & Laravel Admin Dashboard Template
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

$(function () {
    'use strict';

    var totalPresenteHoy                = $('#totalPresenteHoy').val();
    var totalPresenteAyer               = $('#totalPresenteAyer').val();
    var totalAusenteHoy                 = $('#totalAusenteHoy').val();
    var totalAusenteAyer                = $('#totalAusenteAyer').val();
    var totalTardanzas_x_diar           = $('#totalTardanzas_x_diar').val();
    var totalInasistencia_x_diar        = $('#totalInasistencia_x_diar').val();
    var fechas_grefico                  = $('#fechas_grefico').val();
    var totalTardanzas_x_dia_ayer       = $('#totalTardanzas_x_dia_ayer').val();
    var totalInasistencia_x_dia_ayer    = $('#totalInasistencia_x_dia_ayer').val();

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

    // heat chart data generator
    function generateDataHeat(count, yrange) {
        var i = 0;
        var series = [];
        while (i < count) {
            var x = 'w' + (i + 1).toString();
            var y = Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min;

            series.push({
                x: x,
                y: y
            });
            i++;
        }
        return series;
    }

    // Init flatpicker
    if (flatPicker.length) {
        var date = new Date();
        flatPicker.each(function () {
            $(this).flatpickr({
                mode: 'range',
                defaultDate: ['2019-05-01', '2019-05-10']
            });
        });
    }

    // Area Chart
    // --------------------------------------------------------------------
    /*
  var areaChartEl = document.querySelector('#line-area-chart-0'),
    areaChartConfig = {
      chart: {
        height: 400,
        type: 'area',
        parentHeightOffset: 0,
        toolbar: {
          show: false
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        show: false,
        curve: 'straight'
      },
      legend: {
        show: true,
        position: 'top',
        horizontalAlign: 'start'
      },
      grid: {
        xaxis: {
          lines: {
            show: true
          }
        }
      },
      colors: [chartColors.area.series3, chartColors.area.series2, chartColors.area.series1],
      series: [
        {
          name: 'Tardanza',
          data: [100, 120, 90, 170, 130, 160, 140, 240, 220, 180, 270, 280, 375]
        },
        {
          name: 'Inasistencia',
          data: [60, 80, 70, 110, 80, 100, 90, 180, 160, 140, 200, 220, 275]
        }
      ],
      xaxis: {
        categories: [
          '7/12',
          '8/12',
          '9/12',
          '10/12',
          '11/12',
          '12/12',
          '13/12',
          '14/12',
          '15/12',
          '16/12',
          '17/12',
          '18/12',
          '19/12',
          '20/12'
        ]
      },
      fill: {
        opacity: 1,
        type: 'solid'
      },
      tooltip: {
        shared: false
      },
      yaxis: {
        opposite: isRtl
      }
    };
  if (typeof areaChartEl !== undefined && areaChartEl !== null) {
    var areaChart = new ApexCharts(areaChartEl, areaChartConfig);
    areaChart.render();
  }
  */


    // Area Chart
    // --------------------------------------------------------------------
    //Trdanza
    var arr_tandanzas_hoy = totalTardanzas_x_diar.split('||');
    var arr_tandanzas_ayer = totalTardanzas_x_dia_ayer.split('||');
    var arr_fecha = fechas_grefico.split('||');
    

    //Inasistencia
    var arr_inasistencia_hoy = totalInasistencia_x_diar.split('||');
    var arr_inasistencia_ayer = totalInasistencia_x_dia_ayer.split('||');
    //var fechas = fechas_grefico.split(',');


    for (var i = 0; i < 2; i++) {
        var arr_2_arr_tandanzas_hoy = arr_tandanzas_hoy[i].split(',');
        //var arr_2_arr_tandanzas_ayer = arr_tandanzas_ayer[i].split(',');
        var arr_2_arr_inasistencia_hoy = arr_inasistencia_hoy[i].split(',');
        //var arr_2_arr_inasistencia_ayer = arr_inasistencia_ayer[i].split(',');

        var datos = [
            [
                
                arr_2_arr_inasistencia_hoy,
                arr_2_arr_tandanzas_hoy
                //arr_2_arr_tandanzas_ayer
            ],
            [
                arr_2_arr_tandanzas_hoy,
                arr_2_arr_inasistencia_hoy                
                //arr_2_arr_inasistencia_ayer
            ]
        ];


        var fechas = arr_fecha[i].split(',');
        var areaChartEl = document.querySelector('#line-area-chart-' + i),
            areaChartConfig = {
                chart: {
                    height: 400,
                    type: 'area',
                    parentHeightOffset: 0,
                    toolbar: {
                        show: true
                    }
                },
                dataLabels: {
                    enabled: true,
                    enabledOnSeries: undefined,
                    formatter: function (val, opts) {
                        return val
                    },
                    textAnchor: 'middle',
                    distributed: false,
                    offsetX: 0,
                    offsetY: 0,
                    style: {
                        fontSize: '14px',
                        fontFamily: 'Helvetica, Arial, sans-serif',
                        fontWeight: 'bold',
                        colors: ['#FFF']
                    },
                    background: {
                        enabled: true,
                        foreColor: '#fff',
                        padding: 4,
                        borderRadius: 2,
                        borderWidth: 1,
                        borderColor: '#AAA',
                        opacity: 1,
                        dropShadow: {
                            enabled: true,
                            top: 1,
                            left: 1,
                            blur: 1,
                            color: '#FFF',
                            opacity: 1
                        }
                    },
                    dropShadow: {
                        enabled: false,
                        top: 1,
                        left: 1,
                        blur: 1,
                        color: '#FFF',
                        opacity: 1
                    }
                },
                stroke: {
                    show: true,
                    curve: 'straight'
                },
                legend: {
                    show: true,
                    position: 'top',
                    horizontalAlign: 'start'
                },
                grid: {
                    xaxis: {
                        lines: {
                            show: true
                        }
                    }
                },
                colors: [chartColors.area.series3, chartColors.area.series2, chartColors.area.series1],
                series: [
                    {
                        name: 'Tardanza',
                        data: datos[0][i]
                    },
                    {
                        name: 'Inasistencia',
                        data: datos[1][i]
                    }
                ],
                xaxis: {
                    categories: fechas
                },
                fill: {
                    opacity: 1,
                    type: 'solid'
                },
                tooltip: {
                    shared: true
                },
                yaxis: {
                    opposite: isRtl
                }
            };
        if (typeof areaChartEl !== undefined && areaChartEl !== null) {
            var areaChart = new ApexCharts(areaChartEl, areaChartConfig);
            areaChart.render();
        }
    }

    // Column Chart
    // --------------------------------------------------------------------
    var columnChartEl = document.querySelector('#column-chart'),
        columnChartConfig = {
            chart: {
                height: 400,
                type: 'bar',
                stacked: true,
                parentHeightOffset: 0,
                toolbar: {
                    show: false
                }
            },
            plotOptions: {
                bar: {
                    columnWidth: '15%',
                    colors: {
                        backgroundBarColors: [
                            chartColors.column.bg,
                            chartColors.column.bg,
                            chartColors.column.bg,
                            chartColors.column.bg,
                            chartColors.column.bg
                        ],
                        backgroundBarRadius: 10
                    }
                }
            },
            dataLabels: {
                enabled: false
            },
            legend: {
                show: true,
                position: 'top',
                horizontalAlign: 'start'
            },
            colors: [chartColors.column.series1, chartColors.column.series2],
            stroke: {
                show: true,
                colors: ['transparent']
            },
            grid: {
                xaxis: {
                    lines: {
                        show: true
                    }
                }
            },
            series: [
                {
                    name: 'Apple',
                    data: [90, 120, 55, 100, 80, 125, 175, 70, 88, 180]
                },
                {
                    name: 'Samsung',
                    data: [85, 100, 30, 40, 95, 90, 30, 110, 62, 20]
                }
            ],
            xaxis: {
                categories: ['7/12', '8/12', '9/12', '10/12', '11/12', '12/12', '13/12', '14/12', '15/12', '16/12']
            },
            fill: {
                opacity: 1
            },
            yaxis: {
                opposite: isRtl
            }
        };
    if (typeof columnChartEl !== undefined && columnChartEl !== null) {
        var columnChart = new ApexCharts(columnChartEl, columnChartConfig);
        columnChart.render();
    }

    // Scatter Chart
    // --------------------------------------------------------------------
    var scatterChartEl = document.querySelector('#scatter-chart'),
        scatterChartConfig = {
            chart: {
                height: 400,
                type: 'scatter',
                zoom: {
                    enabled: true,
                    type: 'xy'
                },
                parentHeightOffset: 0,
                toolbar: {
                    show: false
                }
            },
            grid: {
                xaxis: {
                    lines: {
                        show: true
                    }
                }
            },
            legend: {
                show: true,
                position: 'top',
                horizontalAlign: 'start'
            },
            colors: [window.colors.solid.warning, window.colors.solid.primary, window.colors.solid.success],
            series: [
                {
                    name: 'Angular',
                    data: [
                        [5.4, 170],
                        [5.4, 100],
                        [6.3, 170],
                        [5.7, 140],
                        [5.9, 130],
                        [7.0, 150],
                        [8.0, 120],
                        [9.0, 170],
                        [10.0, 190],
                        [11.0, 220],
                        [12.0, 170],
                        [13.0, 230]
                    ]
                },
                {
                    name: 'Vue',
                    data: [
                        [14.0, 220],
                        [15.0, 280],
                        [16.0, 230],
                        [18.0, 320],
                        [17.5, 280],
                        [19.0, 250],
                        [20.0, 350],
                        [20.5, 320],
                        [20.0, 320],
                        [19.0, 280],
                        [17.0, 280],
                        [22.0, 300],
                        [18.0, 120]
                    ]
                },
                {
                    name: 'React',
                    data: [
                        [14.0, 290],
                        [13.0, 190],
                        [20.0, 220],
                        [21.0, 350],
                        [21.5, 290],
                        [22.0, 220],
                        [23.0, 140],
                        [19.0, 400],
                        [20.0, 200],
                        [22.0, 90],
                        [20.0, 120]
                    ]
                }
            ],
            xaxis: {
                tickAmount: 10,
                labels: {
                    formatter: function (val) {
                        return parseFloat(val).toFixed(1);
                    }
                }
            },
            yaxis: {
                opposite: isRtl
            }
        };
    if (typeof scatterChartEl !== undefined && scatterChartEl !== null) {
        var scatterChart = new ApexCharts(scatterChartEl, scatterChartConfig);
        scatterChart.render();
    }

    // Line Chart
    // --------------------------------------------------------------------
    var lineChartEl = document.querySelector('#line-chart'),
        lineChartConfig = {
            chart: {
                height: 400,
                type: 'line',
                zoom: {
                    enabled: false
                },
                parentHeightOffset: 0,
                toolbar: {
                    show: false
                }
            },
            series: [
                {
                    data: [280, 200, 220, 180, 270, 250, 70, 90, 200, 150, 160, 100, 150, 100, 50]
                }
            ],
            markers: {
                strokeWidth: 7,
                strokeOpacity: 1,
                strokeColors: [window.colors.solid.white],
                colors: [window.colors.solid.warning]
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                curve: 'straight'
            },
            colors: [window.colors.solid.warning],
            grid: {
                xaxis: {
                    lines: {
                        show: true
                    }
                },
                padding: {
                    top: -20
                }
            },
            tooltip: {
                custom: function (data) {
                    return (
                        '<div class="px-1 py-50">' +
                        '<span>' +
                        data.series[data.seriesIndex][data.dataPointIndex] +
                        '%</span>' +
                        '</div>'
                    );
                }
            },
            xaxis: {
                categories: [
                    '7/12',
                    '8/12',
                    '9/12',
                    '10/12',
                    '11/12',
                    '12/12',
                    '13/12',
                    '14/12',
                    '15/12',
                    '16/12',
                    '17/12',
                    '18/12',
                    '19/12',
                    '20/12',
                    '21/12'
                ]
            },
            yaxis: {
                opposite: isRtl
            }
        };
    if (typeof lineChartEl !== undefined && lineChartEl !== null) {
        var lineChart = new ApexCharts(lineChartEl, lineChartConfig);
        lineChart.render();
    }

    // Bar Chart
    // --------------------------------------------------------------------
    var barChartEl = document.querySelector('#bar-chart'),
        barChartConfig = {
            chart: {
                height: 400,
                type: 'bar',
                parentHeightOffset: 0,
                toolbar: {
                    show: false
                }
            },
            plotOptions: {
                bar: {
                    horizontal: true,
                    barHeight: '30%',
                    endingShape: 'rounded'
                }
            },
            grid: {
                xaxis: {
                    lines: {
                        show: false
                    }
                },
                padding: {
                    top: -15,
                    bottom: -10
                }
            },
            colors: window.colors.solid.info,
            dataLabels: {
                enabled: false
            },
            series: [
                {
                    data: [700, 350, 480, 600, 210, 550, 150]
                }
            ],
            xaxis: {
                categories: ['MON, 11', 'THU, 14', 'FRI, 15', 'MON, 18', 'WED, 20', 'FRI, 21', 'MON, 23']
            },
            yaxis: {
                opposite: isRtl
            }
        };
    if (typeof barChartEl !== undefined && barChartEl !== null) {
        var barChart = new ApexCharts(barChartEl, barChartConfig);
        barChart.render();
    }

    // Candlestick Chart
    // --------------------------------------------------------------------
    var candlestickEl = document.querySelector('#candlestick-chart'),
        candlestickChartConfig = {
            chart: {
                height: 400,
                type: 'candlestick',
                parentHeightOffset: 0,
                toolbar: {
                    show: false
                }
            },
            series: [
                {
                    data: [
                        {
                            x: new Date(1538778600000),
                            y: [150, 170, 50, 100]
                        },
                        {
                            x: new Date(1538780400000),
                            y: [200, 400, 170, 330]
                        },
                        {
                            x: new Date(1538782200000),
                            y: [330, 340, 250, 280]
                        },
                        {
                            x: new Date(1538784000000),
                            y: [300, 330, 200, 320]
                        },
                        {
                            x: new Date(1538785800000),
                            y: [320, 450, 280, 350]
                        },
                        {
                            x: new Date(1538787600000),
                            y: [300, 350, 80, 250]
                        },
                        {
                            x: new Date(1538789400000),
                            y: [200, 330, 170, 300]
                        },
                        {
                            x: new Date(1538791200000),
                            y: [200, 220, 70, 130]
                        },
                        {
                            x: new Date(1538793000000),
                            y: [220, 270, 180, 250]
                        },
                        {
                            x: new Date(1538794800000),
                            y: [200, 250, 80, 100]
                        },
                        {
                            x: new Date(1538796600000),
                            y: [150, 170, 50, 120]
                        },
                        {
                            x: new Date(1538798400000),
                            y: [110, 450, 10, 420]
                        },
                        {
                            x: new Date(1538800200000),
                            y: [400, 480, 300, 320]
                        },
                        {
                            x: new Date(1538802000000),
                            y: [380, 480, 350, 450]
                        }
                    ]
                }
            ],
            xaxis: {
                type: 'datetime'
            },
            yaxis: {
                tooltip: {
                    enabled: true
                },
                opposite: isRtl
            },
            grid: {
                xaxis: {
                    lines: {
                        show: true
                    }
                },
                padding: {
                    top: -23
                }
            },
            plotOptions: {
                candlestick: {
                    colors: {
                        upward: window.colors.solid.success,
                        downward: window.colors.solid.danger
                    }
                },
                bar: {
                    columnWidth: '40%'
                }
            }
        };
    if (typeof candlestickEl !== undefined && candlestickEl !== null) {
        var candlestickChart = new ApexCharts(candlestickEl, candlestickChartConfig);
        candlestickChart.render();
    }

    // Heat map chart
    // --------------------------------------------------------------------
    var heatmapEl = document.querySelector('#heatmap-chart'),
        heatmapChartConfig = {
            chart: {
                height: 350,
                type: 'heatmap',
                parentHeightOffset: 0,
                toolbar: {
                    show: false
                }
            },
            plotOptions: {
                heatmap: {
                    enableShades: false,

                    colorScale: {
                        ranges: [
                            {
                                from: 0,
                                to: 10,
                                name: '0-10',
                                color: '#b9b3f8'
                            },
                            {
                                from: 11,
                                to: 20,
                                name: '10-20',
                                color: '#aba4f6'
                            },
                            {
                                from: 21,
                                to: 30,
                                name: '20-30',
                                color: '#9d95f5'
                            },
                            {
                                from: 31,
                                to: 40,
                                name: '30-40',
                                color: '#8f85f3'
                            },
                            {
                                from: 41,
                                to: 50,
                                name: '40-50',
                                color: '#8176f2'
                            },
                            {
                                from: 51,
                                to: 60,
                                name: '50-60',
                                color: '#7367f0'
                            }
                        ]
                    }
                }
            },
            dataLabels: {
                enabled: false
            },
            legend: {
                show: true,
                position: 'bottom'
            },
            grid: {
                padding: {
                    top: -25
                }
            },
            series: [
                {
                    name: 'SUN',
                    data: generateDataHeat(24, {
                        min: 0,
                        max: 60
                    })
                },
                {
                    name: 'MON',
                    data: generateDataHeat(24, {
                        min: 0,
                        max: 60
                    })
                },
                {
                    name: 'TUE',
                    data: generateDataHeat(24, {
                        min: 0,
                        max: 60
                    })
                },
                {
                    name: 'WED',
                    data: generateDataHeat(24, {
                        min: 0,
                        max: 60
                    })
                },
                {
                    name: 'THU',
                    data: generateDataHeat(24, {
                        min: 0,
                        max: 60
                    })
                },
                {
                    name: 'FRI',
                    data: generateDataHeat(24, {
                        min: 0,
                        max: 60
                    })
                },
                {
                    name: 'SAT',
                    data: generateDataHeat(24, {
                        min: 0,
                        max: 60
                    })
                }
            ],
            xaxis: {
                labels: {
                    show: false
                },
                axisBorder: {
                    show: false
                },
                axisTicks: {
                    show: false
                }
            }
        };
    if (typeof heatmapEl !== undefined && heatmapEl !== null) {
        var heatmapChart = new ApexCharts(heatmapEl, heatmapChartConfig);
        heatmapChart.render();
    }

    // Radialbar Chart
    // --------------------------------------------------------------------
    var datos = [[52, 32], [36, 9]];
    for (var i = 0; i < 2; i++) {
        var radialBarChartEl = document.querySelector('#radialbar-chart-' + i),
            radialBarChartConfig = {
                chart: {
                    height: 350,
                    type: 'radialBar'
                },
                colors: [chartColors.donut.series1, chartColors.donut.series2, chartColors.donut.series4],
                plotOptions: {
                    radialBar: {
                        size: 185,
                        hollow: {
                            size: '25%'
                        },
                        track: {
                            margin: 10
                        },
                        dataLabels: {
                            name: {
                                fontSize: '2rem',
                                fontFamily: 'Montserrat'
                            },
                            value: {
                                fontSize: '1rem',
                                fontFamily: 'Montserrat'
                            },
                            total: {
                                show: true,
                                fontSize: '1rem',
                                label: 'Asistensia',
                                formatter: function (w) {
                                    return '%';
                                }
                            }
                        }
                    }
                },
                grid: {
                    padding: {
                        top: -35,
                        bottom: -0
                    }
                },
                legend: {
                    show: true,
                    position: 'top'
                },
                stroke: {
                    lineCap: 'round'
                },
                series: [datos[0][i], datos[1][i]],
                labels: ['Presente', 'Ausente']
            };
        if (typeof radialBarChartEl !== undefined && radialBarChartEl !== null) {
            var radialChart = new ApexCharts(radialBarChartEl, radialBarChartConfig);
            radialChart.render();
        }
    }

    // Radar Chart
    // --------------------------------------------------------------------
    var radarChartEl = document.querySelector('#radar-chart'),
        radarChartConfig = {
            chart: {
                height: 400,
                type: 'radar',
                toolbar: {
                    show: false
                },
                parentHeightOffset: 0,
                dropShadow: {
                    enabled: false,
                    blur: 8,
                    left: 1,
                    top: 1,
                    opacity: 0.2
                }
            },
            legend: {
                show: true,
                position: 'bottom'
            },
            yaxis: {
                show: false
            },
            series: [
                {
                    name: 'iPhone 11',
                    data: [41, 64, 81, 60, 42, 42, 33, 23]
                },
                {
                    name: 'Samsung s20',
                    data: [65, 46, 42, 25, 58, 63, 76, 43]
                }
            ],
            colors: [chartColors.donut.series1, chartColors.donut.series3],
            xaxis: {
                categories: ['Battery', 'Brand', 'Camera', 'Memory', 'Storage', 'Display', 'OS', 'Price']
            },
            fill: {
                opacity: [1, 0.8]
            },
            stroke: {
                show: false,
                width: 0
            },
            markers: {
                size: 0
            },
            grid: {
                show: false,
                padding: {
                    top: -20,
                    bottom: -20
                }
            }
        };
    if (typeof radarChartEl !== undefined && radarChartEl !== null) {
        var radarChart = new ApexCharts(radarChartEl, radarChartConfig);
        radarChart.render();
    }

    /*{
  type: 'doughnut',
  data: {
    labels: labels,
    datasets: [{
      backgroundColor: Utils.colors({
        color: Utils.color(0),
        count: DATA_COUNT
      }),
      data: Utils.numbers({
        count: DATA_COUNT,
        min: 0,
        max: 100
      }),
      datalabels: {
        anchor: 'end'
      }
    }, {
      backgroundColor: Utils.colors({
        color: Utils.color(1),
        count: DATA_COUNT
      }),
      data: Utils.numbers({
        count: DATA_COUNT,
        min: 0,
        max: 100
      }),
      datalabels: {
        anchor: 'center',
        backgroundColor: null,
        borderWidth: 0
      }
    }, {
      backgroundColor: Utils.colors({
        color: Utils.color(2),
        count: DATA_COUNT
      }),
      data: Utils.numbers({
        count: DATA_COUNT,
        min: 0,
        max: 100
      }),
      datalabels: {
        anchor: 'start'
      }
    }]
  },
  options: {
    plugins: {
      datalabels: {
        backgroundColor: function(context) {
          return context.dataset.backgroundColor;
        },
        borderColor: 'white',
        borderRadius: 25,
        borderWidth: 2,
        color: 'white',
        display: function(context) {
          var dataset = context.dataset;
          var count = dataset.data.length;
          var value = dataset.data[context.dataIndex];
          return value > count * 1.5;
        },
        font: {
          weight: 'bold'
        },
        padding: 6,
        formatter: Math.round
      }
    },

    // Core options
    aspectRatio: 4 / 3,
    cutoutPercentage: 32,
    layout: {
      padding: 32
    },
    elements: {
      line: {
        fill: false
      },
      point: {
        hoverRadius: 7,
        radius: 5
      }
    },
  }
}*/
    // Donut Chart
    // --------------------------------------------------------------------
    //alert(totalPresenteHoy)
    var _datoserPrenHoy = totalPresenteHoy.split(',');
    var _datoserAusenHoy = totalAusenteHoy.split(',');
    /*var numeros = "";
    for (var x = 0; x < count(datoser_); x++) {
        numeros += "[" + datoser_[x] + "]";
    }*/

    //alert(_datoser[1]);
    var datos = [[parseFloat(_datoserPrenHoy[0]), parseFloat(_datoserPrenHoy[1])], [parseFloat(_datoserAusenHoy[0]), parseFloat(_datoserAusenHoy[1])]];
    var i = 0;
    
    var valor_centro = datos[0][i];
    var prens = datos[0][0];
        var donutChartEl = document.querySelector('#donut-chart-' + i),
            donutChartConfig = {
                chart: {
                    height: 350,
                    type: 'donut'
                },
                legend: {
                    show: true,
                    position: 'top'
                },
                labels: ['Presente', 'Ausente'],
                series: [datos[0][i], datos[1][i]],
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
                                    label: 'Presente',
                                    formatter: function (val, opt) {
                                        return parseInt(prens) + '%';
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
                                height: 280
                            }
                        }
                    },
                    {
                        breakpoint: 576,
                        options: {
                            chart: {
                                height: 320
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
        }


    var i = 1;

    //var valor_centro = datos[0][i];
    var donutChartEl = document.querySelector('#donut-chart-' + i),
        donutChartConfig = {
            chart: {
                height: 350,
                type: 'donut'
            },
            legend: {
                show: true,
                position: 'top'
            },
            labels: ['Presente', 'Ausente'],
            series: [datos[0][i], datos[1][i]],
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
                                label: 'Presente',
                                formatter: function (val, opt) {
                                    return datos[0][i] + '%';
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
                            height: 280
                        }
                    }
                },
                {
                    breakpoint: 576,
                    options: {
                        chart: {
                            height: 320
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
    }
    
});
