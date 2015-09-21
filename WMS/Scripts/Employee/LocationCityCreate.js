$(document).ready(function () {

    $('#LocID').empty();
    var convalue = $('#CityID').val();
    //var URL = '/WMS/Emp/LocationList';
    var URL = '/Emp/LocationList';
    $.getJSON(URL + '/' + convalue, function (data) {
        var items;
        $.each(data, function (i, state) {
            items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
            // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
        });
        $('#LocID').html(items);
            
        $('#LocDivID').show();
    });
    

    $('#CityID').change(function () {
        $('#LocID').empty();
       // var URL = '/WMS/Emp/LocationList';
       var URL = '/Emp/LocationList';
        var convalue = $('#CityID').val();
        $.getJSON(URL + '/' + convalue, function (data) {
            var items;
            $.each(data, function (i, state) {
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#LocID').html(items);
            $('#LocDivID').show();
        });
    });

});