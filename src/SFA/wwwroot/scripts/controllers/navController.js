app.controller('navController', function ($scope, $rootScope, $window, $interval, $mdSidenav, $mdPanel, $translate, notificationService) {
    $scope.notifyCount = 0;
    $scope.notifications = [{}];
    $scope.navigateTo = function (category) {
        $window.location.href = "/nav/" + category;
    };
    $scope.openSideNavPanel = function () {
        $mdSidenav('right').open();
    };
    $scope.closeSideNavPanel = function () {
        $mdSidenav('right').close();
    };
    $scope.logout = function () {
        $window.location.href = '/logout';
    };
    $scope.changePass = function () {
        $window.location.href = '/user/changePassword';
    };

    $scope.openNotification = function (id, url) {
        notificationService.openByUserId(id).then(function (data) {
            $scope.status = data;
            notification();
            $window.location.href = url;
        });
    };

    $scope.showUserMenu = function ($event) {
        var template = '<div class="menu-panel" data-ng-controller="navController" md-whiteframe="4">' +
            '<div class="menu-content">' +
            '    <div class="menu-item">' +
            '      <button class="md-button" ng-click="changePass()">' +
            '        <span>Change Password</span>' +
            '      </button>' +
            '    </div>' +
            '    <md-divider></md-divider>' +
            '    <div class="menu-item">' +
            '      <button class="md-button" ng-click="logout()">' +
            '        <span>Log Out</span>' +
            '      </button>' +
            '    </div>' +
            '  </div>' +
            '</div>';

        var position = $mdPanel.newPanelPosition()
            .relativeTo($event.target.parentNode.parentNode)
            .addPanelPosition(
                $mdPanel.xPosition.ALIGN_END,
                $mdPanel.yPosition.BELOW
            );

        var config = {
            id: 'toolbar_user',
            attachTo: angular.element(document.body),
            //controller: this,
            //controllerAs: 'ctrl',
            template: template,
            position: position,
            panelClass: 'menu-panel-container',
            locals: {

            },
            openFrom: $event,
            focusOnOpen: false,
            zIndex: 900,
            propagateContainerEvents: true,
            clickOutsideToClose: true,
            groupName: ['toolbar', 'menus']
        };

        $mdPanel.open(config);
    };
    var tick = function () {
        $scope.clock = Date.now();
    };
    var notification = function () {
        notificationService.getUnOpenedNotification().then(function (resp) {
            $scope.notifications = resp.data.notifications;
            $scope.count = resp.data.count;
        });
        //messageLogService.getAllUnopened().then(function (messages) {
        //    $scope.notifyCount = messages.data.length;
        //});
    };

    tick();
    notification();
    $interval(tick, 1000);
    $interval(notification, 60000);
    //$interval(notify, 5000);
});