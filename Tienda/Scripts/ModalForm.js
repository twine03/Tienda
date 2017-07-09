$(function () {
    bindcontrols();
});

function bindcontrols() {
    $("a[data-modal]").on("click", function (e) {
        $("#myModalContent").load(this.href, function () {
            $("#myModal").modal({ keyboard: true }, 'show');
            bindForm(this);
        });
        return false;
    });
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $("#myModal").modal('hide');
                    $("#contenido-lista").empty();
                    $("#contenido-lista").load(result.url);
                    bindcontrols();
                    //window.location.reload();
                } else {
                    $("#myModalContent").html(result)
                    bindForm();
                }
                
            },
            error: function (result) {
                alert(result);
            }
        });
        return false;
    });
}