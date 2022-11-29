app.controller('homeController', function ($scope, $window, $mdDialog, homeService) {
    
    $scope.grandTotalMember = 0;
    $scope.grandTotalDemand = 0;
    $scope.grandTotalCollection = 0;
    $scope.grandTotalOverDue = 0;
    $scope.grandTotalPrecloseAmount = 0;
    $scope.labels = [];
    
    $scope.series = ['Demand', 'Collection', 'Not Collection','Preclose'];
    $scope.options = {
        legend: {
            display: true,
            labels: {
                fontColor: 'rgb(255, 99, 132)'
            }
        }
    };
    $scope.data = [];

    $scope.pieLabels = ['Demand', 'Collection', 'Not Collection', 'Preclose'];
    $scope.pieData = [];


    function init() {
        homeService.getAll().then(function (resp) {
            $scope.homeData = resp.data; 

            angular.forEach(resp.data.toDaySummaryList, function (value) {                
                $scope.grandTotalMember = $scope.grandTotalMember + value.totalMember;
                $scope.grandTotalDemand = $scope.grandTotalDemand + value.totalDemand;
                $scope.grandTotalCollection = $scope.grandTotalCollection + value.totalCollection;
                $scope.grandTotalOverDue = $scope.grandTotalOverDue + value.totalOverDue;
                $scope.grandTotalPrecloseAmount = $scope.grandTotalPrecloseAmount + value.totalPrecloseAmount;
            });

            var demandSeries = [];
            var collectionSeries = [];
            var odSeries = [];
            var precloseSeries = [];
            angular.forEach(resp.data.chartSummaryList, function (value) {
                //var x = value;
                demandSeries.push(value.totalDemand);
                collectionSeries.push(value.totalCollection);
                odSeries.push(value.totalOverDue);
                precloseSeries.push(value.totalPrecloseAmount);

                $scope.labels.push(value.label);
            });

            $scope.data.push(demandSeries);
            $scope.data.push(collectionSeries);
            $scope.data.push(odSeries);
            $scope.data.push(precloseSeries);


            
                
            $scope.pieData.push((resp.data.monthlyChartSummaryList.totalDemand).toFixed(2));
            $scope.pieData.push((resp.data.monthlyChartSummaryList.totalCollection).toFixed(2));
            $scope.pieData.push((resp.data.monthlyChartSummaryList.totalOverDue).toFixed(2));
            $scope.pieData.push((resp.data.monthlyChartSummaryList.totalPrecloseAmount).toFixed(2));
            
        });
    }

    init();
});