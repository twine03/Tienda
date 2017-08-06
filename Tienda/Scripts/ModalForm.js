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
        $("#myModalContent").load(this.href, function () {
            $("#myModal").modal({ keyboard: true }, 'show');
            bindFormFile(this);
        });
        return false;
        //$.ajax({
        //    url: this.href,
        //    type: "get",
        //    success: function (result) {
        //        if (result === "")
        //            location.href = "/Account/Login?ReturnUrl="+ URL;
        //        $("#myModalContent").html(result);
        //        $("#myModal").modal({ keyboard: true }, 'show');
        //        bindFormFile($("#myModalContent"));
        //    }
        //});
        //return false;
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
                
            }
        });
        return false;
    });
}

function bindFormFile(dialog) {
    $('form', dialog).submit(function () {
        $(this).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($(this))
        myValidate($(this));
        var validator = $(this).validate(); // obtain validator
        var anyError = false;
        $(this).find("input").each(function () {
            if (!validator.element(this)) { // validate every input element inside this step
                anyError = true;
            }
        });

        if (anyError)
            return false; 
        

        data = new FormData($(this)[0]);
        $.ajax({
            url: this.action,
            type: this.method,
            data: data,
            processData: false,
            contentType: false,
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
                }
                else {
                    $("#myModalContent").html(result)
                    bindFormFile($("#myModalContent"));
                }

            }
        });
        return false;
    });
}