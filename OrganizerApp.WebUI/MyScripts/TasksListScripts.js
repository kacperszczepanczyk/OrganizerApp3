$('.taskItem input:checkbox').on('click', function (e) {
    e.stopPropagation();
    var modelId = $(this).siblings('input[name="id"]').val();

    this.setAttribute('disabled', 'disabled');
    var selector = '#' + modelId;
    $(selector).css('text-decoration', "line-through");

    ajaxHelper.sendAjaxRequest("POST", doneTaskApiAction, "id=" + modelId);
});