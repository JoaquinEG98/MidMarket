<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Gestión de Patentes</title>
    <link rel="stylesheet" href="Styles/gestion_familias.css">
</head>
<body>
    <div class="container">
        <form class="registro-form">
            <h2>Alta de Familia</h2>
            
            <div class="form-group">
                <h3>Nombre de Familia</h3>
                <input type="text" id="nombreFamilia" name="nombreFamilia" required>
            </div>

            <h3>Patentes Existentes</h3>
            <table id="tablaExistentes">
                <thead>
                    <tr>
                        <th>Seleccionar</th>
                        <th>Nombre de Patente</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><input type="checkbox" class="select-patente"></td>
                        <td>Patente 1</td>
                        <td>Descripción de la patente 1</td>
                    </tr>
                    <tr>
                        <td><input type="checkbox" class="select-patente"></td>
                        <td>Patente 2</td>
                        <td>Descripción de la patente 2</td>
                    </tr>
                </tbody>
            </table>

            <button type="button" class="submit-btn" onclick="agregarPatente()">Agregar Patente</button>

            <h3>Patentes Agregadas</h3>
            <table id="tablaAgregadas">
                <thead>
                    <tr>
                        <th>Nombre de Patente</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>

            <button type="button" class="submit-btn" onclick="crear()">Crear</button>
        </form>
    </div>

    <script>
        function agregarPatente() {
            const checkboxes = document.querySelectorAll('.select-patente:checked');
            const tablaAgregadas = document.getElementById('tablaAgregadas').getElementsByTagName('tbody')[0];

            checkboxes.forEach(checkbox => {
                const fila = checkbox.parentElement.parentElement;
                const nuevaFila = fila.cloneNode(true);

                nuevaFila.deleteCell(0);

                tablaAgregadas.appendChild(nuevaFila);

                checkbox.checked = false;
                checkbox.disabled = true;
            });
        }

        function crear() {
            const nombreFamilia = document.getElementById('nombreFamilia').value;
            const tablaAgregadas = document.getElementById('tablaAgregadas').getElementsByTagName('tbody')[0];

            if (nombreFamilia.trim() === "") {
                alert("Por favor, ingresa el nombre de la familia.");
                return;
            }

            if (tablaAgregadas.rows.length > 0) {
                alert(`¡Las patentes de la familia "${nombreFamilia}" han sido creadas con éxito!`);
            } else {
                alert("No hay patentes agregadas.");
            }
        }
    </script>
</body>
</html>
