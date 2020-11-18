angular.module("deptApp")
    .controller("createConStaffCtrl", ($scope, apiUrl, ApiService) => {
        $scope.newConStaffs = null;
        $scope.insertConStaff = (frm) => {
            ApiService.post(apiUrl + "ContactualStaffs", $scope.newConStaffs, null)
                .then(r => {
                    $scope.$emit('conStaffsInserted', r.data);
                    $scope.newConStaffs = {};
                    frm.$setPristine();
                }, err => {
                    console.log(err);
                });
        }
    });