$(document).ready(function () {


    function initChart() {

        var table = $('#tbl_missionary');
        var json = [];
        var headers = [];
        var xlabel = [];
        var cmdata = [];
        var csdata = [];
        var pmdata = [];
        var psdata = [];

        for (var i = 0; i < table[0].rows[0].cells.length; i++) {
            headers[i] = table[0].rows[0].cells[i].innerHTML.toLowerCase().replace(/ /gi, '');
        }

        for (var i = 1; i < table[0].rows.length; i++) {
            var tableRow = table[0].rows[i];
            var rowData = {};
            for (var j = 0; j < tableRow.cells.length; j++) {
                rowData[headers[j]] = tableRow.cells[j].innerHTML;
                if (i < 13) {
                    xlabel[i - 1] = tableRow.cells[1].innerHTML + ' ' + tableRow.cells[0].innerHTML;
                    cmdata[i - 1] = tableRow.cells[2].innerHTML;
                    csdata[i - 1] = tableRow.cells[3].innerHTML;
                    pmdata[i - 1] = tableRow.cells[4].innerHTML;
                    psdata[i - 1] = tableRow.cells[5].innerHTML;
                }
            }
            json.push(rowData);
        }

        var color = Chart.helpers.color;
        var barChartData = {
            labels: xlabel,
            datasets: [{
                type: 'bar',
                label: 'Missionaries',
                backgroundColor: '#537092',
                //borderColor: '#537092',
                data: cmdata
            }, {
                type: 'bar',
                label: 'Missionaries (PY)',
                //borderColor: window.chartColors.blue,
                data: csdata
            }, {
                type: 'bar',
                label: 'Schedules',
                backgroundColor: '#a93131',
                borderColor: '#6e2020',
                data: pmdata
            }, {
                type: 'bar',
                label: 'Schedules (PY)',
                backgroundColor: '#bd7f7f',
                borderColor: '#bd7f7f',
                data: psdata
            }]
        };

        var btx = document.getElementById("canvas1").getContext("2d");
        window.myBar = new Chart(btx, {
            type: 'bar',
            data: barChartData,
            options: {
                responsive: true,
                legend: {
                    display: true,
                    labels: {
                        color: 'rgb(255, 99, 132)'
                    }
                }
            }
        });
    }

    setTimeout(function () {
        initChart();
    }, 1000);


    function initChart2() {

        var table = $('#tbl_services');
        var json = [];
        var headers = [];
        var xlabel = [];
        var cmdata = [];
        var csdata = [];
        var pmdata = [];
        var psdata = [];

        for (var i = 0; i < table[0].rows[0].cells.length; i++) {
            headers[i] = table[0].rows[0].cells[i].innerHTML.toLowerCase().replace(/ /gi, '');
        }

        for (var i = 1; i < table[0].rows.length; i++) {
            var tableRow = table[0].rows[i];
            var rowData = {};
            for (var j = 0; j < tableRow.cells.length; j++) {
                rowData[headers[j]] = tableRow.cells[j].innerHTML;
                if (i < 13) {
                    xlabel[i - 1] = tableRow.cells[1].innerHTML + ' ' + tableRow.cells[0].innerHTML;
                    cmdata[i - 1] = tableRow.cells[2].innerHTML;
                    pmdata[i - 1] = tableRow.cells[3].innerHTML;
                }
            }
            json.push(rowData);
        }

        var color = Chart.helpers.color;
        var svcChartData = {
            labels: xlabel,
            datasets: [{
                type: 'line',
                label: 'Services',

                backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(), 
                data: cmdata
            },
            {
                type: 'line',
                label: 'Services PY',
                data: pmdata
            }]
        };

        var btx = document.getElementById("canvas2").getContext("2d");
        window.myBar = new Chart(btx, {
            type: 'line',
            data: svcChartData,
            fill: true,
            options: {
                responsive: true,
                legend: {
                    display: true,
                    labels: {
                        color: 'rgb(255, 99, 132)'
                    }
                }
            }
        });
    }

    setTimeout(function () {
        initChart2();
    }, 1000);

});

/*
 $(document).ready(function () {

    var randomScalingFactor = function () {
        return Math.round(Math.random() * 100);
    };

    var config = {
        type: 'pie',
        data: {
            datasets: [{
                data: [
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                    randomScalingFactor(),
                ],
                backgroundColor: [
                    window.chartColors.red,
                    window.chartColors.orange,
                    window.chartColors.yellow,
                    window.chartColors.green,
                    window.chartColors.blue,
                ],
                label: 'Dataset 1'
            }],
            labels: [
                "Red",
                "Orange",
                "Yellow",
                "Green",
                "Blue"
            ]
        },
        options: {
            responsive: true,
            legend: {
                display: true,
                labels: {
                    color: 'rgb(255, 99, 132)'
                }
            }
        }
    };

    var ctx = document.getElementById("chartjs_pie").getContext("2d");
    window.myPie = new Chart(ctx, config);

});
*/