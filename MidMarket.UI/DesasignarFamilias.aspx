<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesasignarFamilias.aspx.cs" Inherits="MidMarket.UI.DesasignarFamilias" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="desasignar-familias-container" class="container">
        <h2>Eliminar Familia a Usuario</h2>

        <div class="form-group">
            <label for="filtroUsuario">Filtrar Usuario:</label>
            <input type="text" id="filtroUsuario" oninput="filtrarUsuarios()" placeholder="Escribe para buscar...">
        </div>

        <div class="form-group">
            <label for="selectUsuario">Seleccionar Usuario:</label>
            <select id="selectUsuario" onchange="cargarFamiliasAsignadas()">
                <option value="">Selecciona un usuario</option>
                <option value="1">Usuario 1</option>
                <option value="2">Usuario 2</option>
                <option value="3">Usuario 3</option>
            </select>
        </div>

        <h3>Familias Asignadas</h3>
        <table id="tablaFamiliasAsignadas">
            <thead>
                <tr>
                    <th>Seleccionar</th>
                    <th>Nombre de Familia</th>
                    <th>Descripción</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

        <button type="button" class="submit-btn" onclick="eliminarFamilias()">Eliminar Familias</button>
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

        function cargarFamiliasAsignadas() {
            const usuarioSeleccionado = document.getElementById('selectUsuario').value;
            const tablaAsignadas = document.getElementById('tablaFamiliasAsignadas').getElementsByTagName('tbody')[0];
            tablaAsignadas.innerHTML = '';

            if (usuarioSeleccionado) {
                const familias = {
                    1: [
                        { nombre: 'Familia A', descripcion: 'Descripción de la familia A' },
                        { nombre: 'Familia B', descripcion: 'Descripción de la familia B' }
                    ],
                    2: [
                        { nombre: 'Familia C', descripcion: 'Descripción de la familia C' }
                    ],
                    3: [
                        { nombre: 'Familia D', descripcion: 'Descripción de la familia D' }
                    ]
                };

                const familiasAsignadas = familias[usuarioSeleccionado] || [];
                familiasAsignadas.forEach(familia => {
                    const fila = document.createElement('tr');
                    fila.innerHTML = `
                        <td><input type="checkbox" class="select-familia"></td>
                        <td>${familia.nombre}</td>
                        <td>${familia.descripcion}</td>`;
                    tablaAsignadas.appendChild(fila);
                });
            }
        }

        function eliminarFamilias() {
            const checkboxes = document.querySelectorAll('.select-familia:checked');
            if (checkboxes.length === 0) {
                alert("Por favor, selecciona al menos una familia para eliminar.");
                return;
            }

            checkboxes.forEach(checkbox => {
                const fila = checkbox.parentElement.parentElement;
                fila.remove();
            });

            alert("Familias eliminadas con éxito.");
        }
    </script>
</asp:Content>
