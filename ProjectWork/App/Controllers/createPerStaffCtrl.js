angular.module("deptApp")
    .controller("createPerStaffCtrl", ($scope, $routeParams, apiUrl, ApiService) => {
        $scope.newPerStaffs = null;
        $scope.insertPerStaff = (frm) => {
            ApiService.post(apiUrl + "PermanentStaffs", $scope.newPerStaffs, null)
                .then(r => {
                    //console.log(r);
                    $scope.$emit('perStaffInserted', r.data);
                    $scope.newPerStaffs = {};
                    frm.$setPristine();
                    // $location.path("/companies");
                }, err => {
                    console.log(err);
                });
        }
    });