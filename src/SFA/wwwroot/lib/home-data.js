$(document).ready(function () {

    var table = document.getElementById('tbl_missionary');
    var json = [];
    var headers = [];
    var xlabel = [];
    var wdata = [];
    var xdata = [];

    for (var i = 0; i < table.rows[0].cells.length; i++) {
        headers[i] = table.rows[0].cells[i].innerHTML.toLowerCase().replace(/ /gi, '');
    }

    for (var i = 1; i < table.rows.length; i++) {
        var tableRow = table.rows[i];
        var rowData = {};
        for (var j = 0; j < tableRow.cells.length; j++) {
            rowData[headers[j]] = tableRow.cells[j].innerHTML;
            if (i < 13) {
                xlabel[i-1] = tableRow.cells[1].innerHTML;
                wdata[i-1] = tableRow.cells[2].innerHTML;
                xdata[i-1] = tableRow.cells[3].innerHTML;
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
            data: wdata
        }, {
            type: 'bar',
            label: 'Missionaries (PY)',
            backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(),
            borderColor: window.chartColors.blue,
            data: wdata
        }, {
            type: 'bar',
            label: 'Schedules',
            backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
            borderColor: window.chartColors.green,
            data: xdata
        }, {
            type: 'bar',
            label: 'Schedules (PY)',
            backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
            borderColor: window.chartColors.green,
            data: xdata
        }]
    };

    var btx = document.getElementById("canvas1").getContext("2d");
    window.myBar = new Chart(btx, {
        type: 'bar',
        data: barChartData,
        options: {
            responsive: true
        }
    });

});