angular.module("deptApp")
    .controller("perStaffCtrl", ($scope, $routeParams, apiUrl, ApiService) => {
        var id = $routeParams.id;
       
        $scope.currentPerStaffs = null;


       
        ApiService.get(apiUrl + "PermanentStaffs/" + id, null)
            .then(r => {
                r.data.JoinDate = new Date(r.data.JoinDate);
                $scope.currentPerStaffs = r.data;
                //console.log(r.data);
            }, err => {
                console.log(err);
            });
        $scope.updatePerStaff = (frm) => {
            ApiService.put(apiUrl + "PermanentStaffs/" + id, $scope.currentPerStaffs, null)
                .then(r => {
                    console.log(r);
                    $scope.$emit('updatedPerStaff', $scope.currentPerStaffs);
                    $scope.currentPerStaffs = {};
                    frm.$setPristine();
                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });
        }
       
    });