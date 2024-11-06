using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Enums;
using MidMarket.Entities.Factory;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class PermisoService : IPermisoService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IPermisoDAO _permisoDataAccess;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IBitacoraService _bitacoraService;

        public PermisoService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _permisoDataAccess = DependencyResolver.Resolve<IPermisoDAO>();
            _digitoVerificadorService = DependencyResolver.Resolve<IDigitoVerificadorService>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
        }

        public void GuardarFamiliaCreada(Familia familia)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _permisoDataAccess.GuardarFamiliaCreada(familia);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) guardó la familia {familia.Id}", Criticidad.Alta, clienteLogueado);

                _digitoVerificadorService.RecalcularDigitosPermisoDTO(this);

                scope.Complete();
            }
        }

        public int GuardarPatenteFamilia(Componente componente, bool familia)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = _permisoDataAccess.GuardarPatenteFamilia(componente, familia);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) guardó la patente/familia {id}", Criticidad.Alta, clienteLogueado);

                _digitoVerificadorService.RecalcularDigitosPermisoDTO(this);

                scope.Complete();

                return id;
            }
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
                        Componente permiso = item.Id > 0 ? item : PermisoFactory.CrearPermiso(item.Permiso, item.Id);

                        UsuarioPermisoDTO usuarioPermisosDVH = new UsuarioPermisoDTO
                        {
                            UsuarioId = cliente.Id,
                            PermisoId = permiso.Id
                        };

                        string DVH = DigitoVerificador.GenerarDVH(usuarioPermisosDVH);
                        _permisoDataAccess.GuardarPermiso(cliente, permiso, DVH);
                    }
                }

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) cambió los permisos de: {cliente.RazonSocial} ({cliente.Id})",
                    Criticidad.Alta,
                    clienteLogueado
                );

                _digitoVerificadorService.ActualizarDVV("UsuarioPermiso");

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

        public List<UsuarioPermisoDTO> GetUsuariosPermisos()
        {
            List<UsuarioPermisoDTO> usuariosPermisos = _permisoDataAccess.GetUsuariosPermisos();
            return usuariosPermisos;
        }

        public List<PermisoDTO> GetPermisoDTO()
        {
            List<PermisoDTO> permisos = _permisoDataAccess.GetPermisosDTO();
            return permisos;
        }
    }
}
