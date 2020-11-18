angular.module("deptApp")
    .controller("conStaffCtrl", ($scope, $routeParams, apiUrl, ApiService) => {
        var id = $routeParams.id;

        $scope.currentConStaffs = null;



        ApiService.get(apiUrl + "ContactualStaffs/" + id, null)
            .then(r => {
                r.data.StartDate = new Date(r.data.StartDate);
                $scope.currentConStaffs = r.data;
                //console.log(r.data);
            }, err => {
                console.log(err);
            });
        $scope.updateConStaffs = (frm) => {
            ApiService.put(apiUrl + "ContactualStaffs/" + id, $scope.currentConStaffs, null)
                .then(r => {
                    //console.log(r);
                    $scope.$emit('updatedConStaffs', $scope.currentConStaffs);
                    $scope.currentConStaffs = {};
                    frm.$setPristine();
                }, err => {
                    console.log(err);
                });
        }

    });