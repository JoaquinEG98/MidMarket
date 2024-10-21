<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MidMarket.UI.Default" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>MidMarket</title>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="Styles/homepage.css">
</head>

<body>
    <form id="form1" runat="server">
        <header>
            <div class="header-container">
                <h1>MidMarket</h1>
                <nav>
                    <a href="login.aspx" class="login-btn">Iniciar Sesión</a>
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
                    <h3>Nuestras Características</h3>
                    <div class="feature-item">
                        <h4>Asesoría Financiera</h4>
                        <p>Expertos en brindar soluciones a medida para las necesidades específicas de su empresa.</p>
                    </div>
                    <div class="feature-item">
                        <h4>Seguridad y Confianza</h4>
                        <p>Protegemos sus transacciones con la más alta seguridad.</p>
                    </div>
                    <div class="feature-item">
                        <h4>Optimización de Recursos</h4>
                        <p>Maximizamos el rendimiento de sus inversiones con estrategias personalizadas.</p>
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
                    <h3>Nuestros Servicios</h3>
                    <div class="feature-item">
                        <h4>Financiamiento Empresarial</h4>
                        <p>Proveemos las mejores opciones de financiamiento para apoyar el crecimiento de su empresa.</p>
                    </div>
                    <div class="feature-item">
                        <h4>Gestión de Inversiones</h4>
                        <p>Optimizamos sus inversiones para asegurar un rendimiento sólido y constante.</p>
                    </div>
                    <div class="feature-item">
                        <h4>Portafolios de Inversión Avanzados</h4>
                        <p>Contamos con Portafolios de Inversión dedicados exclusivamente para cada Empresa.</p>
                    </div>
                </div>
            </div>
        </section>

        <footer class="hero">
            <div class="hero-container">
                <h2>Soluciones Financieras Exclusivas para Empresas Medianas</h2>
                <p>Impulsando el crecimiento de su empresa con servicios financieros personalizados y eficaces.</p>
                <a href="mailto:contacto@midmarket.com.ar" class="cta-btn">Contáctanos</a>
            </div>
        </footer>
    </form>
</body>
</html>
