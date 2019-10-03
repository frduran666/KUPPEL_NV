$(document).ready(function () {

    $("#btnActualizarCorreo").click(function () {
        var v = $("#txtVenCod").val();
        var c = $("#txtCorreo").val();
        var cc = $("#txtContrasena").val();
        var mascota = prompt("Validar Contraseña", "");
        if (mascota != cc) {
            alert("Contraseñas no Coinciden");
        }
        else {
            $.ajax({
                type: "POST",
                url: "ModificaCorreo",
                data: {
                    _VendCod: v,
                    _Correo: c,
                    _Contrasena: cc
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
    });

});