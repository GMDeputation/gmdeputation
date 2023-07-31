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
                backgroundColor: '#a7c4e7',
                borderColor: '#537092',
                data: cmdata
            }, {
                type: 'bar',
                label: 'Missionaries (PY)',
                backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(),
                borderColor: window.chartColors.blue,
                data: csdata
            }, {
                type: 'bar',
                label: 'Schedules',
                backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
                borderColor: window.chartColors.green,
                data: pmdata
            }, {
                type: 'bar',
                label: 'Schedules (PY)',
                backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
                borderColor: window.chartColors.green,
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

});