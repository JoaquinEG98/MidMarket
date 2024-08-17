using System.Collections.Generic;
using System.Linq;

namespace MidMarket.Entities.Composite
{
    public class Patente : Componente
    {
        private IList<Componente> _hijos;

        public Patente()
        {
            _hijos = new List<Componente>();
        }

        public override IList<Componente> Hijos
        {
            get
            {
                return _hijos.ToArray();
            }
        }
        public override void AgregarHijo(Componente componente)
        {
            _hijos.Add(componente);
        }
        public override void VaciarHijos()
        {
        }
        public override void BorrarHijo(Componente componente)
        {
        }
    }
}
