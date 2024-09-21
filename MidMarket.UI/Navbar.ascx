<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="MidMarket.UI.Navbar" %>

<nav class="navbar navbar-expand-lg navbar-light bg-light shadow-sm">
    <div class="container">
        <a class="navbar-brand fw-bold" runat="server" href="/MenuPrincipal.aspx">MidMarket</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse d-flex justify-content-between" id="navbarNav">
            <ul class="navbar-nav mx-auto">
                <li class="nav-item dropdown" runat="server" id="menuPrincipalDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Mi Cuenta</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="menuPrincipalDropdown">
                        <li><a class="dropdown-item" href="#">Cambiar Password</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="accionesDropDown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Administrar Acciones</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="accionesDropDown">
                        <li><a class="dropdown-item" href="#">Alta Acciones</a></li>
                        <li><a class="dropdown-item" href="#">Modificar Acciones</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="bonosDropDown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Administrar Acciones</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="bonosDropDown">
                        <li><a class="dropdown-item" href="#">Alta Bonos</a></li>
                        <li><a class="dropdown-item" href="#">Modificar Bonos</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="usuariosDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Usuarios</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="usuariosDropdown">
                        <li><a class="dropdown-item" href="#">Usuarios</a></li>
                        <li><a class="dropdown-item" href="#">Alta de Usuario</a></li>
                        <li><a class="dropdown-item" href="#">Modificación de Usuario</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="permisosDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Permisos</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="permisosDropdown">
                        <li><a class="dropdown-item" href="/AsignarPatentes.aspx">Asignar Patentes</a></li>
                        <li><a class="dropdown-item" href="/AsignarFamilias.aspx">Asignar Familias</a></li>
                        <li><a class="dropdown-item" href="/DesasignarPatentes.aspx">Desasignar Patentes</a></li>
                        <li><a class="dropdown-item" href="/DesasignarFamilias.aspx">Desasignar Familias</a></li>
                    </ul>
                </li>
                <li class="nav-item dropdown" runat="server" id="familiasDropdown">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">Familias</a>
                    <ul class="dropdown-menu animate__animated animate__fadeInDown animate__faster" aria-labelledby="familiasDropdown">
                        <li><a class="dropdown-item" href="/AdministracionFamilias.aspx">Administración Familias</a></li>
                        <li><a class="dropdown-item" href="/AltaFamilia.aspx">Alta de Familia</a></li>
                    </ul>
                </li>
                <li class="nav-item" runat="server" id="administracionBD">
                    <a class="nav-link" href="/AdministracionBD.aspx">Administración de Base de Datos</a>
                </li>
                <li class="nav-item" runat="server" id="bitacora">
                    <a class="nav-link" href="/Bitacora.aspx">Bitácora</a>
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

<script>
    document.querySelectorAll('.nav-item.dropdown').forEach(function (dropdown) {
        dropdown.addEventListener('click', function (e) {
            if (window.innerWidth <= 992) {
                this.querySelector('.dropdown-menu').classList.toggle('show');
            }
        });
    });
</script>
