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
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Mi Cuenta</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="menuPrincipalDropdown">
                        <li><a class="dropdown-item" runat="server" id="cargarSaldoLink" href="/CargarSaldo.aspx">Cargar Saldo</a></li>
                        <li><a class="dropdown-item" runat="server" id="cambiarPasswordLink" href="/CambiarPassword.aspx">Cambiar Password</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="accionesDropDown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Acciones</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="accionesDropDown">
                        <li><a class="dropdown-item" runat="server" id="administrarAccionesLink" href="/AdministrarAcciones.aspx">Administrar Acciones</a></li>
                        <li><a class="dropdown-item" runat="server" id="altaAccionesLink" href="/AltaAcciones.aspx">Alta Acciones</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="bonosDropDown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Bonos</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="bonosDropDown">
                        <li><a class="dropdown-item" runat="server" id="administrarBonosLink" href="/AdministrarBonos.aspx">Administrar Bonos</a></li>
                        <li><a class="dropdown-item" runat="server" id="altaBonosLink" href="/AltaBonos.aspx">Alta Bonos</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="usuariosDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Usuarios</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="usuariosDropdown">
                        <li><a class="dropdown-item" runat="server" id="usuariosLink" href="/Usuarios.aspx">Usuarios</a></li>
                        <li><a class="dropdown-item" runat="server" id="altaUsuarioLink" href="/AltaUsuario.aspx">Alta de Usuario</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="permisosDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Permisos</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="permisosDropdown">
                        <li><a class="dropdown-item" runat="server" id="asignarPatentesLink" href="/AsignarPatentes.aspx">Asignar Patentes</a></li>
                        <li><a class="dropdown-item" runat="server" id="asignarFamiliasLink" href="/AsignarFamilias.aspx">Asignar Familias</a></li>
                        <li><a class="dropdown-item" runat="server" id="desasignarPatentesLink" href="/DesasignarPatentes.aspx">Desasignar Patentes</a></li>
                        <li><a class="dropdown-item" runat="server" id="desasignarFamiliasLink" href="/DesasignarFamilias.aspx">Desasignar Familias</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="familiasDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Familias</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="familiasDropdown">
                        <li><a class="dropdown-item" runat="server" id="administracionFamiliasLink" href="/AdministracionFamilias.aspx">Administración Familias</a></li>
                        <li><a class="dropdown-item" runat="server" id="altaFamiliaLink" href="/AltaFamilia.aspx">Alta de Familia</a></li>
                    </ul>
                </li>
                <li class="nav-item" runat="server" id="administracionBD">
                    <a class="nav-link" href="/AdministracionBD.aspx">Administración de Base de Datos</a>
                </li>
                <li class="nav-item" runat="server" id="bitacora">
                    <a class="nav-link" href="/Bitacora.aspx">Bitácora</a>
                </li>
                <li class="nav-item dropdown" runat="server" id="transaccionesDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Transacciones</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="transaccionesDropdown">
                        <li><a class="dropdown-item" runat="server" id="comprarAccionLink" href="/Compra.aspx">Compra de Activos</a></li>
                        <li><a class="dropdown-item" runat="server" id="venderAccionLink" href="/Venta.aspx">Venta de Activos</a></li>
                    </ul>
                </li>
                <li class="nav-item" runat="server" id="carritoDropdown">
                    <a class="nav-link" href="/Carrito.aspx">Carrito</a>
                </li>
                <li class="nav-item" runat="server" id="misTransaccionesDropdown">
                    <a class="nav-link" href="/Transacciones.aspx">Mis Transacciones</a>
                </li>

                <li class="nav-item" runat="server" id="portafolioDrowndown">
                    <a class="nav-link" href="/Portafolio.aspx">Portafolio</a>
                </li>
            </ul>
            <ul class="navbar-nav">
                <li class="nav-item" runat="server" id="logoutLink">
                    <a class="nav-link" href="/Logout.aspx">Cerrar sesión</a>
                </li>
            </ul>
        </div>
    </div>
</nav>
