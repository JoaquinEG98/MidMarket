<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesasignarPatentes.aspx.cs" Inherits="MidMarket.UI.DesasignarPatentes" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="desasignar-patentes-container" class="container">
        <h2>Eliminar Patente a Usuario</h2>

        <div class="form-group">
            <label for="filtroUsuario">Filtrar Usuario:</label>
            <input type="text" id="filtroUsuario" oninput="filtrarUsuarios()" placeholder="Escribe para buscar...">
        </div>

        <div class="form-group">
            <label for="selectUsuario">Seleccionar Usuario:</label>
            <select id="selectUsuario" onchange="cargarPatentesAsignadas()">
                <option value="">Selecciona un usuario</option>
                <option value="1">Usuario 1</option>
                <option value="2">Usuario 2</option>
                <option value="3">Usuario 3</option>
            </select>
        </div>

        <h3>Patentes Asignadas</h3>
        <table id="tablaPatentesAsignadas">
            <thead>
                <tr>
                    <th>Seleccionar</th>
                    <th>Nombre de Patente</th>
                    <th>Descripción</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>

        <button type="button" class="submit-btn" onclick="eliminarPatentes()">Eliminar Patentes</button>
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
                    fila.innerHTML = `
                        <td><input type="checkbox" class="select-patente"></td>
                        <td>${patente.nombre}</td>
                        <td>${patente.descripcion}</td>`;
                    tablaAsignadas.appendChild(fila);
                });
            }
        }

        function eliminarPatentes() {
            const checkboxes = document.querySelectorAll('.select-patente:checked');
            if (checkboxes.length === 0) {
                alert("Por favor, selecciona al menos una patente para eliminar.");
                return;
            }

            checkboxes.forEach(checkbox => {
                const fila = checkbox.parentElement.parentElement;
                fila.remove();
            });

            alert("Patentes eliminadas con éxito.");
        }
    </script>
</asp:Content>
