app.controller('changeUserDistricController', function ($scope, $window, $mdDialog, userService, roleService, districtService, sectionService) {
    $scope.selectedUsers = [];
    $scope.backToList = function () {
        $window.location.href = '/user';
    };
    $scope.filter = {
        options: {
            debounce: 500
        }
    };
    $scope.query = {
        name: '',
        limit: 20,
        order: 'name',
        page: 1
    };
    $scope.selected = [];
    $scope.showFilter = false;
    $scope.data = [];

    $scope.filter = function () {
        $scope.showFilter = true;
    };
    $scope.close = function () {
        $scope.showFilter = false;

        $scope.query = {
            name: '',
            limit: 20,
            order: 'name',
            page: 1
        };
        $scope.search();
    };


    $scope.$watch('query.name', function (newValue, oldValue) {
        if (!oldValue) {
            bookmark = $scope.query.page;
        }
        if (newValue !== oldValue) {
            $scope.query.page = 1;
        }
        if (!newValue) {
            $scope.query.page = bookmark;
        }

        $scope.search();
    });

    $scope.search = function () {
        $scope.promise = userService.search($scope.query).then(success);
    };
    function success(resp) {
        $scope.users = resp.data.result;
        $scope.count = resp.data.count;


        angular.forEach($scope.users, function (event) {
            if (event.districtId !== null && event.districtId !== undefined && event.districtId !== '00000000-0000-0000-0000-000000000000') {
                event.district = { "id": event.districtId, "name": event.districtName };
            }            
        });
    }

    $scope.searchDistrict = function (searchText) {
        $scope.district = [];

        if (searchText !== null && searchText !== undefined && searchText !== "") {
            searchText = searchText.toLowerCase();
        }

        angular.forEach($scope.districts, function (event) {
            if (event.name.toLowerCase().match(searchText)) {
                $scope.district.push(event);
            }
        });

        return $scope.district;
    };
    $scope.selectedDistrictName = function ($item, index) {
        $scope.users[index].districtId = $item !== null && $item !== undefined ? $item.id : '00000000-0000-0000-0000-000000000000';     

        if ($scope.users[index].districtId !== null && $scope.users[index].districtId !== undefined && $scope.users[index].districtId !== '00000000-0000-0000-0000-000000000000') {

            //$scope.users[index].sectionId = null;
            //$scope.users[index].sections = [];

            sectionService.getSectionByDistrictId($scope.users[index].districtId).then(function (resp) {
                $scope.users[index].sections = resp;
            });           
        }
    };


    $scope.save = function () {
        if ($scope.selectedUsers.length > 0) {
            var confirm = $mdDialog.confirm()
                .title('Church Admin')
                .textContent('Are You sure To Update All Selected User?')
                .ariaLabel('Alert Dialog')
                .cancel('No')
                .ok('Yes');

            $mdDialog.show(confirm).then(function () {
                $scope.isDisabled = true;

                userService.updateDistrictAndSection($scope.selectedUsers).then(function (resp) {
                    if (resp.data !== null && resp.data !== undefined && resp.data === "") {
                        $mdDialog.show(
                            $mdDialog.alert()
                                .clickOutsideToClose(false)
                                .title('Church Admin')
                                .textContent('Users Update successfully')
                                .ariaLabel('Alert Dialog')
                                .ok('OK')
                        ).then(function () {
                            $window.location.href = '/user/changeDistrict';
                            $scope.isDisabled = false;
                        });
                    }
                    else {
                        $mdDialog.show(
                            $mdDialog.alert()
                                .clickOutsideToClose(false)
                                .title('Church Admin')
                                .textContent('Failed To Update Users')
                                .ok('OK')
                        ).then(function () {
                            $scope.isDisabled = false;
                        });
                    }
                });
            }, function () {
                $mdDialog.hide();
            });
        }
        else {
            $mdDialog.show(
                $mdDialog.alert()
                    .clickOutsideToClose(false)
                    .title('Church Admin')
                    .textContent('Please Select User')
                    .ok('OK')
            );
        }
    };

    function init() {
        districtService.getAll().then(function (resp) {
            $scope.districts = resp.data;
        });
        roleService.getAll().then(function (resp) {
            $scope.roles = resp.data;
        });
        $scope.search();
    }
    init();
});