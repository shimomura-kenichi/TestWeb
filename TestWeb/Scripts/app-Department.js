$(function () {
    $("#DepartmentList").on("click", "tr", function (e) {
        // 対象行
        var $targetRow = $(this);
        // キーの取得
        var keyStr = $targetRow.find('span[name="SelectNumber"]').text();

        //フォームに対してサブミットする
        $('<input>').attr({
            'type': 'hidden',
            'name': 'SelectNumber',
            'value': keyStr
        }).appendTo($("#DepartmentForm"));
        $("#DepartmentForm").submit();
    });
});





