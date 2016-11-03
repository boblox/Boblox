angular.module("umbraco").controller("grid.coloured-header.controller", function ($scope) {
    if ($scope.control.headerValue == null) {
        $scope.control.headerValue = {
            colour: "orange"
        }
    }
});