angular.module("deptApp")
    .controller("createDeptCtrl", ($scope, apiUrl, ApiService) => {
        /////////
        /// locals
        $scope.activeTab = 0;
        $scope.companyInsertMsg = "";

        ///Model Related
        $scope.newDept = {};
        $scope.newPer = {};
        $scope.permanentStaff = [];
        $scope.newCon = {};
        $scope.contactualStaff = [];
        $scope.DeptDone = () => {
            $scope.activeTab = 1;
        };
        $scope.perDone = function (frm) {
            $scope.permanentStaff.push($scope.newPer);
            $scope.newPer = {};
            frm.$setPristine();
        };
        $scope.perDel = (index) => {
            $scope.permanentStaff.splice(index, 1);
        };
        $scope.perPre = () => {
            $scope.activeTab = 0;
        };
        $scope.perNext = () => {
            $scope.activeTab = 2;
        };
        $scope.conPre = () => {
            $scope.activeTab = 1;
        };
        $scope.conDel = (index) => {
            $scope.contactualStaff.splice(index, 1);
        }
        $scope.conDone = function (frm) {
            $scope.contactualStaff.push($scope.newCon);

            $scope.newCon = {};
            frm.$setPristine();
        };
        $scope.saveAll = (frms) => {
            $scope.newDept.PermanentStaff = $scope.permanentStaff;
            $scope.newDept.ContactualStaff = $scope.contactualStaff;
            ApiService.post(apiUrl + "Departments", $scope.newDept, null)
                .then(r => {
                    $scope.companyInsertMsg = "Data inserted successfully."
                    $scope.$emit('deptInserted', r.data);
                    $scope.newDept = {};
                    $scope.newPer = {};
                    $scope.permanentStaff = [];
                    $scope.newCon = {};
                    $scope.contactualStaff = [];
                    $scope.activeTab = 0;
                    frms[0].$setPristine();
                    frms[1].$setPristine();
                    frms[2].$setPristine();
                    //console.log(frms);
                    //console.log(r.data);
                }, err => {
                    console.log(err);
                });
        };

    });