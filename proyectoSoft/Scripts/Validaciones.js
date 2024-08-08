$(document).ready(function () {
    // Validación de números
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
            }).html("Campo válido <span style='color:green;'>&#10004;</span>");
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

    // Validación de letras
    $(".validar-letras").on("input", function () {
        // Modificación de la expresión regular para incluir letras con tildes
        this.value = this.value.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
        if (this.value.trim()) {
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().css({
                "color": "green",
                "font-weight": "bold"
            }).html("Campo válido <span style='color:green;'>&#10004;</span>");
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


    // Validación de cédula y formateo
    $(".validar-Cedula").on("input", function () {
        // Reemplazar cualquier carácter no numérico
        this.value = this.value.replace(/[^0-9]/g, '');

        // Limitar a 10 caracteres numéricos
        if (this.value.length > 10) {
            this.value = this.value.slice(0, 10);
        }

        if (this.value.trim()) {
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().css({
                "color": "green",
                "font-weight": "bold"
            }).html("Campo válido <span style='color:green;'>&#10004;</span>");
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






    // Validación de correo electrónico
    $(".validar-correo").on("input", function () {
        var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!re.test(this.value)) {
            this.setCustomValidity("Correo electrónico no válido");
            $(this).css({
                "border-color": "red",
                "background-color": "#f8d7da"
            });
            $(this).next(".mensajeError").show().text("Correo electrónico no válido").css({
                "color": "red",
                "font-weight": "bold"
            }).html("Correo electrónico no válido <span style='color:red;'>&#10006;</span>");
        } else {
            this.setCustomValidity("");
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().css({
                "color": "green",
                "font-weight": "bold"
            }).html("Correo válido <span style='color:green;'>&#10004;</span>");
        }
    });

    // Validación de campos obligatorios
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
                }).html("Campo válido <span style='color:green;'>&#10004;</span>");
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
            }).html("Campo válido <span style='color:green;'>&#10004;</span>");
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




    $(".validar-password").on("input", function () {
        var password = this.value;
        var re = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

        if (!re.test(password)) {
            $(this).css({
                "border-color": "red",
                "background-color": "#f8d7da"
            });
            $(this).next(".mensajeError").show().html("La contraseña debe tener al menos 8 caracteres, una letra mayúscula, una letra minúscula, un número y un carácter especial <span style='color:red;'>&#10006;</span>").css({
                "color": "red",
                "font-weight": "bold"
            });
        } else {
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().html("Contraseña válida <span style='color:green;'>&#10004;</span>").css({
                "color": "green",
                "font-weight": "bold"
            });
        }
    });

    $(".validar-telefono").on("input", function () {
        // Reemplazar cualquier carácter no numérico y limitar a 8 dígitos
        this.value = this.value.replace(/[^0-9]/g, '').slice(0, 8);

        // Validar que el número tenga 8 dígitos y empiece con un dígito entre 2 y 9
        if (this.value.length === 8 && /^[2-9]/.test(this.value)) {
            $(this).css({
                "border-color": "green",
                "background-color": "#d4edda"
            });
            $(this).next(".mensajeError").show().css({
                "color": "green",
                "font-weight": "bold"
            }).html("Campo válido <span style='color:green;'>&#10004;</span>");
        } else {
            $(this).css({
                "border-color": "red",
                "background-color": "#f8d7da"
            });
            $(this).next(".mensajeError").show().css({
                "color": "red",
                "font-weight": "bold"
            }).html("Debe contener 8 dígitos y empezar con un número entre 2 y 9 <span style='color:red;'>&#10006;</span>");
        }
    });
});
