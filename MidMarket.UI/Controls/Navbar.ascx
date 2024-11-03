<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="MidMarket.UI.Navbar" %>

<nav class="navbar navbar-expand-lg navbar-light bg-light shadow-sm">
    <div class="container">
        <a class="navbar-brand fw-bold" runat="server" id="brandLink" href="/MenuPrincipal.aspx">MidMarket</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse d-flex justify-content-between" id="navbarNav">
            <ul class="navbar-nav mx-auto">
                <li class="nav-item dropdown" runat="server" id="menuPrincipalDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false" data-etiqueta="drop_MiCuenta">Mi Cuenta</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="menuPrincipalDropdown">
                        <li><a class="dropdown-item" runat="server" id="cargarSaldoLink" href="/CargarSaldo.aspx" data-etiqueta="drop_CargarSaldo">Cargar Saldo</a></li>
                        <li><a class="dropdown-item" runat="server" id="cambiarPasswordLink" href="/CambiarPassword.aspx" data-etiqueta="drop_CambiarPassword">Cambiar Contraseña</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="accionesDropDown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false" data-etiqueta="drop_Acciones">Acciones</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="accionesDropDown">
                        <li><a class="dropdown-item" runat="server" id="administrarAccionesLink" href="/AdministrarAcciones.aspx" data-etiqueta="drop_AdministrarAcciones">Administrar Acciones</a></li>
                        <li><a class="dropdown-item" runat="server" id="altaAccionesLink" href="/AltaAcciones.aspx" data-etiqueta="drop_AltaAcciones">Alta de Acciones</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="bonosDropDown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false" data-etiqueta="drop_Bonos">Bonos</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="bonosDropDown">
                        <li><a class="dropdown-item" runat="server" id="administrarBonosLink" href="/AdministrarBonos.aspx" data-etiqueta="drop_AdministrarBonos">Administrar Bonos</a></li>
                        <li><a class="dropdown-item" runat="server" id="altaBonosLink" href="/AltaBonos.aspx" data-etiqueta="drop_AltaBonos">Alta de Bonos</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="usuariosDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false" data-etiqueta="drop_Usuarios">Usuarios</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="usuariosDropdown">
                        <li><a class="dropdown-item" runat="server" id="usuariosLink" href="/Usuarios.aspx" data-etiqueta="drop_Usuarios">Usuarios</a></li>
                        <li><a class="dropdown-item" runat="server" id="altaUsuarioLink" href="/AltaUsuario.aspx" data-etiqueta="drop_AltaUsuario">Alta de Usuario</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="permisosDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false" data-etiqueta="drop_Permisos">Permisos</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="permisosDropdown">
                        <li><a class="dropdown-item" runat="server" id="asignarPatentesLink" href="/AsignarPatentes.aspx" data-etiqueta="drop_AsignarPatentes">Asignar Patentes</a></li>
                        <li><a class="dropdown-item" runat="server" id="asignarFamiliasLink" href="/AsignarFamilias.aspx" data-etiqueta="drop_AsignarFamilias">Asignar Familias</a></li>
                        <li><a class="dropdown-item" runat="server" id="desasignarPatentesLink" href="/DesasignarPatentes.aspx" data-etiqueta="drop_DesasignarPatentes">Desasignar Patentes</a></li>
                        <li><a class="dropdown-item" runat="server" id="desasignarFamiliasLink" href="/DesasignarFamilias.aspx" data-etiqueta="drop_DesasignarFamilias">Desasignar Familias</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="familiasDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false" data-etiqueta="drop_Familias">Familias</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="familiasDropdown">
                        <li><a class="dropdown-item" runat="server" id="administracionFamiliasLink" href="/AdministracionFamilias.aspx" data-etiqueta="drop_Familias">Administración de Familias</a></li>
                        <li><a class="dropdown-item" runat="server" id="altaFamiliaLink" href="/AltaFamilia.aspx" data-etiqueta="drop_AltaFamilia">Alta de Familia</a></li>
                    </ul>
                </li>
                <li class="nav-item" runat="server" id="administracionBD">
                    <a class="nav-link" href="/AdministracionBD.aspx" data-etiqueta="drop_AdministracionBD">Administración de Base de Datos</a>
                </li>
                <li class="nav-item" runat="server" id="bitacora">
                    <a class="nav-link" href="/Bitacora.aspx" data-etiqueta="drop_Bitacora">Bitácora</a>
                </li>
                <li class="nav-item dropdown" runat="server" id="transaccionesDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false" data-etiqueta="drop_Transacciones">Transacciones</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="transaccionesDropdown">
                        <li><a class="dropdown-item" runat="server" id="comprarAccionLink" href="/Compra.aspx" data-etiqueta="drop_CompraActivos">Compra de Activos</a></li>
                        <li><a class="dropdown-item" runat="server" id="venderAccionLink" href="/Venta.aspx" data-etiqueta="drop_VentaActivos">Venta de Activos</a></li>
                    </ul>
                </li>
                <li class="nav-item" runat="server" id="carritoDropdown">
                    <a class="nav-link" href="/Carrito.aspx" data-etiqueta="drop_Carrito">Carrito</a>
                </li>
                <li class="nav-item" runat="server" id="misTransaccionesDropdown">
                    <a class="nav-link" href="/Transacciones.aspx" data-etiqueta="drop_MisTransacciones">Mis Transacciones</a>
                </li>

                <li class="nav-item" runat="server" id="portafolioDrowndown">
                    <a class="nav-link" href="/Portafolio.aspx" data-etiqueta="drop_Portafolio">Portafolio</a>
                </li>

                <li class="nav-item dropdown" runat="server" id="idiomaDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false" data-etiqueta="drop_Idioma">Idioma</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="idiomaDropdown">
                        <asp:Repeater ID="idiomaRepeater" runat="server">
                            <ItemTemplate>
                                <li><a class="dropdown-item idioma-item" href="#" data-id='<%# Eval("Id") %>'><%# Eval("Nombre") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>

            </ul>
            <ul class="navbar-nav">
                <li class="nav-item" runat="server" id="logoutLink">
                    <a class="nav-link" href="/Logout.aspx" data-etiqueta="drop_Logout">Cerrar sesión</a>
                </li>
            </ul>
        </div>
    </div>
</nav>

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
