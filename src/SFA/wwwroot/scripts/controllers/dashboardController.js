app.controller('dashboardController', function ($scope, $filter, $window, $location, $mdDialog, $interval, homeService) {
    $scope.templates = [
        {
            'key': 'DMTM',
            'name': 'District Missionary This Month',
            'url': 'templates/distrcitMissionaryThisMonth.html'
        },
        {
            'key': 'DMNM',
            'name': 'District Missionary Next Month',
            'url': 'templates/distrcitMissionaryNextMonth.html'
        },
        {
            'key': 'SMTM',
            'name': 'Section Missionary This Month',
            'url': 'templates/sectionMissionarythisMonth.html'
        },
        {
            'key': 'SMNM',
            'name': 'Section Missionary Next Month',
            'url': 'templates/sectionMissionaryNextMonth.html'
        },
        {
            'key': 'C',
            'name': 'Church Dashboard',
            'url': 'templates/church.html'
        }
    ];
    $scope.template = {};
    $scope.isShowPage = false;
    
    $scope.labels = [];
    $scope.series = ['Current', 'Previous'];
    $scope.options = {
        legend: {
            display: true,
            labels: {
                fontColor: 'rgb(255, 99, 132)'
            }
        }
    };   
    $scope.data1 = [];
    $scope.data2 = [];

    $scope.loadDashboard = function (key) {
        $scope.isShowPage = true;

        $scope.template = $filter('filter')($scope.templates, { 'key': key }, true)[0];
        $scope.template.url = $scope.template.url + '?r=' + Date.now();
    };

    $scope.getDistrictThisMonth = function () {
        homeService.getDistrictThisMonth().then(function (resp) {
            $scope.districtThisMonthDatas = resp.data;
        });
    };
    $scope.getDistrictNextMonth = function () {
        homeService.getDistrictNextMonth().then(function (resp) {
            $scope.districtNextMonthDatas = resp.data;
        });
    };
    $scope.getSectionThisMonth = function () {
        homeService.getSectionThisMonth().then(function (resp) {
            $scope.sectionThisMonthDatas = resp.data;
        });
    };
    $scope.getSectionNextMonth = function () {
        homeService.getSectionNextMonth().then(function (resp) {
            $scope.sectionNextMonthDatas = resp.data;
        });
    };

    function init() {
        homeService.getDashbordData().then(function (resp) {
            $scope.dashbordData = resp.data;

            var currentSeries1 = [];
            var previousSeries1 = [];
            $scope.data1 = [];
            $scope.data2 = [];
            $scope.labels = [];

            angular.forEach(resp.data.districtChartData, function (value) {
                
                currentSeries1.push(value.current);
                previousSeries1.push(value.previous);

                $scope.labels.push(value.monthName);
            });

            var currentSeries2 = [];
            var previousSeries2 = [];
            angular.forEach(resp.data.sectionChartData, function (value) {

                currentSeries2.push(value.current);
                previousSeries2.push(value.previous);
            });

            $scope.data1.push(currentSeries1);
            $scope.data1.push(previousSeries1);
            $scope.data2.push(currentSeries2);
            $scope.data2.push(previousSeries2);
        });
    }
    init();
    $interval(init, 60000);
});