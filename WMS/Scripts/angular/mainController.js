var app = angular.module('NHA', []);

app.controller('SummaryReports', function ($scope, $http) {
    $http({ method: 'GET', url: '/Category/GetFromToDateForSummaryReport' }).
      then(function (response) {
          $scope.GraphData = response.data;
          console.log($scope.GraphData);
          $scope.DateFrom = new Date($scope.GraphData[0]);
          $scope.DateTo =  new Date($scope.GraphData[1]);
      }, function (response) { });
    $scope.$watch('DateFrom', function () {
        $http({ method: 'POST', url: '/Category/GiveFromDateToSummaryReport', data: { date: $scope.DateFrom } }).
      then(function (response) {
          
          
      }, function (response) { });
       
    }, true);
    $scope.$watch('DateTo', function () {
        $http({ method: 'POST', url: '/Category/GiveToDateToSummaryReport', data: { date: $scope.DateTo } }).
      then(function (response) {
         
      }, function (response) { });
    }, true);

});