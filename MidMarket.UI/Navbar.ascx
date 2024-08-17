<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="MidMarket.UI.Navbar" %>

<nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark">
    <div class="container">
        <a class="navbar-brand" runat="server" href="~/">MidMarket</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav">
                <!-- Menú Principal -->
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="menuPrincipalDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Menú Principal
                    </a>
                    <ul class="dropdown-menu" aria-labelledby="menuPrincipalDropdown">
                        <li><a class="dropdown-item" href="#">Restablecer Password</a></li>
                    </ul>
                </li>

                <!-- Usuarios -->
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="usuariosDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Usuarios
                    </a>
                    <ul class="dropdown-menu" aria-labelledby="usuariosDropdown">
                        <li><a class="dropdown-item" href="#">Usuarios</a></li>
                        <li><a class="dropdown-item" href="#">Alta de Usuario</a></li>
                        <li><a class="dropdown-item" href="#">Modificación de Usuario</a></li>
                    </ul>
                </li>

                <!-- Permisos -->
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="permisosDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Permisos
                    </a>
                    <ul class="dropdown-menu" aria-labelledby="permisosDropdown">
                        <li><a class="dropdown-item" href="#">Permisos</a></li>
                        <li><a class="dropdown-item" href="#">Asignar Patentes</a></li>
                        <li><a class="dropdown-item" href="#">Asignar Familias</a></li>
                        <li><a class="dropdown-item" href="#">Desasignar Patentes</a></li>
                        <li><a class="dropdown-item" href="#">Desasignar Familias</a></li>
                    </ul>
                </li>

                <!-- Familias -->
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="familiasDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Familias
                    </a>
                    <ul class="dropdown-menu" aria-labelledby="familiasDropdown">
                        <li><a class="dropdown-item" href="#">Administración Familias</a></li>
                        <li><a class="dropdown-item" href="#">Alta de Familia</a></li>
                    </ul>
                </li>

                <!-- Administración de Base de Datos -->
                <li class="nav-item">
                    <a class="nav-link" href="#">Administración de Base de Datos</a>
                </li>

                <!-- Bitácora -->
                <li class="nav-item">
                    <a class="nav-link" href="#">Bitácora</a>
                </li>

                <!-- Cerrar sesión -->
                <li class="nav-item">
                    <a class="nav-link" href="/Logout.aspx">Cerrar sesión</a>
                </li>
            </ul>
        </div>
    </div>
</nav>