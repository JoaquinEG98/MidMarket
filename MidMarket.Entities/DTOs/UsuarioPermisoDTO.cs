namespace MidMarket.Entities.DTOs
{
    public class UsuarioPermisoDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PermisoId { get; set; }
    }
}
