$(function () {
    $("#DepartmentList").on("click", "tr", function (e) {
        // 対象行
        var $targetRow = $(this);

        if ($targetRow.parent()[0].tagName === "TBODY") {
            // キーの取得
            var keyStr = $targetRow.find('span[name="SelectNumber"]').text();

            //フォームに対してサブミットする
            $('<input>').attr({
                'type': 'hidden',
                'name': 'SelectNumber',
                'value': keyStr
            }).appendTo($("#DepartmentForm"));
            $("#DepartmentForm").submit();

        }
    });
});





