var Datepicker = {
    BindAutoCloseDatepicker: function (inputObject) {
        var $control = null;
        if (typeof inputObject == typeof 'String') {
            if (inputObject.toString().substr(0, 1) != '#') {
                inputObject = '#' + inputObject;
            }
            $control = $(inputObject);
        }
        else if (!(inputObject instanceof jQuery)) {
            $control = $(inputObject);
        }
        else {
            $control = inputObject;
        }
        if ($control == null || inputObject == null)
            throw Error("Cannot find control that has a string value of 'null'");
        else if (($control instanceof jQuery && $control.length == 0))
            throw Error("Cannot find control '" + inputObject.toString() + "'");
        
        $control.datepicker({
            todayHighlight: true,
            autoclose: true,
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true,
            yearRange: "-120:+20"
        });
    }
}
