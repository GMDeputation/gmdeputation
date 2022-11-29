app.controller('selectionController', function ($scope, $window, finYearService, branchService, userService) {
    $scope.selParams = {
        'userId': '',
        'branchId': '',
        'finYearId': '',
        'workingDate': new Date()
    };

    $scope.set = function () {
        var t = new Date($scope.selParams.workingDate).toUTCString();
        $scope.selParams.workingDate = t; //new Date(t.getFullYear(), t.getMonth() + 1, t.getDate());
        userService.set($scope.selParams).then(function (resp) {
            if (resp.status === 200) {
                $window.location.href = '/home';
            }
        });
    };
    $scope.changeFinYear = function () {
        angular.forEach($scope.finYears, function (value, key) {
            if (value.id === $scope.selParams.finYearId) {
                $scope.finYearStartDate = moment(value.startDate).toDate();
                $scope.finYearEndDate = moment(value.endDate).toDate();
                $scope.selParams.workingDate = moment(value.startDate).toDate();
                return;
            }
        });
    };

    $scope.$watch('selParams.userId', function (newValue, oldValue) {
        //if (newValue !== oldValue) {
            branchService.getAllByUser(newValue).then(function (resp) {
                $scope.branches = resp.data;
                $scope.selParams.branchId = resp.data[0].id;

                branchService.getWorkingDate($scope.selParams.branchId).then(function (data) {
                    $scope.maxDate = new Date(data);  
                    $scope.selParams.workingDate = new Date(data);
                });
            });
        //}
    });

    function init() {
        finYearService.getAll().then(function (resp) {
            $scope.finYears = resp.data;
        });
       
    }

    init();
});