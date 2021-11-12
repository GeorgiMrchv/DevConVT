
(function() {

    var dateFormat = "YYYY-MM-DD";

    // Controller
    function multipleDatePicker($scope) {

        $scope.dates = ConvertToObjects($scope.model.value);

        $scope.$watch('dates', function(newValue) {
            $scope.model.value = ConvertToStrings(newValue);
        }, true);

    };

    function ConvertToObjects(dates) {
        var stringObjects = [];
        for (var i = 0; i < dates.length; i++) {
            stringObjects.push(moment(dates[i], dateFormat));
        }
        return stringObjects;
    }

    function ConvertToStrings(dates) {
        var stringDates = [];
        for (var i = 0; i < dates.length; i++) {
            stringDates.push(dates[i].format(dateFormat));
        }
        return stringDates;
    }


    // Register the controller
    var app = angular.module("umbraco")
    app.requires.push('multipleDatePicker');
    app.controller('multipleDatePicker', multipleDatePicker);
    
})();
