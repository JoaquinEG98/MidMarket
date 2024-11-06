using MidMarket.Entities.Enums;

namespace MidMarket.Entities.DTOs
{
    public class PermisoDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Permiso Permiso { get; set; }
    }
}