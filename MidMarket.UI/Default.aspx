<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MidMarket.UI.Default" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title data-etiqueta="titulo_MidMarket">MidMarket</title>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="Styles/homepage.css">
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
            </Scripts>
        </asp:ScriptManager>

        <header>
            <div class="header-container">
                <h1 data-etiqueta="titulo_MidMarket">MidMarket</h1>
                <nav>
                    <div class="idioma-dropdown">
                        <a href="#" class="idioma-btn" data-etiqueta="drop_Idioma">Idioma</a>
                        <div class="idioma-menu">
                            <asp:Repeater ID="idiomaRepeater" runat="server">
                                <ItemTemplate>
                                    <a href="#" class="dropdown-item idioma-item" data-id='<%# Eval("Id") %>'>
                                        <%# Eval("Nombre") %>
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <a href="login.aspx" class="login-btn" data-etiqueta="btn_IniciarSesion">Iniciar Sesión</a>
                </nav>
            </div>
        </header>

        <div class="floating-icons">
            <img src="images/icono1.png" class="floating-icon" alt="Icono 1">
            <img src="images/icono2.png" class="floating-icon" alt="Icono 2">
            <img src="images/icono3.png" class="floating-icon" alt="Icono 3">
            <img src="images/icono4.png" class="floating-icon" alt="Icono 4">
            <img src="images/icono5.png" class="floating-icon" alt="Icono 5">
            <img src="images/icono6.png" class="floating-icon" alt="Icono 6">
            <img src="images/icono7.png" class="floating-icon" alt="Icono 7">
            <img src="images/icono8.png" class="floating-icon" alt="Icono 8">
        </div>

        <section id="features" class="features">
            <div class="features-container">
                <div class="image-wrapper">
                    <asp:Image ID="imgFeatures" runat="server" ImageUrl="~/images/brokerfinanciero.png" AlternateText="Financial Illustration" />
                </div>
                <div class="text-wrapper">
                    <h3 data-etiqueta="titulo_NuestrasCaracteristicas">Nuestras Características</h3>
                    <div class="feature-item">
                        <h4 data-etiqueta="titulo_AsesoriaFinanciera">Asesoría Financiera</h4>
                        <p data-etiqueta="desc_AsesoriaFinanciera">Expertos en brindar soluciones a medida para las necesidades específicas de su empresa.</p>
                    </div>
                    <div class="feature-item">
                        <h4 data-etiqueta="titulo_SeguridadConfianza">Seguridad y Confianza</h4>
                        <p data-etiqueta="desc_SeguridadConfianza">Protegemos sus transacciones con la más alta seguridad.</p>
                    </div>
                    <div class="feature-item">
                        <h4 data-etiqueta="titulo_OptimizacionRecursos">Optimización de Recursos</h4>
                        <p data-etiqueta="desc_OptimizacionRecursos">Maximizamos el rendimiento de sus inversiones con estrategias personalizadas.</p>
                    </div>
                </div>
            </div>
        </section>

        <section id="services" class="services">
            <div class="services-container">
                <div class="image-wrapper">
                    <asp:Image ID="imgServices" runat="server" ImageUrl="~/images/brokerfinanciero2.png" AlternateText="Nuestros Servicios" />
                </div>
                <div class="text-wrapper">
                    <h3 data-etiqueta="titulo_NuestrosServicios">Nuestros Servicios</h3>
                    <div class="feature-item">
                        <h4 data-etiqueta="titulo_FinanciamientoEmpresarial">Financiamiento Empresarial</h4>
                        <p data-etiqueta="desc_FinanciamientoEmpresarial">Proveemos las mejores opciones de financiamiento para apoyar el crecimiento de su empresa.</p>
                    </div>
                    <div class="feature-item">
                        <h4 data-etiqueta="titulo_GestionInversiones">Gestión de Inversiones</h4>
                        <p data-etiqueta="desc_GestionInversiones">Optimizamos sus inversiones para asegurar un rendimiento sólido y constante.</p>
                    </div>
                    <div class="feature-item">
                        <h4 data-etiqueta="titulo_PortafoliosInversion">Portafolios de Inversión Avanzados</h4>
                        <p data-etiqueta="desc_PortafoliosInversion">Contamos con Portafolios de Inversión dedicados exclusivamente para cada Empresa.</p>
                    </div>
                </div>
            </div>
        </section>

        <footer class="hero">
            <div class="hero-container">
                <h2 data-etiqueta="titulo_SolucionesFinancieras">Soluciones Financieras Exclusivas para Empresas Medianas</h2>
                <p data-etiqueta="desc_SolucionesFinancieras">Impulsando el crecimiento de su empresa con servicios financieros personalizados y eficaces.</p>
                <a href="mailto:contacto@midmarket.com.ar" class="cta-btn" data-etiqueta="btn_Contactanos">Contáctanos</a>
            </div>
        </footer>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll('.idioma-item').forEach(item => {
                item.addEventListener('click', function (event) {
                    event.preventDefault();
                    const idiomaId = event.target.getAttribute("data-id");
                    __doPostBack('ChangeLanguage', idiomaId);
                });
            });
        });

        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll("[data-etiqueta]").forEach(function (element) {
                const etiqueta = element.getAttribute("data-etiqueta");
                if (traducciones && traducciones[etiqueta]) {
                    element.textContent = traducciones[etiqueta];
                }
            });
        });
    </script>
</body>
</html>
