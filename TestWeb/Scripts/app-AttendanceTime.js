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
            position: { my: "left+10 top+30", at: "left+10 top+30", of: window },
            modal: true,
            open: function (event, ui) {
                openDialog(keyStrJson);
            },
            close: function (event, ui) {
                closeDialog();
                $("#DetailInput").dialog("destroy");
            }
        });
    });

    $("#AttendanceTimeList").on("click", 'button[name="AddBtn"]', function (e) {
        // 対象行
        $targetRow = $(this).closest("tr");
        // キーの取得
        var keyStr = $targetRow.find('span[name="KeyValueJson"]').text();
        var keyStrJson = JSON.parse(keyStr);

        // 現在の行の次に空行を追加
        $.ajax('/AttendanceTime/AddEmptyDetail',
            {
                type: 'post',
                data: keyStrJson,
            }
        ).done(function (result) {
            if (result.ErrorMessage !== undefined) {
                alert(result.ErrorMessage);
            } else {
                $.ajax('/AttendanceTime/ReDrowDetail',
                    {
                        type: 'get',
                        data: result,
                    }
                ).done(function (result2) {
                    // 明細の書き換え
                    $targetRow.after(result2);
                });
            }
        });
        e.stopPropagation();
    });

    $("#AttendanceTimeList").on("click", 'button[name="DelBtn"]', function (e) {
        // 対象行
        $targetRow = $(this).closest("tr");
        // キーの取得
        var keyStr = $targetRow.find('span[name="KeyValueJson"]').text();
        var keyStrJson = JSON.parse(keyStr);

        // 対象行を削除する
        $.ajax('/AttendanceTime/DeleteDetail',
            {
                type: 'post',
                data: keyStrJson,
            }
        ).done(function (result) {
            if (result.ErrorMessage !== undefined) {
                alert(result.ErrorMessage);
            } else {
                // 削除
                $targetRow.remove();
            }
        });
        e.stopPropagation();
    });

});

// ダイアログオープン
function openDialog(keyStrJson) {
    $("#DetailInput").load("/AttendanceTime/DetailInputIndex", $.param(keyStrJson), function () {
        $("#DetailInput").find('input[type!="hidden"],select').first().trigger("focus");
    });

}

// ダイアログクローズ
function closeDialog() {
}

// 出退勤時間登録後処理
function onDetailRegistResult(result) {
    // JSONで返却された場合は処理成功と判断する
    if ($.isPlainObject(result)) {
        var procBtn = result.ProcBtn;
        var $nextRow = $targetRow.next("tr");

        $.ajax('/AttendanceTime/ReDrowDetail',
            {
                type: 'get',
                data: result,
            }
        ).done(function (data) {
            // 明細の書き換え
            $targetRow.replaceWith(data);

            // イベント開放
            closeDialog();

            if (procBtn === "1") {
                $("#DetailInput").dialog("destroy");
            } else {
                // 次のデータ
                $targetRow = $nextRow;
                if ($targetRow.length > 0) {
                    // キーの取得
                    var keyStr = $targetRow.find('span[name="KeyValueJson"]').text();
                    var keyStrJson = JSON.parse(keyStr);

                    openDialog(keyStrJson);
                } else {
                    $("#DetailInput").dialog("destroy");
                }
            }

        });
    }    
}




