using MidMarket.Entities.Composite;
using MidMarket.Entities.Enums;

namespace MidMarket.Entities.Factory
{
    public static class PermisoFactory
    {
        public static Componente CrearPermiso(Permiso tipoPermiso, int? id = null)
        {
            if (tipoPermiso == Permiso.EsFamilia)
            {
                return new Familia { Nombre = tipoPermiso.ToString(), Id = id ?? 0 };
            }
            else
            {
                return new Patente { Nombre = tipoPermiso.ToString(), Permiso = tipoPermiso, Id = id ?? 0 };
            }
        }
    }
}
