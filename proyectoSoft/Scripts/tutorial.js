$(document).ready(function () {
    $('#startTutorial').click(function () {
        introJs().setOptions({
            steps: [
                {
                    intro: "Bienvenido al Sistema de Cotizaci�n de Cargas Dunamis. Te guiaremos a trav�s de las diferentes secciones del sistema."
                },
                {
                    element: '.navbar-brand',
                    intro: "Este es el logo y t�tulo del sistema que te lleva de vuelta a la p�gina de inicio desde cualquier parte del sistema."
                },
                {
                    element: '#sidebarToggle',
                    intro: "Este bot�n permite colapsar o expandir el men� de navegaci�n lateral para maximizar el espacio de trabajo."
                },
                {
                    element: '.fa-user',
                    intro: "Aqu� puedes acceder a las opciones del usuario, incluyendo cerrar sesi�n."
                },
                {
                    element: '#sidenavAccordion',
                    intro: "Este es el men� de navegaci�n lateral donde puedes acceder a las diferentes funcionalidades del sistema."
                },
                {
                    element: '#collapseCotizarCarga',
                    intro: "En esta secci�n puedes cotizar diferentes tipos de carga y ver las cotizaciones guardadas."
                },
                {
                    element: '#collapseFacturar',
                    intro: "En esta secci�n puedes facturar las cargas cotizadas y ver las facturas guardadas."
                },
                {
                    element: '#collapseReportes',
                    intro: "En esta secci�n puedes ver diversos reportes relacionados con las cargas y facturas."
                },
                {
                    element: 'a[href*="Cliente"]',
                    intro: "Aqu� puedes gestionar la informaci�n de los clientes."
                },
                {
                    element: '#collapseMantenimiento',
                    intro: "En esta secci�n puedes gestionar descuentos, impuestos, ubicaciones, contactos y tipos relacionados con la empresa."
                },
                {
                    element: '#collapseSeguridad',
                    intro: "En esta secci�n puedes gestionar usuarios, roles y permisos de acceso al sistema."
                },
                {
                    intro: "Esto concluye el tutorial del layout del sistema. �Gracias por tu atenci�n!"
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
