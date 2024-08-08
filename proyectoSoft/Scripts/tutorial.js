$(document).ready(function () {
    $('#startTutorial').click(function () {
        introJs().setOptions({
            steps: [
                {
                    intro: "Bienvenido al Sistema de Cotización de Cargas Dunamis. Te guiaremos a través de las diferentes secciones del sistema."
                },
                {
                    element: '.navbar-brand',
                    intro: "Este es el logo y título del sistema que te lleva de vuelta a la página de inicio desde cualquier parte del sistema."
                },
                {
                    element: '#sidebarToggle',
                    intro: "Este botón permite colapsar o expandir el menú de navegación lateral para maximizar el espacio de trabajo."
                },
                {
                    element: '.fa-user',
                    intro: "Aquí puedes acceder a las opciones del usuario, incluyendo cerrar sesión."
                },
                {
                    element: '#sidenavAccordion',
                    intro: "Este es el menú de navegación lateral donde puedes acceder a las diferentes funcionalidades del sistema."
                },
                {
                    element: '#collapseCotizarCarga',
                    intro: "En esta sección puedes cotizar diferentes tipos de carga y ver las cotizaciones guardadas."
                },
                {
                    element: '#collapseFacturar',
                    intro: "En esta sección puedes facturar las cargas cotizadas y ver las facturas guardadas."
                },
                {
                    element: '#collapseReportes',
                    intro: "En esta sección puedes ver diversos reportes relacionados con las cargas y facturas."
                },
                {
                    element: 'a[href*="Cliente"]',
                    intro: "Aquí puedes gestionar la información de los clientes."
                },
                {
                    element: '#collapseMantenimiento',
                    intro: "En esta sección puedes gestionar descuentos, impuestos, ubicaciones, contactos y tipos relacionados con la empresa."
                },
                {
                    element: '#collapseSeguridad',
                    intro: "En esta sección puedes gestionar usuarios, roles y permisos de acceso al sistema."
                },
                {
                    intro: "Esto concluye el tutorial del layout del sistema. ¡Gracias por tu atención!"
                }
            ],
            showProgress: true,
            exitOnOverlayClick: false,
            exitOnEsc: true,
            showStepNumbers: true,
            nextLabel: 'Siguiente',
            prevLabel: 'Anterior',
            skipLabel: 'Salir',
            doneLabel: 'Finalizar'
        }).start();
    });
});
