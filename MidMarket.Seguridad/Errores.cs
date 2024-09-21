using System.Collections.Generic;

namespace MidMarket.Seguridad
{
    public static class Errores
    {
        private static readonly Dictionary<int, string> errores = new Dictionary<int, string>
        {
            { 1, "[ERR-001]: Inconsistencia en los datos ingresados en los campos" },
            { 2, "[ERR-002]: Campos obligatorios incompletos" },
            { 3, "[ERR-003]: No se puede establecer conexión a la base de datos" },
            { 4, "[ERR-004]: El item seleccionado ya se encuentra deshabilitado" },
            { 5, "[ERR-005]: El usuario y/o contraseña no contienen datos" },
            { 6, "[ERR-006]: La contraseña no posee el formato correcto" },
            { 7, "[ERR-007]: Contraseña incorrecta" },
            { 8, "[ERR-008]: El usuario se encuentra bloqueado. Contáctese con el administrador" },
            { 9, "[ERR-009]: El usuario no existe en la base de datos" },
            { 10, "[ERR-010]: Problema al enviar el mail" },
            { 11, "[ERR-011]: El usuario ya está registrado" },
            { 12, "[ERR-012]: El usuario se encuentra bloqueado" },
            { 13, "[ERR-013]: Nombre de familia ya existente" },
            { 14, "[ERR-014]: Error al generar copia de seguridad" },
            { 15, "[ERR-015]: Error al querer restaurar una copia de seguridad" },
            { 16, "[ERR-016]: Error de integridad en la base de datos" },
            { 17, "[ERR-017]: Error en la compra de Activo" },
            { 18, "[ERR-018]: Saldo insuficiente para la Compra" },
            { 19, "[ERR-019]: Error al canjear Recompensa" },
            { 20, "[ERR-020]: La fecha desde no puede ser mayor que la fecha hasta" }
        };

        public static string ObtenerError(int codigo)
        {
            if (errores.ContainsKey(codigo))
            {
                return errores[codigo];
            }
            else
            {
                return "[ERR-000]: Código de error desconocido";
            }
        }
    }
}
