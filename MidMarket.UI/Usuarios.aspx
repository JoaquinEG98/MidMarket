<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="MidMarket.UI.Usuarios" MasterPageFile="~/Site.Master" Title="Administración de Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-usuarios">
        <h2 data-etiqueta="titulo_AdministracionUsuarios">Administración de Usuarios</h2>

        <table id="tablaUsuarios">
            <thead>
                <tr>
                    <th data-etiqueta="table_RazonSocial">Razón Social</th>
                    <th data-etiqueta="table_CUIT">CUIT</th>
                    <th data-etiqueta="table_Familia">Familia</th>
                    <th data-etiqueta="btn_VerMas">Ver Más</th>
                    <th data-etiqueta="btn_Modificar">Modificar</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var cliente in Clientes) { %>
                <tr>
                    <td><%= cliente.RazonSocial %></td>
                    <td><%= cliente.CUIT %></td>
                    <td><%= cliente.Permisos[0].Nombre %></td>
                    <td>
                        <button type="button" class="submit-btn-usuarios ver-detalle-btn" onclick="abrirModalDetalle('<%= cliente.Id %>')" data-etiqueta="btn_VerMas">Ver Más</button>
                    </td>
                    <td>
                        <button type="button" class="submit-btn-usuarios modificar-btn" onclick="window.location.href='<%= ResolveUrl("~/ModificarUsuario.aspx?id=" + cliente.Id) %>';" data-etiqueta="btn_Modificar">Modificar</button>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>

    <div id="usuariosModal" class="usuarios-modal">
        <div class="usuarios-modal-content">
            <div class="usuarios-modal-header">
                <h5 class="usuarios-modal-title" data-etiqueta="modal_TituloDetallesUsuario">Detalles del Usuario</h5>
                <span class="usuarios-close" onclick="cerrarModal()">&times;</span>
            </div>
            <div class="usuarios-modal-body">
                <p><strong data-etiqueta="modal_ID">- ID:</strong> <span id="detalleId"></span></p>
                <p><strong data-etiqueta="modal_Email">- Email:</strong> <span id="detalleEmail"></span></p>
                <p><strong data-etiqueta="modal_RazonSocial">- Razón Social:</strong> <span id="detalleRazonSocial"></span></p>
                <p><strong data-etiqueta="modal_CUIT">- CUIT:</strong> <span id="detalleCuit"></span></p>
                <p><strong data-etiqueta="modal_NumeroCuenta">- Número de Cuenta:</strong> <span id="detalleNumeroCuenta"></span></p>
                <p><strong data-etiqueta="modal_Saldo">- Saldo:</strong> $<span id="detalleSaldo"></span></p>
                <div class="seguridad-container">
                    <p><strong data-etiqueta="modal_Familias">- Familias y Patentes:</strong></p>
                    <div id="detalleFamilias"></div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var clientesData = <%= ViewState["ClientesJson"] %>;

        function abrirModalDetalle(clienteId) {
            event.preventDefault();
            event.stopPropagation();

            var cliente = clientesData.find(c => c.Id == clienteId);
            if (!cliente) return;

            document.getElementById('detalleId').textContent = cliente.Id;
            document.getElementById('detalleEmail').textContent = cliente.Email;
            document.getElementById('detalleRazonSocial').textContent = cliente.RazonSocial;
            document.getElementById('detalleCuit').textContent = cliente.CUIT;
            document.getElementById('detalleNumeroCuenta').textContent = cliente.Cuenta.NumeroCuenta;
            document.getElementById('detalleSaldo').textContent = cliente.Cuenta.Saldo;

            var detalleFamilias = document.getElementById('detalleFamilias');
            detalleFamilias.innerHTML = '';

            cliente.Permisos.forEach(function (familia) {
                if (familia.Permiso === 0) {
                    var familiaContainer = document.createElement('div');
                    familiaContainer.className = 'familia-container';

                    var familiaTitle = document.createElement('p');
                    familiaTitle.innerHTML = `<strong>${familia.Nombre}</strong>`;
                    familiaContainer.appendChild(familiaTitle);

                    var patentesList = document.createElement('ul');
                    familia.Hijos.forEach(function (patente) {
                        if (patente.Permiso !== 0) {
                            var patenteItem = document.createElement('li');
                            patenteItem.textContent = patente.Nombre;
                            patentesList.appendChild(patenteItem);
                        }
                    });

                    familiaContainer.appendChild(patentesList);
                    detalleFamilias.appendChild(familiaContainer);
                }
            });

            var permisosSueltos = cliente.Permisos.filter(p => p.Permiso !== 0);
            if (permisosSueltos.length > 0) {
                var sueltosContainer = document.createElement('div');
                sueltosContainer.className = 'familia-container';

                var sueltosTitle = document.createElement('p');
                sueltosTitle.innerHTML = `<strong>Patentes Sueltas</strong>`;
                sueltosContainer.appendChild(sueltosTitle);

                var sueltosList = document.createElement('ul');
                permisosSueltos.forEach(function (patente) {
                    var patenteItem = document.createElement('li');
                    patenteItem.textContent = patente.Nombre;
                    sueltosList.appendChild(patenteItem);
                });

                sueltosContainer.appendChild(sueltosList);
                detalleFamilias.appendChild(sueltosContainer);
            }

            document.getElementById('usuariosModal').style.display = 'block';
        }

        function cerrarModal() {
            document.getElementById('usuariosModal').style.display = 'none';
        }

        window.onclick = function (event) {
            var modal = document.getElementById('usuariosModal');
            if (event.target == modal) {
                cerrarModal();
            }
        }
    </script>
</asp:Content>
