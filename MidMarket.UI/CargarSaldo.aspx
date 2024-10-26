<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargarSaldo.aspx.cs" Inherits="MidMarket.UI.CargarSaldo" MasterPageFile="~/Site.Master" Title="Cargar Saldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-cargar-saldo" class="container-cargar-saldo">
        <form class="cargar-saldo-form" method="post">
            <h2>Cargar Saldo</h2>

            <div class="form-group-saldo">
                <label for="nombreTitular">Nombre del Titular</label>
                <input type="text" id="nombreTitular" required />
            </div>

            <div class="form-group-saldo">
                <label for="dniTitular">DNI</label>
                <input type="text" id="dniTitular" required />
            </div>

            <div class="form-group-saldo">
                <label for="numeroTarjeta">Número de Tarjeta</label>
                
                <!-- Contenedor para el input y el ícono de tarjeta -->
                <div class="input-icon-container">
                    <input type="text" id="numeroTarjeta" maxlength="16" oninput="validarNumeroTarjeta()" placeholder="#### #### #### ####" />
                    <img id="cardIcon" class="card-icon" src="" alt="Icono de tarjeta" style="display: none;" />
                </div>

                <span id="lblCardType" class="card-type-label"></span> <!-- `span` para mensaje de tipo de tarjeta -->
            </div>

            <div class="form-group-saldo">
                <label for="fechaVencimiento">Fecha de Vencimiento (MM/AA)</label>
                <input type="text" id="fechaVencimiento" placeholder="MM/AA" required />
            </div>

            <div class="form-group-saldo">
                <label for="codigoSeguridad">Código de Seguridad (CVV)</label>
                <input type="text" id="codigoSeguridad" maxlength="4" required />
            </div>

            <div class="form-group-saldo">
                <label for="monto">Monto a Cargar</label>
                <input type="text" id="monto" required />
            </div>

            <asp:Label ID="lblResultado" runat="server" Text="" CssClass="resultado-label"></asp:Label>
            
            <asp:Button ID="btnCargarSaldo" runat="server" Text="Cargar Saldo" CssClass="submit-btn-saldo" OnClick="btnCargarSaldo_Click" />
            <div id="progressBar" class="progress-bar" style="display:none;">
                <div class="progress-fill"></div>
            </div>
        </form>
    </div>

    <!-- JavaScript de Validación en Tiempo Real y Selección de Icono -->
    <script type="text/javascript">
        function validarNumeroTarjeta() {
            var numeroTarjeta = document.getElementById("numeroTarjeta").value;
            var lblCardType = document.getElementById("lblCardType");
            var cardIcon = document.getElementById("cardIcon");
            var mensaje = "";
            var iconSrc = ""; // variable para almacenar la ruta del icono

            // Validación de tipo de tarjeta en tiempo real
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
                iconSrc = ""; // Limpiar icono si no es válida
            } else {
                mensaje = "";
                iconSrc = "";
            }

            lblCardType.innerText = mensaje; // Mostrar el mensaje de validación

            // Mostrar el icono si hay un tipo de tarjeta válido, ocultarlo si no
            if (iconSrc) {
                cardIcon.src = iconSrc;
                cardIcon.style.display = "inline-block";
            } else {
                cardIcon.style.display = "none";
            }
        }

        document.getElementById("<%= btnCargarSaldo.ClientID %>").onclick = function() {
            var progressBar = document.getElementById("progressBar");
            var progressFill = document.querySelector(".progress-fill");
            
            progressBar.style.display = "block";
            progressFill.style.width = "0";
            
            setTimeout(function() {
                progressFill.style.width = "100%";
            }, 100);

            setTimeout(function() {
                progressBar.style.display = "none";
                document.getElementById("<%= lblResultado.ClientID %>").innerText = "Saldo cargado correctamente.";
            }, 2000);
        };
    </script>
</asp:Content>
