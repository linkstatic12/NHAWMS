$(document).ready(function () {

    $('#DesigID').empty();
    $('#PostedAs').empty();
    var selectedItemIDpost = document.getElementById("selectedPostAsIDHidden").value;
    
    var URL = '/Emp/DesignationList';
    //var URL = '/Emp/DesignationList';
    $.getJSON(URL + '/' + $('#CompanyID').val(), function (data) {
        var selectedItemID = document.getElementById("selectedDesigIDHidden").value;
        
        var items;
        $.each(data, function (i, state) {
            if (state.Value == selectedItemID)
                items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
            else
                items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
            
        });
        console.log(selectedItemID);
        $('#DesigID').html(items);
        var itemspostedas;
        $.each(data, function (i, state) {
            if (state.Value == selectedItemIDpost)
                itemspostedas += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
            else
                itemspostedas += "<option value='" + state.Value + "'>" + state.Text + "</option>";

        });

        console.log(selectedItemID);
        $('#PostedAs').html(itemspostedas);
    });


    $('#CompanyID').change(function () {
        $('#DesigID').empty();
        $('#PostedAs').empty();
        var URL = '/Emp/DesignationList';
        //var URL = '/Emp/DesignationList';
        $.getJSON(URL + '/' + $('#CompanyID').val(), function (data) {
            var selectedItemID = document.getElementById("selectedDesigIDHidden").value;
            var items;
            $.each(data, function (i, state) {
                if (state.Value == selectedItemID)
                    items += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
                else
                    items += "<option value='" + state.Value + "'>" + state.Text + "</option>";
                // state.Value cannot contain ' character. We are OK because state.Value = cnt++;
            });
            var selectedItemID = document.getElementById("selectedPostAsIDHidden").value;
            var itemspostedas;
            $.each(data, function (i, state) {
                if (state.Value == selectedItemID)
                    itemspostedas += "<option selected value='" + state.Value + "'>" + state.Text + "</option>";
                else
                    itemspostedas += "<option value='" + state.Value + "'>" + state.Text + "</option>";

            }); 

            $('#DesigID').html(items);
            $('#PostedAs').html(itemspostedas);
        });
    });

});