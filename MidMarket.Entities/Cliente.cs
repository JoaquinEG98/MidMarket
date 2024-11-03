using MidMarket.Entities.Composite;
using MidMarket.Entities.Enums;
using MidMarket.Entities.Factory;
using MidMarket.Entities.Observer;
using System.Collections.Generic;

namespace MidMarket.Entities
{
    public class Cliente : DigitoVerificadorHorizontal
    {
        List<Componente> _permisos = new List<Componente>();
        static List<IObserver> _observers = new List<IObserver>();

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

        public void AsignarPermiso(Permiso tipoPermiso)
        {
            Componente permiso = PermisoFactory.CrearPermiso(tipoPermiso);
            _permisos.Add(permiso);
        }

        public void EliminarPermiso(Componente permiso)
        {
            _permisos.Remove(permiso);
        }

        public void SuscribirObservador(IObserver o)
        {
            _observers.Add(o);
        }

        public void DesuscribirObservador(IObserver o)
        {
            _observers.Remove(o);
        }

        private static void Notificar(IIdioma idioma)
        {
            foreach (var o in _observers)
            {
                o.UpdateLanguage(idioma);
            }
        }
        public void CambiarIdioma(IIdioma idioma)
        {
            Notificar(idioma);
        }
    }
}
