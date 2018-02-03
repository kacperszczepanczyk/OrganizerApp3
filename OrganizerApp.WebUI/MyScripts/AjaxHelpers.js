
var ajaxHelper = (function () {

    function _sendAjaxRequest(httpMethodType, requestUrl, queryString, onSuccess, onError, onBeforeSend) {
        var httpMethodType = httpMethodType === undefined ? "GET" : httpMethodType;
        if (requestUrl === undefined) {
            return false;
        }
        var queryString = queryString === undefined ? "" : queryString;
        var onSuccessFunc = onSuccess === undefined ? _defaultOnSuccess : onSuccess;
        var onErrorFunc = onError === undefined ? _defaultOnError : onError;
        var onBeforeSendFunc = onBeforeSend === undefined ? _defaultOnBeforeSend : onBeforeSend;


        $.ajax({
            type: httpMethodType,
            url: _buildRequestUrl(requestUrl, queryString),
            error: function (sthObj, statusInfo) {
                onErrorFunc(sthObj, statusInfo);
            },
            success: function (data, callback, sthObj) {
                onSuccessFunc(data, callback, sthObj);
            },
            beforeSend: function () {
                onBeforeSendFunc();
            }
        });
    }


    function _defaultOnSuccess(data, callback, sthObj) {

    }


    function _defaultOnError() {

    }


    function _defaultOnBeforeSend() {

    }


    function _buildRequestUrl(requestUrl, queryString) {
        queryString === undefined ? "" : queryString;
        var result = "";
        if (queryString) {
            result = requestUrl + "?" + queryString;
        } else {
            result = requestUrl;
        }
        return result;
    }

    return {
        sendAjaxRequest: _sendAjaxRequest
    }

})();



