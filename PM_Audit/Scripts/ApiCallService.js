var ApiCallService = {
    ApiPath: "",
    Ajax: function (apiBase, url, data, successCallBack, httpMethod, async, headers, AjaxFailureCallback, dataType, contentType, cache) {
        //   var apiBase = "http://localhost:14529/";
        debugger;
        url = apiBase + url;
        httpMethod = httpMethod || "POST";
        dataType = dataType || "JSON";
        contentType = contentType || "application/json;";

        if (typeof async == "undefined") {
            async = true;
        }
        if (typeof cache == "undefined") {
            cache = false;
        }
        if (typeof AjaxFailureCallback == "undefined" || AjaxFailureCallback == null) {
            AjaxFailureCallback = ApiCallService.AjaxFailureCallback;
        }
        if (typeof headers == "undefined" || headers == null) {
            headers = { "Authorization": 'Bearer ' + localStorage.getItem('authToken') }
        }
        if (typeof responseTypeXHR == "undefined" || responseTypeXHR == null) {
            responseTypeXHR = "JSON";
        }

        var ajaxObj = $.ajax({
            type: httpMethod.toUpperCase(),
            url: url,
            headers: headers,
            data: data,
            dataType: dataType,
            async: async,
            cache: cache,
            success: successCallBack,
            error: function (err, type, httpStatus) {
                AjaxFailureCallback(err, type, httpStatus);
            }
        });

        return ajaxObj;
    },
    AjaxNative: function (apiBase, url, data, successCallBack, httpMethod, async, headers, AjaxFailureCallback, dataType, responseTypeXHR, contentType, cache) {
        //   var apiBase = "http://localhost:14529/";
        debugger;
        url = apiBase + url;
        httpMethod = httpMethod || "POST";
        dataType = dataType || "JSON";
        contentType = contentType || "application/json;";

        if (typeof async == "undefined") {
            async = true;
        }
        if (typeof cache == "undefined") {
            cache = false;
        }
        if (typeof AjaxFailureCallback == "undefined" || AjaxFailureCallback == null) {
            AjaxFailureCallback = ApiCallService.AjaxFailureCallback;
        }
        if (typeof headers == "undefined" || headers == null) {
            headers = { "Authorization": 'Bearer ' + localStorage.getItem('authToken') }
        }
        if (typeof responseTypeXHR == "undefined" || responseTypeXHR == null) {
            responseTypeXHR = "JSON";
        }

        var ajaxObj = $.ajax({
            type: httpMethod.toUpperCase(),
            url: url,
            headers: headers,
            data: data,
            dataType: dataType,
            async: async,
            cache: cache,
            xhrFields: {
                responseType: responseTypeXHR
            },
            success: successCallBack,
            error: function (err, type, httpStatus) {
                AjaxFailureCallback(err, type, httpStatus);
            }
        });

        return ajaxObj;
    },
    AjaxFailureCallback: function (err, type, httpStatus) {
        if (httpStatus == "Unauthorized" || httpStatus == "") {
            localStorage.clear();
            window.location.href = "/Login/LogOut";
        }
        var failureMessage = 'Error occurred in ajax call' + err.status + " - " + err.responseText + " - " + httpStatus;
        console.log(failureMessage);
    }
}