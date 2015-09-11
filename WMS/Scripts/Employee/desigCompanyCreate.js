$(document).ready(function () {
    $('#PostedAs').empty();
    $('#DesigID').empty();
    var URL = '/Emp/DesignationList';
    //var URL = '/Emp/DesignationList';
    $.getJSON(URL + '/' + $('#CompanyID').val(), function (data) {
        var items;
        $.each(data, function (i, state) {
            items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
            // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
        });
        $('#DesigID').html(items);
        $('#PostedAs').html(items);
    });


    $('#CompanyID').change(function () {
        $('#PostedAs').empty();
        $('#DesigID').empty();
        var URL = '/Emp/DesignationList';
        //var URL = '/Emp/DesignationList';
        $.getJSON(URL + '/' + $('#CompanyID').val(), function (data) {
            var items;
            $.each(data, function (i, state) {
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            $('#DesigID').html(items);
            $('#PostedAs').html(items);
        });
    });

});