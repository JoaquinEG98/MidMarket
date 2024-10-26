<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargarSaldo.aspx.cs" Inherits="MidMarket.UI.CargarSaldo" MasterPageFile="~/Site.Master" Title="Cargar Saldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-cargar-saldo" class="container-cargar-saldo">
        <form class="cargar-saldo-form" method="post" autocomplete="off">
            <h2>Cargar Saldo</h2>

            <div class="form-group-saldo">
                <label for="nombreTitular">Nombre del Titular</label>
                <asp:TextBox ID="nombreTitular" runat="server" ClientIDMode="Static" CssClass="form-control" autocomplete="off" />
            </div>

            <div class="form-group-saldo">
                <label for="dniTitular">DNI</label>
                <asp:TextBox ID="dniTitular" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="8" oninput="validarDNI()" autocomplete="off" />
                <asp:Label ID="lblDniValido" runat="server" CssClass="dni-validacion-label"></asp:Label>
            </div>

            <div class="form-group-saldo">
                <label for="numeroTarjeta">Número de Tarjeta</label>
                <div class="input-icon-container">
                    <asp:TextBox ID="numeroTarjeta" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="16" oninput="validarNumeroTarjeta()" placeholder="#### #### #### ####" autocomplete="off" />
                    <asp:Image ID="cardIcon" runat="server" CssClass="card-icon" ImageUrl="" AlternateText="Icono de tarjeta" Style="display: none;" />
                </div>
                <asp:Label ID="lblCardType" runat="server" CssClass="card-type-label"></asp:Label>
            </div>

            <div class="form-group-saldo">
                <label for="fechaVencimiento">Fecha de Vencimiento (MM/AA)</label>
                <asp:TextBox ID="fechaVencimiento" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="5" oninput="formatearFechaVencimiento()" placeholder="MM/AA" autocomplete="off" />
            </div>

            <div class="form-group-saldo">
                <label for="codigoSeguridad">Código de Seguridad (CVV)</label>
                <asp:TextBox ID="codigoSeguridad" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="4" autocomplete="off" />
            </div>

            <div class="form-group-saldo">
                <label for="monto">Monto a Cargar</label>
                <asp:TextBox ID="monto" runat="server" ClientIDMode="Static" CssClass="form-control" autocomplete="off" />
            </div>

            <asp:Label ID="lblResultado" runat="server" Text="" CssClass="resultado-label"></asp:Label>

            <asp:Button ID="btnCargarSaldo" runat="server" Text="Cargar Saldo" CssClass="submit-btn-saldo" OnClick="btnCargarSaldo_Click" OnClientClick="iniciarCarga(); return false;" />

            <div id="progressBar" class="progress-bar" style="display: none;">
                <div class="progress-fill"></div>
            </div>
        </form>
    </div>

    <style>
        .progress-bar {
            width: 100%;
            height: 8px;
            background-color: #ddd;
            border-radius: 5px;
            overflow: hidden;
            margin-top: 10px;
        }

        .progress-fill {
            height: 100%;
            background-color: #7e57c2;
            width: 0;
            transition: width 2s ease;
        }
    </style>

    <script type="text/javascript">
        function iniciarCarga() {
            var progressBar = document.getElementById("progressBar");
            var progressFill = document.querySelector(".progress-fill");

            progressBar.style.display = "block";
            progressFill.style.width = "0";

            setTimeout(function () {
                progressFill.style.width = "100%";
            }, 100);

            setTimeout(function () {
                __doPostBack('<%= btnCargarSaldo.UniqueID %>', '');
            }, 2100);
        }

        function formatearFechaVencimiento() {
            var fechaInput = document.getElementById("<%= fechaVencimiento.ClientID %>");
            var fecha = fechaInput.value.replace(/\D/g, '');

            if (fecha.length >= 3) {
                fecha = fecha.slice(0, 2) + '/' + fecha.slice(2, 4);
            }

            fechaInput.value = fecha;
        }

        function validarDNI() {
            var dni = document.getElementById("<%= dniTitular.ClientID %>").value;
            var lblDniValido = document.getElementById("<%= lblDniValido.ClientID %>");

            if (/^\d{8}$/.test(dni)) {
                lblDniValido.innerText = "DNI válido";
                lblDniValido.style.color = "green";
            } else {
                lblDniValido.innerText = "DNI inválido";
                lblDniValido.style.color = "red";
            }
        }

        function validarNumeroTarjeta() {
            var numeroTarjeta = document.getElementById("<%= numeroTarjeta.ClientID %>").value;
            var lblCardType = document.getElementById("<%= lblCardType.ClientID %>");
            var cardIcon = document.getElementById("<%= cardIcon.ClientID %>");
            var mensaje = "";
            var iconSrc = "";

            if (/^4[0-9]{12}(?:[0-9]{3})?$/.test(numeroTarjeta)) {
                mensaje = "Tarjeta VISA válida";
                lblCardType.style.color = "green";
                iconSrc = "images/visa.png";
            } else if (/^5[1-5][0-9]{14}$/.test(numeroTarjeta)) {
                mensaje = "Tarjeta MasterCard válida";
                lblCardType.style.color = "green";
                iconSrc = "images/mastercard.png";
            } else if (/^3[47][0-9]{13}$/.test(numeroTarjeta)) {
                mensaje = "Tarjeta AMEX válida";
                lblCardType.style.color = "green";
                iconSrc = "images/amex.png";
            } else if (numeroTarjeta.length > 0) {
                mensaje = "Número de tarjeta inválido";
                lblCardType.style.color = "red";
                iconSrc = "";
            } else {
                mensaje = "";
                iconSrc = "";
            }

            lblCardType.innerText = mensaje;

            if (iconSrc) {
                cardIcon.src = iconSrc;
                cardIcon.style.display = "inline-block";
            } else {
                cardIcon.style.display = "none";
            }
        }
    </script>
</asp:Content>
