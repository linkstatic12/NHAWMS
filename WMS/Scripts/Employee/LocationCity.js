$(document).ready(function () {

    $('#LocID').empty();
    var convalue = $('#CityID').val();
    var URL = '/Emp/LocationList';
    //var URL = '/Emp/LocationList';
    $.getJSON(URL + '/' + convalue, function (data) {
        var selectedItemID = document.getElementById("selectedLocIdHidden").value;
        var items;
        $.each(data, function (i, state) {

            if (state.Value == selectedItemID)
                items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
            else
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
            // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
        });
        $('#LocID').html(items);
        $('#LocDivID').show();
    });


    $('#CityID').change(function () {
        $('#LocID').empty();
        var convalue = $('#CityID').val();
        //var URL = '/WMS/Emp/LocationList';
        var URL = '/Emp/LocationList';
        $.getJSON(URL + '/' + convalue, function (data) {
            console.log(data);
            var selectedItemID = document.getElementById("selectedLocIdHidden").value;
            var items;
            $.each(data, function (i, state) {
                if (state.Value == selectedItemID)
                    items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
                else
                    items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#LocID').html(items);
            $('#LocDivID').show();
        });
    });

});