$(document).ready(function () {
    switch ($("#RoleID option:selected").html()) {

        case 'SuperUser': $('#LocationDiv').css('display', 'none');
            $('#ZoneDiv').css('display', 'none');
            $('#RegionDiv').css('display', 'none');
            $('#CityDiv').css('display', 'none');
            break;
        case 'Zone': $('#LocationDiv').css('display', 'none');
            $('#ZoneDiv').css('display', 'inline');
            $('#RegionDiv').css('display', 'none');
            $('#CityDiv').css('display', 'none');
            break;
        case 'Region': $('#LocationDiv').css('display', 'none');
            $('#ZoneDiv').css('display', 'none');
            $('#RegionDiv').css('display', 'inline');
            $('#CityDiv').css('display', 'none');
            break;
        case 'City': $('#LocationDiv').css('display', 'none');
            $('#ZoneDiv').css('display', 'none');
            $('#RegionDiv').css('display', 'none');
            $('#CityDiv').css('display', 'inline');
            break;
        case 'Location': $('#LocationDiv').css('display', 'inline');
            $('#ZoneDiv').css('display', 'none');
            $('#RegionDiv').css('display', 'none');
            $('#CityDiv').css('display', 'none');
            break;






    }
       

    $('#RoleID').change(function () {
        console.log($("#RoleID option:selected").html());
        switch ($("#RoleID option:selected").html())
        {

            case 'SuperUser': $('#LocationDiv').css('display', 'none');
                $('#ZoneDiv').css('display', 'none');
                $('#RegionDiv').css('display', 'none');
                $('#CityDiv').css('display', 'none');
                             break;
            case 'Zone': $('#LocationDiv').css('display', 'none');
                $('#ZoneDiv').css('display', 'inline');
                $('#RegionDiv').css('display', 'none');
                $('#CityDiv').css('display', 'none');
                         break;
            case 'Region': $('#LocationDiv').css('display', 'none');
                $('#ZoneDiv').css('display', 'none');
                $('#RegionDiv').css('display', 'inline');
                $('#CityDiv').css('display', 'none');
                          break;
            case 'City': $('#LocationDiv').css('display', 'none');
                $('#ZoneDiv').css('display', 'none');
                $('#RegionDiv').css('display', 'none');
                $('#CityDiv').css('display', 'inline');
                          break;
            case 'Location': $('#LocationDiv').css('display', 'inline');
                $('#ZoneDiv').css('display', 'none');
                $('#RegionDiv').css('display', 'none');
                $('#CityDiv').css('display', 'none');
                            break;






        }
    });

});