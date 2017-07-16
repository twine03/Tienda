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
    $("a[data-modal-file]").on("click", function (e) {
        var URL = window.location.pathname;
        $.ajax({
            url: this.href,
            type: "get",
            success: function (result) {
                if (result === "")
                    location.href = "/Account/Login?ReturnUrl="+ URL;
                $("#myModalContent").html(result);
                $("#myModal").modal({ keyboard: true }, 'show');
                bindFormFile(this);
            }
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
                    if (result.url) {
                        $("#contenido-lista").empty();
                        $("#contenido-lista").load(result.url, function () {
                            bindcontrols();
                        });
                    } else {
                        crear_tabla(result.data);
                    }
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

function bindFormFile(dialog) {
    $('form', dialog).submit(function () {
        data = new FormData($(this)[0]);
        $.ajax({
            url: this.action,
            type: this.method,
            data: data,
            contentype: false,
            success: function (result) {
                if (result.success) {
                    $("#myModal").modal('hide');
                    if (result.url) {
                        $("#contenido-lista").empty();
                        $("#contenido-lista").load(result.url, function () {
                            bindcontrols();
                        });
                    } else {
                        crear_tabla(result.data);
                    }
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