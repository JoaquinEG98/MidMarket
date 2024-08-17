using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class PermisoService : IPermisoService
    {
        private readonly IPermisoDAO _permisoDataAccess;

        public PermisoService()
        {
            _permisoDataAccess = DependencyResolver.Resolve<IPermisoDAO>();
        }

        public void GuardarFamiliaCreada(Familia familia)
        {
            _permisoDataAccess.GuardarFamiliaCreada(familia);
        }

        public int GuardarPatenteFamilia(Componente componente, bool familia)
        {
            int id = _permisoDataAccess.GuardarPatenteFamilia(componente, familia);

            return id;
        }

        public void GuardarPermiso(Cliente cliente)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _permisoDataAccess.BorrarPermisoUsuario(cliente);

                if (cliente.Permisos.Count > 0)
                {
                    foreach (Componente item in cliente.Permisos)
                    {
                        UsuarioPermisoDTO usuarioPermisosDVH = new UsuarioPermisoDTO();
                        usuarioPermisosDVH.UsuarioId = cliente.Id;
                        usuarioPermisosDVH.PermisoId = item.Id;
                        string DVH = DigitoVerificador.GenerarDVH(usuarioPermisosDVH);

                        _permisoDataAccess.GuardarPermiso(cliente, item, DVH);
                    }
                }

                //_digitoVerificador.ActualizarDVV("UsuarioPermiso");

                scope.Complete();
            }
        }

        public IList<Componente> TraerFamiliaPatentes(int familiaId)
        {
            IList<Componente> componentes = _permisoDataAccess.TraerFamiliaPatentes(familiaId);
            return componentes;
        }

        public Componente GetFamiliaArbol(int familiaId, Componente componenteOriginal, Componente componenteAgregar)
        {
            Componente comp = _permisoDataAccess.GetFamiliaArbol(familiaId, componenteOriginal, componenteAgregar);
            return comp;
        }

        public Componente GetUsuarioArbol(int usuarioId, Componente componenteOriginal, Componente componenteAgregar)
        {
            Componente comp = _permisoDataAccess.GetUsuarioArbol(usuarioId, componenteOriginal, componenteAgregar);
            return comp;
        }

        public IList<Familia> GetFamilias()
        {
            IList<Familia> familias = _permisoDataAccess.GetFamilias();
            return familias;
        }

        public IList<Patente> GetPatentes()
        {
            IList<Patente> patentes = _permisoDataAccess.GetPatentes();
            return patentes;
        }

        public Array TraerPermisos()
        {
            return Enum.GetValues(typeof(Permiso));
        }

        public IList<Familia> GetFamiliasValidacion(int familiaId)
        {
            IList<Familia> familias = _permisoDataAccess.GetFamiliasValidacion(familiaId);
            return familias;
        }

        public bool ExisteComponente(Componente componente, int Id)
        {
            bool existeComponente = false;

            if (componente.Id.Equals(Id))
                existeComponente = true;

            else
            {
                foreach (var item in componente.Hijos)
                {
                    existeComponente = ExisteComponente(item, Id);
                    if (existeComponente) return true;
                }
            }

            return existeComponente;
        }

        public void GetComponenteUsuario(Cliente cliente)
        {
            _permisoDataAccess.GetComponenteUsuario(cliente);
        }

        public void GetComponenteFamilia(Familia familia)
        {
            familia.VaciarHijos();
            foreach (Componente item in TraerFamiliaPatentes(familia.Id))
            {
                familia.AgregarHijo(item);
            }
        }
    }
}
