$(function () {

    $("#disCheck").click(function () {
        if ($(this).prop("checked") === true) {
            $("#prDiscount").removeClass("d-none");
        } else {
            $("#prDiscount").addClass("d-none");
            $("#prDiscount").val("");

        }
    });
    $("#categoryId").on("change", function () {
            $.ajax({
                url: "https://texnogallery.az/Admin/AddProducts/GetCategory/" + $(this).val(),
                type: "POST",
                dataType: "Json",
                success: function (res) {
                    $("#SubCategoryId").empty();
                    if (res.status === 200) {
                        var result = res.response;
                        for (var i = 0; i < result.length; i++) {
                            $("#SubCategoryId").append("<option  value="+ result[i].Id + ">" + result[i].Name + "</option>");
                        }
                    }
                }
            });
    });

});
 