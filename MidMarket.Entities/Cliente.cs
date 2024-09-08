using MidMarket.Entities.Composite;
using System.Collections.Generic;

namespace MidMarket.Entities
{
    public class Cliente : DigitoVerificadorHorizontal
    {
        List<Componente> _permisos = new List<Componente>();

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public double Puntaje { get; set; }
        public int Bloqueo { get; set; }
        public Cuenta Cuenta { get; set; }
        public List<Componente> Permisos
        {
            get
            {
                return _permisos;
            }
        }
        public void EliminarPermisosPorId(List<int> ids)
        {
            _permisos.RemoveAll(p => ids.Contains(p.Id));
        }
    }
}
