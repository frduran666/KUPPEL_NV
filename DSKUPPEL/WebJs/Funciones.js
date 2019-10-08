$(document).ready(function () {

    $("#btnActualizarCorreo").click(function () {
        var correo = $('#txtCorreo').val();
        var pass1 = $("#txtContrasena").val();
        var pass2 = $("#txtContrasenaMod").val();
        if (correo == '' || pass1 == '') {
            alert('Debe Ingresar Campos Obligatorios');
        }
        else {
            if (pass1 == pass2) {
                $.ajax({
                    type: "POST",
                    url: "ModificaCorreo",
                    data: {
                        _VendCod: $("#txtVenCod").val(),
                        _Correo: $("#txtCorreo").val(),
                        _Contrasena: $("#txtContrasena").val()
                    },
                    success: function (data) {
                        if (data == 1) {
                            alert("Contraseña Actualizada");
                            location.reload();
                        }
                    },
                    async: true
                });
            }
            else {
                alert("Contraseñas no Coinciden");
            }
        }
    });
});