using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class DigitoVerificadorService : IDigitoVerificadorService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IDigitoVerificadorDAO _digitoVerificadorDataAccess;
        private readonly IBitacoraService _bitacoraService;

        public DigitoVerificadorService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _digitoVerificadorDataAccess = DependencyResolver.Resolve<IDigitoVerificadorDAO>();
            //_bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
        }

        private string ObtenerDVVActual(string tabla)
        {
            int valor = 0;
            string actualDVV = "";

            List<string> dvhs = _digitoVerificadorDataAccess.ObtenerDVHActuales(tabla);

            if (dvhs.Count > 0)
            {
                foreach (string dvh in dvhs)
                {
                    valor += int.Parse(Encriptacion.DesencriptarAES(dvh));
                }

                actualDVV = Encriptacion.EncriptarAES(valor.ToString());
            }

            return actualDVV;
        }

        private List<string> ObtenerDVH(string tabla)
        {
            List<string> dvhCalculados = _digitoVerificadorDataAccess.ObtenerDVH(tabla);

            return dvhCalculados;
        }

        private string CalcularDVV(string tabla)
        {
            int valor = 0;
            string actualDVV = "";

            List<string> dvhs = ObtenerDVH(tabla);

            if (dvhs.Count > 0)
            {
                foreach (string dvh in dvhs)
                {
                    valor += int.Parse(Encriptacion.DesencriptarAES(dvh));
                }

                actualDVV = Encriptacion.EncriptarAES(valor.ToString());
            }

            return actualDVV;
        }

        public void ActualizarDVV(string tabla)
        {
            string nuevoDVV = CalcularDVV(tabla);

            _digitoVerificadorDataAccess.ActualizarDVV(tabla, nuevoDVV);
        }

        private string ObtenerDVV(string tabla)
        {
            string dvv = _digitoVerificadorDataAccess.ObtenerDVV(tabla);
            return dvv;
        }

        public bool ValidarDigitosVerificadores(string tabla)
        {
            string baseDVV = ObtenerDVVActual(tabla);
            string actualDVV = ObtenerDVV(tabla);
            string compararDVV = CalcularDVV(tabla);

            if (baseDVV == actualDVV && actualDVV == compararDVV)
                return true;

            return false;
        }

        public bool VerificarInconsistenciaTablas()
        {
            bool cliente = ValidarDigitosVerificadores("Cliente");
            bool usuarioPermiso = ValidarDigitosVerificadores("UsuarioPermiso");
            bool transaccionCompra = ValidarDigitosVerificadores("TransaccionCompra");
            bool detalleCompra = ValidarDigitosVerificadores("DetalleCompra");
            bool clienteActivo = ValidarDigitosVerificadores("ClienteActivo");
            bool transaccionVenta = ValidarDigitosVerificadores("TransaccionVenta");
            bool detalleVenta = ValidarDigitosVerificadores("DetalleVenta");
            bool permisos = ValidarDigitosVerificadores("Permiso");
            bool familiaPatente = ValidarDigitosVerificadores("FamiliaPatente");

            if (!cliente || !usuarioPermiso || !transaccionCompra || !detalleCompra || !clienteActivo || !transaccionVenta || !detalleVenta || !permisos || !familiaPatente)
                return false;

            else
                return true;
        }

        private void ActualizarTablaDVH(List<Cliente> clientes)
        {
            foreach (Cliente cliente in clientes)
            {
                Cliente clienteCalculado = new Cliente()
                {
                    Id = cliente.Id,
                    Email = cliente.Email,
                    Password = cliente.Password,
                    RazonSocial = cliente.RazonSocial,
                    CUIT = cliente.CUIT,
                    Puntaje = cliente.Puntaje,
                };
                clienteCalculado.DVH = DigitoVerificador.GenerarDVH(clienteCalculado);

                _digitoVerificadorDataAccess.ActualizarTablaDVH("Cliente", clienteCalculado.DVH, clienteCalculado.Id);
            }
        }

        private void ActualizarTablaDVH(List<UsuarioPermisoDTO> usuariosPermisos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (UsuarioPermisoDTO usuarioPermiso in usuariosPermisos)
                {
                    var userPermiso = new UsuarioPermisoDTO()
                    {
                        Id = usuarioPermiso.Id,
                        UsuarioId = usuarioPermiso.UsuarioId,
                        PermisoId = usuarioPermiso.PermisoId
                    };
                    userPermiso.DVH = DigitoVerificador.GenerarDVH(userPermiso);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("UsuarioPermiso", userPermiso.DVH, userPermiso.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<TransaccionCompra> compras)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (TransaccionCompra compra in compras)
                {
                    var compraDTO = new TransaccionCompra()
                    {
                        Id = compra.Id,
                        Fecha = compra.Fecha,
                        Total = compra.Total,
                    };
                    compraDTO.DVH = DigitoVerificador.GenerarDVH(compraDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("TransaccionCompra", compraDTO.DVH, compraDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<DetalleCompra> detalle)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (DetalleCompra item in detalle)
                {
                    var detalleDTO = new DetalleCompra()
                    {
                        Id = item.Id,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                    };
                    detalleDTO.DVH = DigitoVerificador.GenerarDVH(detalleDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("DetalleCompra", detalleDTO.DVH, detalleDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<ClienteActivoDTO> clienteActivo)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var item in clienteActivo)
                {
                    var clienteActivoDTO = new ClienteActivoDTO()
                    {
                        Id = item.Id,
                        Id_Cliente = item.Id_Cliente,
                        Id_Activo = item.Id_Activo,
                        Cantidad = item.Cantidad
                    };
                    clienteActivoDTO.DVH = DigitoVerificador.GenerarDVH(clienteActivoDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("ClienteActivo", clienteActivoDTO.DVH, clienteActivoDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<TransaccionVenta> ventas)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var venta in ventas)
                {
                    var ventaDTO = new TransaccionVenta()
                    {
                        Id = venta.Id,
                        Fecha = venta.Fecha,
                        Total = venta.Total,
                    };
                    ventaDTO.DVH = DigitoVerificador.GenerarDVH(ventaDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("TransaccionVenta", ventaDTO.DVH, ventaDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<DetalleVenta> detalle)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var item in detalle)
                {
                    var detalleDTO = new DetalleVenta()
                    {
                        Id = item.Id,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                    };
                    detalleDTO.DVH = DigitoVerificador.GenerarDVH(detalleDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("DetalleVenta", detalleDTO.DVH, detalleDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<PermisoDTO> permisos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var permiso in permisos)
                {
                    var permisoDTO = new PermisoDTO()
                    {
                        Id = permiso.Id,
                        Nombre = permiso.Nombre,
                        Permiso = permiso.Permiso,
                        DVH = permiso.DVH,
                    };
                    permisoDTO.DVH = DigitoVerificador.GenerarDVH(permisoDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("Permiso", permisoDTO.DVH, permisoDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<FamiliaPatenteDTO> familiaPatente)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var fp in familiaPatente)
                {
                    var familiaPatenteDTO = new FamiliaPatenteDTO()
                    {
                        Id_Padre = fp.Id_Padre,
                        Id_Hijo = fp.Id_Hijo,
                    };
                    familiaPatenteDTO.DVH = DigitoVerificador.GenerarDVH(familiaPatenteDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVHFamiliaPatente("FamiliaPatente", familiaPatenteDTO.DVH, familiaPatenteDTO.Id_Padre, familiaPatenteDTO.Id_Hijo);
                }
                scope.Complete();
            }
        }

        public void RecalcularTodosDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService, ICompraService compraService, IVentaService ventaService)
        {
            var clientes = usuarioService.GetClientesEncriptados();
            ActualizarTablaDVH(clientes);
            ActualizarDVV("Cliente");

            var usuariosPermisos = permisoService.GetUsuariosPermisos();
            ActualizarTablaDVH(usuariosPermisos);
            ActualizarDVV("UsuarioPermiso");

            var compras = compraService.GetAllCompras();
            ActualizarTablaDVH(compras);
            ActualizarDVV("TransaccionCompra");

            var detalleCompra = compraService.GetAllComprasDetalle();
            ActualizarTablaDVH(detalleCompra);
            ActualizarDVV("DetalleCompra");

            var clienteActivo = compraService.GetAllClienteActivo();
            ActualizarTablaDVH(clienteActivo);
            ActualizarDVV("ClienteActivo");

            var ventas = ventaService.GetAllVentas();
            ActualizarTablaDVH(ventas);
            ActualizarDVV("TransaccionVenta");

            var detalleVenta = ventaService.GetAllVentasDetalle();
            ActualizarTablaDVH(detalleVenta);
            ActualizarDVV("DetalleVenta");

            var permisos = permisoService.GetPermisoDTO();
            ActualizarTablaDVH(permisos);
            ActualizarDVV("Permiso");

            var familiaPatente = permisoService.GetFamiliaPatenteDTO();
            ActualizarTablaDVH(familiaPatente);
            ActualizarDVV("FamiliaPatente");
        }

        public void RecalcularDigitosUsuario(IUsuarioService usuarioService, IPermisoService permisoService)
        {
            var clientes = usuarioService.GetClientesEncriptados();
            ActualizarTablaDVH(clientes);
            ActualizarDVV("Cliente");
        }

        public void RecalcularDigitosClienteActivo(ICompraService compraService)
        {
            var clienteActivo = compraService.GetAllClienteActivo();
            ActualizarTablaDVH(clienteActivo);
            ActualizarDVV("ClienteActivo");
        }

        public void RecalcularDigitosPermisoDTO(IPermisoService permisoService)
        {
            var permisos = permisoService.GetPermisoDTO();
            ActualizarTablaDVH(permisos);
            ActualizarDVV("Permiso");
        }

        public void RecalcularDigitosFamiliaPatente(IPermisoService permisoService)
        {
            var familiaPatente = permisoService.GetFamiliaPatenteDTO();
            ActualizarTablaDVH(familiaPatente);
            ActualizarDVV("FamiliaPatente");
        }
    }
}
