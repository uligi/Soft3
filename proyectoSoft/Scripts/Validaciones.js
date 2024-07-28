$(document).ready(function () {
    // Validaci�n de n�meros
    $(".validar-numeros").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
        if (this.value.trim()) {
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().css({
                "color": "green",
                "font-weight": "bold"
            }).html("Campo v�lido <span style='color:green;'>&#10004;</span>");
        } else {
            $(this).css({
                "border-color": "red",
                "background-color": "#f8d7da"
            });
            $(this).next(".mensajeError").show().css({
                "color": "red",
                "font-weight": "bold"
            }).html("Este campo es obligatorio <span style='color:red;'>&#10006;</span>");
        }
    });

    // Validaci�n de letras
    $(".validar-letras").on("input", function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
        if (this.value.trim()) {
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().css({
                "color": "green",
                "font-weight": "bold"
            }).html("Campo v�lido <span style='color:green;'>&#10004;</span>");
        } else {
            $(this).css({
                "border-color": "red",
                "background-color": "#f8d7da"
            });
            $(this).next(".mensajeError").show().css({
                "color": "red",
                "font-weight": "bold"
            }).html("Este campo es obligatorio <span style='color:red;'>&#10006;</span>");
        }
    });

    // Validaci�n de c�dula
    $(".validar-cedula").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
        if (this.value.length > 10) this.value = this.value.slice(0, 10);
        if (this.value.trim() && this.value.length <= 10) {
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().css({
                "color": "green",
                "font-weight": "bold"
            }).html("Campo v�lido <span style='color:green;'>&#10004;</span>");
        } else {
            $(this).css({
                "border-color": "red",
                "background-color": "#f8d7da"
            });
            $(this).next(".mensajeError").show().css({
                "color": "red",
                "font-weight": "bold"
            }).html("Este campo es obligatorio y debe tener m�ximo 10 d�gitos <span style='color:red;'>&#10006;</span>");
        }
    });

    // Validaci�n de correo electr�nico
    $(".validar-correo").on("input", function () {
        var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!re.test(this.value)) {
            this.setCustomValidity("Correo electr�nico no v�lido");
            $(this).css({
                "border-color": "red",
                "background-color": "#f8d7da"
            });
            $(this).next(".mensajeError").show().text("Correo electr�nico no v�lido").css({
                "color": "red",
                "font-weight": "bold"
            }).html("Correo electr�nico no v�lido <span style='color:red;'>&#10006;</span>");
        } else {
            this.setCustomValidity("");
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().css({
                "color": "green",
                "font-weight": "bold"
            }).html("Correo v�lido <span style='color:green;'>&#10004;</span>");
        }
    });

    // Validaci�n de campos obligatorios
    $("form").on("submit", function () {
        var valid = true;
        $(this).find("input[required]").each(function () {
            if (!this.value.trim()) {
                valid = false;
                $(this).css({
                    "border-color": "red",
                    "background-color": "#f8d7da"
                });
                $(this).next(".mensajeError").show().text("Este campo es obligatorio").css({
                    "color": "red",
                    "font-weight": "bold"
                }).html("Este campo es obligatorio <span style='color:red;'>&#10006;</span>");
            } else {
                $(this).css({
                    "border-color": "green",
                    "background-color": "#d4edda"
                });
                $(this).next(".mensajeError").show().css({
                    "color": "green",
                    "font-weight": "bold"
                }).html("Campo v�lido <span style='color:green;'>&#10004;</span>");
            }
        });
        return valid;
    });

    // Resetear borde y mensaje de error al escribir
    $("input[required]").on("input", function () {
        if (this.value.trim()) {
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().css({
                "color": "green",
                "font-weight": "bold"
            }).html("Campo v�lido <span style='color:green;'>&#10004;</span>");
        } else {
            $(this).css({
                "border-color": "red",
                "background-color": "#f8d7da"
            });
            $(this).next(".mensajeError").show().css({
                "color": "red",
                "font-weight": "bold"
            }).html("Este campo es obligatorio <span style='color:red;'>&#10006;</span>");
        }
    });
});
