using MidMarket.Entities.Composite;
using MidMarket.Entities;
using System;
using System.Collections.Generic;
using MidMarket.Entities.DTOs;

namespace MidMarket.Business.Interfaces
{
    public interface IPermisoService
    {
        void GuardarFamiliaCreada(Familia familia);
        int GuardarPatenteFamilia(Componente componente, bool familia);
        void GuardarPermiso(Cliente cliente);
        IList<Componente> TraerFamiliaPatentes(int familiaId);
        Componente GetFamiliaArbol(int familiaId, Componente componenteOriginal, Componente componenteAgregar);
        Componente GetUsuarioArbol(int usuarioId, Componente componenteOriginal, Componente componenteAgregar);
        IList<Familia> GetFamilias();
        IList<Patente> GetPatentes();
        Array TraerPermisos();
        IList<Familia> GetFamiliasValidacion(int familiaId);
        bool ExisteComponente(Componente componente, int Id);
        void GetComponenteUsuario(Cliente cliente);
        void GetComponenteFamilia(Familia familia);
        List<UsuarioPermisoDTO> GetUsuariosPermisos();
    }
}
