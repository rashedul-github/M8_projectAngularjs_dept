angular.module("deptApp")
    .controller("editDeptCtrl", ($scope, $routeParams, apiUrl, customUrl, ApiService) => {
        var id = $routeParams.id;
        $scope.deptToEdit = {};
        $scope.newDebts = {};
        $scope.newDeptPerStaff = {};
        $scope.newDeptConStaff = {};
        ApiService.get(customUrl + "Departments/" + id)
            .then(r => {
                $scope.deptToEdit = r.data;
                //$scope.deptToEdit.permanentStaff.JoinDate = new Date($scope.deptToEdit.permanentStaff.JoinDate);
                //$scope.companyToEdit.Eshtablished = $scope.companyToEdit.Eshtablished.substring(0, 10);
                angular.forEach($scope.deptToEdit.permanentStaff, (v, k) => {
                    v.JoinDate = v.JoinDate.substring(0, 10);
                });
                angular.forEach($scope.deptToEdit.contactualStaff, (v, k) => {
                    v.StartDate = v.StartDate.substring(0, 10);
                });

                //console.log(r.data.permanentStaff);
                //console.log(r.data.contactualStaff);

            }, err => {
                console.log(err);
            });
        $scope.delDeptPerStaff = (index) => {
            $scope.deptToEdit.permanentStaff.splice(index, 1);

        }
        $scope.delDeptConStaff = (index) => {
            $scope.deptToEdit.contactualStaff.splice(index, 1);

        }
        $scope.editDone = (frm) => {
            ApiService.put(apiUrl + "Departments/" + $scope.deptToEdit.DepartmentId, $scope.deptToEdit, null)
                .then(r => {
                    //console.log(r);
                    $scope.$emit("departmentUpdated", $scope.deptToEdit);
                    $scope.deptEditMsg = "Data updated successfuly.";
                    $scope.deptToEdit = {};
                    frm.$setPristine();
                }, err => {
                    console.log(err);
                })
        }
        //modal per staff
        $scope.openAddDeptPerStaffDialog = function () {
            //console.log($scope.companyToEdit.CompanyId)
            $("#addDeptPerStaffModal").modal('show');
        }
        $scope.AddPerStaffToDept = (frm) => {
            console.log(frm);
            $scope.newDeptPerStaff.DepartmentId = $scope.deptToEdit.DepartmentId;
            $scope.deptToEdit.permanentStaff.push($scope.newDeptPerStaff);
            frm.$setPristine();
            $scope.newDeptPerStaff = {};
            $("#addDeptPerStaffModal").modal('hide');
        }
        //modal con staff
        $scope.openAddDeptConStaffDialog = function () {
            //console.log($scope.companyToEdit.CompanyId)
            $("#addDeptConStaffModal").modal('show');
        }
        $scope.AddConStaffToDept = (frm) => {
            console.log(frm);
            $scope.newDeptConStaff.DepartmentId = $scope.deptToEdit.DepartmentId;
            $scope.deptToEdit.contactualStaff.push($scope.newDeptConStaff);
            frm.$setPristine();
            $scope.newDeptConStaff = {};  //right
            //$scope.newDeptPerStaff = {}; worng
            $("#addDeptConStaffModal").modal('hide');
        }

    });