var $targetRow;

$(function () {
    $("#AttendanceTimeList").on("click", "tr", function (e) {
        // 対象行
        $targetRow = $(this);
        // キーの取得
        var keyStr = $targetRow.find('span[name="KeyValueJson"]').text();
        var keyStrJson = JSON.parse(keyStr);

        // ダイアログの表示
        $("#DetailInput").dialog({
            title: "出退勤時間入力画面",
            resizable: false,
            position: { my: "left+10 top+50", at: "left+10 top+50", of: window },
            modal: true,
            open: function (event,ui) {
                $("#DetailInput").load("/AttendanceTime/DetailInputIndex", $.param(keyStrJson), function () {
                    $("#DetailInput").find('input[type!="hidden"]').first().trigger("focus");
                });
            },
            close: function (event, ui) {
                $("#DetailInput").dialog("destroy");
            }
        });
    });

    $("#AttendanceTimeList").on("click", 'button[name="AddBtn"]', function (e) {
        // 対象行
        $targetRow = $(this).closest("tr");
        // キーの取得
        var keyStr = $targetRow.find('span[name="KeyValueJson"]').text();

        alert("add " + keyStr);
        e.stopPropagation();
    });

    $("#AttendanceTimeList").on("click", 'button[name="DelBtn"]', function (e) {
        // 対象行
        $targetRow = $(this).closest("tr");
        // キーの取得
        var keyStr = $targetRow.find('span[name="KeyValueJson"]').text();

        alert("del " + keyStr);
        e.stopPropagation();
    });

});

// 出退勤時間登録後処理
function onDetailRegistResult(result) {
    if ($.isPlainObject(result)) {

        // JSONで返却された場合は処理成功と判断する
        $("#DetailInput").dialog("destroy");

        $.ajax('/AttendanceTime/ReDrowDetail',
            {
                type: 'get',
                data: result,
            }
        ).done(function (data) {
            // 明細の書き換え
            $targetRow.replaceWith(data);
        });
    }    
}




