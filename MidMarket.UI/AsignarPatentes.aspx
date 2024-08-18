<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsignarPatentes.aspx.cs" Inherits="MidMarket.UI.AsignarPatentes" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="asignar-patentes-container" class="container">
        <h2>Asignación de Patente a Usuario</h2>

        <div class="form-group">
            <label for="filtroUsuario">Filtrar Usuario:</label>
            <input type="text" id="filtroUsuario" oninput="filtrarUsuarios()" placeholder="Escribe para buscar...">
        </div>

        <div class="form-group">
            <label for="selectUsuario">Seleccionar Usuario:</label>
            <select id="selectUsuario" onchange="cargarPatentesAsignadas()">
                <option value="">Selecciona un usuario</option>
                <% foreach (var cliente in Clientes)
                    { %>
                <option value="<%= cliente.Id %>"><%= cliente.RazonSocial %></option>
                <% } %>
            </select>
        </div>

        <h3>Patentes Asignadas</h3>
        <table id="tablaPatentesAsignadas">
            <thead>
                <tr>
                    <th>Nombre de Patente</th>
                    <th>Descripción</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

        <h3>Patentes Disponibles</h3>
        <table id="tablaPatentesDisponibles">
            <thead>
                <tr>
                    <th>Seleccionar</th>
                    <th>Nombre de Patente</th>
                    <th>Descripción</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <input type="checkbox" class="select-patente"></td>
                    <td>Patente 1</td>
                    <td>Descripción de la patente 1</td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" class="select-patente"></td>
                    <td>Patente 2</td>
                    <td>Descripción de la patente 2</td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" class="select-patente"></td>
                    <td>Patente 3</td>
                    <td>Descripción de la patente 3</td>
                </tr>
            </tbody>
        </table>

        <button type="button" class="submit-btn" onclick="asignarPatentes()">Asignar Patentes</button>
    </div>

    <script>
        function filtrarUsuarios() {
            const filtro = document.getElementById('filtroUsuario').value.toLowerCase();
            const selectUsuario = document.getElementById('selectUsuario');
            const opciones = selectUsuario.getElementsByTagName('option');

            for (let i = 1; i < opciones.length; i++) { 
                const texto = opciones[i].textContent.toLowerCase();
                opciones[i].style.display = texto.includes(filtro) ? '' : 'none';
            }
        }

        function cargarPatentesAsignadas() {
            const usuarioSeleccionado = document.getElementById('selectUsuario').value;
            const tablaAsignadas = document.getElementById('tablaPatentesAsignadas').getElementsByTagName('tbody')[0];
            tablaAsignadas.innerHTML = '';

            if (usuarioSeleccionado) {
                const patentes = {
                    1: [
                        { nombre: 'Patente A', descripcion: 'Descripción de la patente A' },
                        { nombre: 'Patente B', descripcion: 'Descripción de la patente B' }
                    ],
                    2: [
                        { nombre: 'Patente C', descripcion: 'Descripción de la patente C' }
                    ],
                    3: [
                        { nombre: 'Patente D', descripcion: 'Descripción de la patente D' }
                    ]
                };

                const patentesAsignadas = patentes[usuarioSeleccionado] || [];
                patentesAsignadas.forEach(patente => {
                    const fila = document.createElement('tr');
                    fila.innerHTML = `<td>${patente.nombre}</td><td>${patente.descripcion}</td>`;
                    tablaAsignadas.appendChild(fila);
                });

                actualizarPatentesDisponibles(patentesAsignadas);
            }
        }

        function actualizarPatentesDisponibles(patentesAsignadas) {
            const tablaDisponibles = document.getElementById('tablaPatentesDisponibles').getElementsByTagName('tbody')[0];
            const filas = Array.from(tablaDisponibles.getElementsByTagName('tr'));

            patentesAsignadas.forEach(patenteAsignada => {
                filas.forEach(fila => {
                    const nombrePatente = fila.cells[1].textContent;
                    if (nombrePatente === patenteAsignada.nombre) {
                        fila.remove();
                    }
                });
            });
        }

        function asignarPatentes() {
            const checkboxes = document.querySelectorAll('.select-patente:checked');
            const tablaAsignadas = document.getElementById('tablaPatentesAsignadas').getElementsByTagName('tbody')[0];

            if (checkboxes.length === 0) {
                alert("Por favor, selecciona al menos una patente para asignar.");
                return;
            }

            checkboxes.forEach(checkbox => {
                const fila = checkbox.parentElement.parentElement;
                const nuevaFila = fila.cloneNode(true);

                nuevaFila.deleteCell(0);

                tablaAsignadas.appendChild(nuevaFila);

                fila.remove();
            });

            alert("Patentes asignadas con éxito.");
        }
    </script>
</asp:Content>
