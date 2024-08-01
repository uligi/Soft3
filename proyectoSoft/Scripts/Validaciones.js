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
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
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
    $(".validar-cedula").on("input", function () {
        // Reemplazar todos los caracteres que no sean números
        this.value = this.value.replace(/[^0-9]/g, '');

        // Si la longitud es de 9, agregar un 0 al principio
        if (this.value.length === 9) {
            this.value = '0' + this.value;
        }

        // Formato de la cédula "x-xxxx-xxxx"
        let formattedValue = this.value.replace(/^(\d{1})(\d{4})(\d{4})$/, '$1-$2-$3');

        // Actualizar el valor del campo de entrada con el valor formateado
        $(this).val(formattedValue);

        // Validación del campo
        if (this.value.trim() && this.value.length === 11) { // Considerando los guiones, la longitud total es 11
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
            }).html("Este campo es obligatorio y debe tener el formato correcto <span style='color:red;'>&#10006;</span>");
        }
    });

    // Eliminar guiones antes de enviar el formulario
    $("form").on("submit", function () {
        let cedula = $(".validar-cedula").val();
        cedula = cedula.replace(/-/g, ''); // Eliminar guiones
        $(".validar-cedula").val(cedula); // Establecer el valor sin guiones
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

    // Validación de teléfono
    $(".validar-telefono").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
        if (this.value.length >= 8 && this.value.length <= 10) { // Longitud mínima de 8 y máxima de 10
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
            }).html("Debe contener entre 8 y 10 dígitos <span style='color:red;'>&#10006;</span>");
        }
    });
});
