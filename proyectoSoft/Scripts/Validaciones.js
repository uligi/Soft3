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

    // Validaci�n de c�dula y formateo
    $(".validar-cedula").on("input", function () {
        // Reemplazar todos los caracteres que no sean n�meros
        this.value = this.value.replace(/[^0-9]/g, '');

        // Si la longitud es de 9, agregar un 0 al principio
        if (this.value.length === 9) {
            this.value = '0' + this.value;
        }

        // Formato de la c�dula "x-xxxx-xxxx"
        let formattedValue = this.value.replace(/^(\d{1})(\d{4})(\d{4})$/, '$1-$2-$3');

        // Actualizar el valor del campo de entrada con el valor formateado
        $(this).val(formattedValue);

        // Validaci�n del campo
        if (this.value.trim() && this.value.length === 11) { // Considerando los guiones, la longitud total es 11
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
            }).html("Este campo es obligatorio y debe tener el formato correcto <span style='color:red;'>&#10006;</span>");
        }
    });

    // Eliminar guiones antes de enviar el formulario
    $("form").on("submit", function () {
        let cedula = $(".validar-cedula").val();
        cedula = cedula.replace(/-/g, ''); // Eliminar guiones
        $(".validar-cedula").val(cedula); // Establecer el valor sin guiones
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

    // Validaci�n de tel�fono
    $(".validar-telefono").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
        if (this.value.length >= 8 && this.value.length <= 10) { // Longitud m�nima de 8 y m�xima de 10
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
            }).html("Debe contener entre 8 y 10 d�gitos <span style='color:red;'>&#10006;</span>");
        }
    });
});
