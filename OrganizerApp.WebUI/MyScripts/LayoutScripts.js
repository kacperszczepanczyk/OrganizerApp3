$(function () {
    $('#searchForm > input[type="submit"]').on('click', function () {
        var searchTypeInputs = $('input[name="searchType"]');
        var checkedSearchTypeInput = null;

        searchTypeInputs.each(function () {
            if ($(this).is(':checked')) {
                checkedSearchTypeInput = this;
                return false;
            }
        });

        var checkedSearchTypeInputValue = checkedSearchTypeInput.value;
        this.parentElement.setAttribute('action', checkedSearchTypeInputValue);
        return true;
    });
});

