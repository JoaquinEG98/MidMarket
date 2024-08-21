using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IPermisoDAO
    {
        int GuardarPatenteFamilia(Componente componente, bool familia);
        void GuardarFamiliaCreada(Familia familia);
        IList<Familia> GetFamilias();
        IList<Patente> GetPatentes();
        IList<Componente> TraerFamiliaPatentes(int familiaId);
        void GetComponenteUsuario(Cliente cliente);
        void BorrarPermisoUsuario(Cliente cliente);
        void GuardarPermiso(Cliente cliente, Componente permiso, string DVH);
        IList<Familia> GetFamiliasValidacion(int familiaId);
        Componente GetFamiliaArbol(int familiaId, Componente componenteOriginal, Componente componenteAgregar);
        Componente GetUsuarioArbol(int usuarioId, Componente componenteOriginal, Componente componenteAgregar);
        List<UsuarioPermisoDTO> GetUsuariosPermisos();
    }
}
