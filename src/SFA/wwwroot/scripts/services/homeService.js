app.service('homeService', function ($http) {
    this.getAll = function () {
        return $http.get('/api/home/all');
    };

    this.getHoDashboardData = function (dashboard) {
        return $http.post('/api/home/getHoDashboardData', dashboard).then(function (resp) {
            return resp.data;
        });
    };

    this.dashboardSummary = function () {
        return $http.get('/api/home/dashboardSummary');
    };

    this.popoulateCP = function (dashboard) {
        return $http.post('/api/home/popoulateCP', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateCPByBranch = function (dashboard) {
        return $http.post('/api/home/popoulateCPByBranch', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateCPByLO = function (dashboard) {
        return $http.post('/api/home/popoulateCPByLO', dashboard).then(function (resp) {
            return resp.data;
        });
    };

    this.popoulateDisburse = function (dashboard) {
        return $http.post('/api/home/popoulateDisburse', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateDisburseByBranch = function (dashboard) {
        return $http.post('/api/home/popoulateDisburseByBranch', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateDisburseByLO = function (dashboard) {
        return $http.post('/api/home/popoulateDisburseByLO', dashboard).then(function (resp) {
            return resp.data;
        });
    };


    this.popoulateCollection = function (dashboard) {
        return $http.post('/api/home/popoulateCollection', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateCollectionByBranch = function (dashboard) {
        return $http.post('/api/home/popoulateCollectionByBranch', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateCollectionByLO = function (dashboard) {
        return $http.post('/api/home/popoulateCollectionByLO', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateCollectionByCP = function (dashboard) {
        return $http.post('/api/home/popoulateCollectionByCP', dashboard).then(function (resp) {
            return resp.data;
        });
    };


    this.popoulateOutstanding = function (dashboard) {
        return $http.post('/api/home/popoulateOutstanding', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateOutstandingByBranch = function (dashboard) {
        return $http.post('/api/home/popoulateOutstandingByBranch', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateOutstandingByLO = function (dashboard) {
        return $http.post('/api/home/popoulateOutstandingByLO', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateOutstandingByCP = function (dashboard) {
        return $http.post('/api/home/popoulateOutstandingByCP', dashboard).then(function (resp) {
            return resp.data;
        });
    }; 


    this.popoulateDemandColl = function (dashboard) {
        return $http.post('/api/home/popoulateDemandColl', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateDemandCollByBranch = function (dashboard) {
        return $http.post('/api/home/popoulateDemandCollByBranch', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateDemandCollByLO = function (dashboard) {
        return $http.post('/api/home/popoulateDemandCollByLO', dashboard).then(function (resp) {
            return resp.data;
        });
    };


    this.popoulateOverdue = function (dashboard) {
        return $http.post('/api/home/popoulateOverdue', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateOverdueByBranch = function (dashboard) {
        return $http.post('/api/home/popoulateOverdueByBranch', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateOverdueByLO = function (dashboard) {
        return $http.post('/api/home/popoulateOverdueByLO', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateOverdueByCP = function (dashboard) {
        return $http.post('/api/home/popoulateOverdueByCP', dashboard).then(function (resp) {
            return resp.data;
        });
    }; 


    this.popoulateCloseLoan = function (dashboard) {
        return $http.post('/api/home/popoulateCloseLoan', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateCloseLoanByBranch = function (dashboard) {
        return $http.post('/api/home/popoulateCloseLoanByBranch', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateCloseLoanByLO = function (dashboard) {
        return $http.post('/api/home/popoulateCloseLoanByLO', dashboard).then(function (resp) {
            return resp.data;
        });
    };
    this.popoulateCloseLoanCP = function (dashboard) {
        return $http.post('/api/home/popoulateCloseLoanCP', dashboard).then(function (resp) {
            return resp.data;
        });
    }; 
});