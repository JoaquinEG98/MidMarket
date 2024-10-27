using MidMarket.Entities.Composite;
using MidMarket.Entities.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MidMarket.Seguridad
{
    public static class PermisoCheck
    {
        public static bool VerificarPermiso(List<Componente> permisosCliente, Permiso permisoRequerido)
        {
            foreach (var permiso in permisosCliente)
            {
                if (permiso.Permiso == Permiso.EsFamilia)
                {
                    if (permiso.Hijos.Any(hijo => hijo.Permiso == permisoRequerido))
                    {
                        return true;
                    }
                }
                else if (permiso.Permiso == permisoRequerido)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
