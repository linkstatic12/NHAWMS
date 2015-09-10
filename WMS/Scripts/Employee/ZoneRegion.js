$(document).ready(function () {
    setTimeout(function () {}, 1000);
    $('#RegionID').empty();
    var URL = '/Emp/RegionList';
    //var URL = '/Emp/CityList';
    $.getJSON(URL + '/' + $('#ZoneID').val(), function (data) {
        var items;
        $.each(data, function (i, state) {
            items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
            // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
        });
        $('#RegionID').html(items);

        $('#RegionsDivID').show();
        RippleEffectToCity();

    });
    

    $('#ZoneID').change(function () {
        $('#RegionID').empty();
        var URL = '/Emp/RegionList';
        //var URL = '/Emp/CityList';
        $.getJSON(URL + '/' + $('#ZoneID').val(), function (data) {
            var items;
            $.each(data, function (i, state) {
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#RegionID').html(items);
                  
            $('#RegionsDivID').show();
            RippleEffectToCity();
            
        });
    });
    function RippleEffectToLocation()
    {
        $('#LocID').empty();
        var convalue = $('#CityID').val();
        //var URL = '/WMS/Emp/LocationList';
        var URL = '/Emp/LocationList';
        $.getJSON(URL + '/' + convalue, function (data) {
            console.log(data);
           
            var items;
            $.each(data, function (i, state) {
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#LocID').html(items);
            $('#LocDivID').show();
        });
    }


    function RippleEffectToCity()
    {
        $('#CityID').empty();
        var URL = '/Emp/CityList';
        //var URL = '/Emp/CityList';
        $.getJSON(URL + '/' + $('#RegionID').val(), function (data) {
            var items;
            $.each(data, function (i, state) {
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#CityID').html(items);
            $('#CityDivID').show();
            RippleEffectToLocation();
        });
    }
});