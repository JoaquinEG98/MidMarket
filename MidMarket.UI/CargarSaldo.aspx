<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargarSaldo.aspx.cs" Inherits="MidMarket.UI.CargarSaldo" MasterPageFile="~/Site.Master" Title="Cargar Saldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-cargar-saldo" class="container-cargar-saldo">
        <!-- Desactivar autocompletar en el formulario -->
        <form class="cargar-saldo-form" method="post" autocomplete="off">
            <h2>Cargar Saldo</h2>

            <div class="form-group-saldo">
                <label for="nombreTitular">Nombre del Titular</label>
                <input type="text" id="nombreTitular" required autocomplete="off" />
            </div>

            <div class="form-group-saldo">
                <label for="dniTitular">DNI</label>
                <input type="text" id="dniTitular" maxlength="8" oninput="validarDNI()" required autocomplete="off" />
                <span id="lblDniValido" class="dni-validacion-label"></span>
            </div>

            <div class="form-group-saldo">
                <label for="numeroTarjeta">Número de Tarjeta</label>
                <div class="input-icon-container">
                    <input type="text" id="numeroTarjeta" maxlength="16" oninput="validarNumeroTarjeta()" placeholder="#### #### #### ####" autocomplete="off" />
                    <img id="cardIcon" class="card-icon" src="" alt="Icono de tarjeta" style="display: none;" />
                </div>
                <span id="lblCardType" class="card-type-label"></span>
            </div>

            <div class="form-group-saldo">
                <label for="fechaVencimiento">Fecha de Vencimiento (MM/AA)</label>
                <input type="text" id="fechaVencimiento" placeholder="MM/AA" maxlength="5" required oninput="formatearFechaVencimiento()" autocomplete="off" />
            </div>

            <div class="form-group-saldo">
                <label for="codigoSeguridad">Código de Seguridad (CVV)</label>
                <input type="text" id="codigoSeguridad" maxlength="4" required autocomplete="off" />
            </div>

            <div class="form-group-saldo">
                <label for="monto">Monto a Cargar</label>
                <input type="text" id="monto" required autocomplete="off" />
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
        function formatearFechaVencimiento() {
            var fechaInput = document.getElementById("fechaVencimiento");
            var fecha = fechaInput.value.replace(/\D/g, ''); // Remover caracteres no numéricos

            // Insertar la barra automáticamente después de dos dígitos
            if (fecha.length >= 3) {
                fecha = fecha.slice(0, 2) + '/' + fecha.slice(2, 4);
            }

            fechaInput.value = fecha;
        }

        function validarDNI() {
            var dni = document.getElementById("dniTitular").value;
            var lblDniValido = document.getElementById("lblDniValido");

            // Verificar si el DNI tiene exactamente 8 dígitos numéricos
            if (/^\d{8}$/.test(dni)) {
                lblDniValido.innerText = "DNI válido";
                lblDniValido.style.color = "green";
            } else {
                lblDniValido.innerText = "DNI inválido";
                lblDniValido.style.color = "red";
            }
        }

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
