using MidMarket.Entities.Enums;
using System.Collections.Generic;

namespace MidMarket.Entities.Composite
{
    public abstract class Componente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public abstract IList<Componente> Hijos { get; }
        public abstract void AgregarHijo(Componente componente);
        public abstract void VaciarHijos();
        public abstract void BorrarHijo(Componente componente);
        public Permiso Permiso { get; set; }
    }
}
