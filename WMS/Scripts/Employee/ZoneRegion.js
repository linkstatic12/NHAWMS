$(document).ready(function () {
    setTimeout(function () {}, 1000);
   
    var URL = '/Emp/RegionList';
    //var URL = '/Emp/CityList';
    $.getJSON(URL + '/' + $('#ZoneID').val(), function (data) {
        var items;
        if(document.getElementById("selectedRegionIDHidden").value !=null)
        var selectedItemID = document.getElementById("selectedRegionIDHidden").value;
        $.each(data, function (i, state) {
              
            if (state.Value == selectedItemID && document.getElementById("selectedRegionIDHidden").value != null)
                items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
            else
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
           
            // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
        });
        $('#RegionID').empty();
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
            RippleEffectToCityOnChange();
            
        });
    });
    function RippleEffectToLocation()
    {
        $('#LocID').empty();
        var convalue = $('#CityID').val();
        //var URL = '/WMS/Emp/LocationList';
        var URL = '/Emp/LocationList';
        $.getJSON(URL + '/' + convalue, function (data) {
            var items;
            if (document.getElementById("selectedLocationIDHidden").value != null)
                var selectedItemID = document.getElementById("selectedLocationIDHidden").value;

            $.each(data, function (i, state) {
                if (state.Value == selectedItemID && document.getElementById("selectedLocationIDHidden").value != null)
                    items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
                else
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
            if (document.getElementById("selectedCityIDHidden").value != null)
                var selectedItemID = document.getElementById("selectedCityIDHidden").value;

            $.each(data, function (i, state) {
                if (state.Value == selectedItemID && document.getElementById("selectedCityIDHidden").value != null)
                    items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
                else
                    items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#CityID').html(items);
            $('#CityDivID').show();
            RippleEffectToLocation();
        });
    }

    function RippleEffectToLocationOnChange() {
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
    }


    function RippleEffectToCityOnChange() {
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
            RippleEffectToLocationOnChange();
        });
    }

});