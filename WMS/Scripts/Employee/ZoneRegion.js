﻿$(document).ready(function () {

    

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
        });
    });

});