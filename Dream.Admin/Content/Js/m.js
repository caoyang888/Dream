$(function () {
    $(".ajax-do").click(function (e) {
        e.stopPropagation();
        var _this = $(this);
        var _message = _this.attr("message");
        if (!_message) {
            _message = "您确定要执行此操作吗？";
        }
        if (confirm(_message)) {
            $.getJSON(_this.attr("href"), {}, function (r) {
                if (r.errorCode == 0) {
                    if (r.message == "")
                        r.message = "操作成功！";
                    showMessage(r.message, function () { window.location.reload(); });
                }
                else {
                    showMessage(r.message);
                }
            });
        }
        return false;
    });
});


function showMessage(_msg, _callback) {
    alert(_msg);
    if (_callback) {
        _callback();
    }
}

$.fn.pagination = function (ops) {
    var _ops = {
        pageIndex: 1,
        pageCount: 1,
        pageSize: 20
    };
    var __pageObj = $(this);
    if (__pageObj.attr("pageindex")) {
        _ops.pageIndex = parseInt(__pageObj.attr("pageindex"));
    }
    if (__pageObj.attr("pagecount")) {
        _ops.pageCount = parseInt(__pageObj.attr("pagecount"));
    }
    if (__pageObj.attr("pagesize")) {
        _ops.pageSize = parseInt(__pageObj.attr("pagesize"));
    }
    $.extend(_ops, ops);
    if (_ops.pageCount <= 0)
        _ops.pageCount = 1;
    var __startPoint = 0;
    var __endPoint = 8;
    if (_ops.pageIndex < 0)
        _ops.pageIndex = 0;
    if (_ops.pageIndex > 4) {
        __startPoint = _ops.pageIndex - 4;
        __endPoint = _ops.pageIndex + 4;
    }
    if (__endPoint > _ops.pageCount) {
        if (_ops.pageCount > 8) {
            __startPoint = _ops.pageCount - 8;
        }
        __endPoint = _ops.pageCount - 1;
    }
    //if (__startPoint < 1)
    //    __startPoint = 0;
    var __pageul = $('<ul></ul>').addClass("pagination").addClass("pagination-lg");
    var __button;
    //__pageul.append(__button);
    __button = $('<li><a href="javascript:" class="page-start page-number" page-number="0" aria-label="Previous"><span aria-hidden="true">&laquo;</span></a></li>')
    if (_ops.pageIndex <= 1) {
        __button.addClass("disabled");
    }
    __pageul.append(__button);
    for (var __page = __startPoint; __page <= __endPoint; __page++) {
        var __currentButton = $('<li><a href="javascript:" class="page-num page-number" page-number="' + __page + '">' + (__page + 1) + '</a></li>');
        if (__page == _ops.pageIndex)
            __currentButton.addClass('active')
        __currentButton.appendTo(__pageul);
    }
    __button = $('<li><a href="javascript:" class="page-start page-number" page-number="' + (_ops.pageCount - 1) + '" aria-label="Next"><span aria-hidden="true">&raquo;</span></a></li>');
    if (_ops.pageIndex == _ops.pageCount - 1) {
        __button.addClass("disabled");
    }
    __pageul.append(__button);

    __pageul.find(".page-number").not(".active").click(function () {
        var __form = $(this).parentsUntil("form").parent().first();
        if (__form.length > 0) {
            $(__form).find("[name='PageIndex']").remove();
            var __pageIndexTxt = $("<input type='hidden' name='PageIndex' />").val(parseInt($(this).attr("page-number")));
            $(__form).append(__pageIndexTxt);
            $(__form).attr("method", "get");
            $(__form).submit();
        }
    });
    __pageObj.html(__pageul);
}