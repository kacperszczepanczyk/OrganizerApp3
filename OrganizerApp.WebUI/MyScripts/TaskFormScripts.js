(function () {
    setDateTimePickerVisibility();
})();

function validationMsgWrapper(msg) {
    return '<div class="clientValidationError">' + msg + '</div>';
}

$('#executionTime').change(function () {
    setDateTimePickerVisibility();
})

$('#submitTaskForm').on('click', function (e) {
    $('.validationError').each(function (index, element) {
        element.innerText = "";
        var innerText = element.innerText
    });
    validateName(e);
    validatePriority(e);
    validateExecutionTime(e);
    validateStartTime(e);
});

$('input[name="name"]').blur(function (e) {
    validateName(e);
});

$('input[name="name"]').change(function (e) {
    validateName(e);
});

$('input[name="priority"]').change(function (e) {
    validatePriority(e);
});

$('input[name="priority"]').blur(function (e) {
    validatePriority(e);
});

$('select[name="executionTime"]').blur(function (e) {
    validateExecutionTime(e);
});

$('select[name="executionTime"]').change(function (e) {
    validateExecutionTime(e);
});

$('input[name="startTime"]').blur(function (e) {
    validateStartTime(e);
});

$('input[name="startTime"]').change(function (e) {
    validateStartTime(e);
});


function validateName(event) {
    var $validationErrorDiv = $('fieldset#taskNameArea > div.validationError')
    var nameArea = $('input[name="name"]').val();

    if (!(nameArea.length > 0)) {
        var innerHTML = $validationErrorDiv.html();
        if (innerHTML.indexOf(nameRequiredMessage) == -1) {
            $validationErrorDiv.append(validationMsgWrapper(nameRequiredMessage));
        }
        event.preventDefault();
    } else {
        $validationErrorDiv.empty();
    }
    return false;
};

function validatePriority(event) {
    var $validationErrorDiv = $('fieldset#taskPriorityArea > div.validationError');
    var $priorityArea = $('input[name="priority"]');
    var isSomeRadioChecked = false;
    $priorityArea.each(function (index, element) {
        if (element.checked) {
            isSomeRadioChecked = true;
            return false;
        }
    });
    if (!isSomeRadioChecked) {
        var innerHTML = $validationErrorDiv.html();
        if (innerHTML.indexOf(priorityRequiredMessage) == -1) {
            $validationErrorDiv.append(validationMsgWrapper(priorityRequiredMessage));
        }
        event.preventDefault();
    } else {
        $validationErrorDiv.empty();
    }

}

function validateExecutionTime(event) {
    var $validationErrorDiv = $('fieldset#taskExecutionTimeArea > div.validationError');
    var $executionTimeArea = $('select[name="executionTime"]').children();
    var isSelectedOptionEmpty = true;
    $executionTimeArea.each(function (index, element) {
        if (element.selected) {
            if (element.value.length > 0) {
                isSelectedOptionEmpty = false;
            }
            return false;
        }
    });
    if (isSelectedOptionEmpty) {
        event.preventDefault();
        var innerHTML = $validationErrorDiv.html();
        if (innerHTML.indexOf(executionTimeRequiredMessage) == -1) {
            $validationErrorDiv.append(validationMsgWrapper(executionTimeRequiredMessage));
        }
    } else {
        $validationErrorDiv.empty();
    }
}

function validateStartTime(event) {
    var $validationErrorDiv = $('div#taskStartTimeArea').find('.validationError');
    var selectedOptionValue = $('select[name="executionTime"]').find(':selected').val();
    if (selectedOptionValue == "scheduled") {
        var startTimeValue = $('input[name="startTime"]').val();
        if (startTimeValue == "") {
            event.preventDefault();
            var innerHTML = $validationErrorDiv.html();
            if (innerHTML.search(startTimeRequiredMessage) == -1) {
                $validationErrorDiv.append(validationMsgWrapper(startTimeRequiredMessage));
            }
        } else {
            $validationErrorDiv.empty();
        }
    } else {
        $validationErrorDiv.empty();
    }
}

function setDateTimePickerVisibility() {
    var value = $('#executionTime').find(':selected').attr('value');
    if (value == "scheduled") {
        $('#taskStartTimeArea').show();
    }
    else {
        $('#taskStartTimeArea').hide();
    }
}