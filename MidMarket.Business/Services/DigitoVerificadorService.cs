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

        public bool VerificarInconsistenciaTablas(out List<string> tablas)
        {
            tablas = new List<string>();
            var nombresTablas = new[]
            {
                "Cliente", "UsuarioPermiso", "TransaccionCompra", "DetalleCompra", "ClienteActivo",
                "TransaccionVenta", "DetalleVenta", "Permiso", "FamiliaPatente", "Cuenta",
                "Carrito", "Bitacora", "Activo", "Accion", "Bono"
            };

            foreach (var nombreTabla in nombresTablas)
            {
                if (!ValidarDigitosVerificadores(nombreTabla))
                {
                    tablas.Add(nombreTabla);
                }
            }

            return tablas.Count == 0;
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

        private void ActualizarTablaDVH(List<TransaccionCompraDTO> compras)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (TransaccionCompraDTO compra in compras)
                {
                    var compraDTO = new TransaccionCompraDTO()
                    {
                        Id = compra.Id,
                        Id_Cuenta = compra.Id_Cuenta,
                        Id_Cliente = compra.Id_Cliente,
                        Fecha = compra.Fecha,
                        Total = compra.Total,
                    };
                    compraDTO.DVH = DigitoVerificador.GenerarDVH(compraDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("TransaccionCompra", compraDTO.DVH, compraDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<DetalleCompraDTO> detalle)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var item in detalle)
                {
                    var detalleDTO = new DetalleCompraDTO()
                    {
                        Id = item.Id,
                        Id_Activo = item.Id_Activo,
                        Id_Compra = item.Id_Compra,
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

        private void ActualizarTablaDVH(List<TransaccionVentaDTO> ventas)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var venta in ventas)
                {
                    var ventaDTO = new TransaccionVentaDTO()
                    {
                        Id = venta.Id,
                        Id_Cliente = venta.Id_Cliente,
                        Id_Cuenta = venta.Id_Cuenta,
                        Fecha = venta.Fecha,
                        Total = venta.Total,
                    };
                    ventaDTO.DVH = DigitoVerificador.GenerarDVH(ventaDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("TransaccionVenta", ventaDTO.DVH, ventaDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<DetalleVentaDTO> detalle)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var item in detalle)
                {
                    var detalleDTO = new DetalleVentaDTO()
                    {
                        Id = item.Id,
                        Id_Activo = item.Id_Activo,
                        Id_Venta = item.Id_Venta,
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

        private void ActualizarTablaDVH(List<CuentaDTO> cuentas)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var cuenta in cuentas)
                {
                    var cuentaDTO = new CuentaDTO()
                    {
                        Id = cuenta.Id,
                        Id_Cliente = cuenta.Id_Cliente,
                        NumeroCuenta = cuenta.NumeroCuenta,
                        Saldo = cuenta.Saldo,
                        DVH = cuenta.DVH,
                    };
                    cuentaDTO.DVH = DigitoVerificador.GenerarDVH(cuentaDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("Cuenta", cuentaDTO.DVH, cuentaDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<CarritoDTO> carrito)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var item in carrito)
                {
                    var carritoDTO = new CarritoDTO()
                    {
                        Id = item.Id,
                        Id_Activo = item.Id_Activo,
                        Id_Cliente = item.Id_Cliente,
                        Cantidad = item.Cantidad,
                        DVH = item.DVH,
                    };
                    carritoDTO.DVH = DigitoVerificador.GenerarDVH(carritoDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("Carrito", carritoDTO.DVH, carritoDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<BitacoraDTO> bitacora)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var item in bitacora)
                {
                    var bitacoraDTO = new BitacoraDTO()
                    {
                        Id = item.Id,
                        Id_Cliente = item.Id_Cliente,
                        Descripcion = item.Descripcion,
                        Criticidad = item.Criticidad,
                        Fecha = item.Fecha,
                        DVH = item.DVH,
                    };
                    bitacoraDTO.DVH = DigitoVerificador.GenerarDVH(bitacoraDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("Bitacora", bitacoraDTO.DVH, bitacoraDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<ActivoDTO> activos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var activo in activos)
                {
                    var activoDTO = new ActivoDTO()
                    {
                        Id = activo.Id,
                        Nombre = activo.Nombre,
                        DVH = activo.DVH,
                    };
                    activoDTO.DVH = DigitoVerificador.GenerarDVH(activoDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("Activo", activoDTO.DVH, activoDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<AccionDTO> acciones)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var accion in acciones)
                {
                    var accionDTO = new AccionDTO()
                    {
                        Id = accion.Id,
                        Simbolo = accion.Simbolo,
                        Precio = accion.Precio,
                        Id_Activo = accion.Id_Activo,
                        DVH = accion.DVH,
                    };
                    accionDTO.DVH = DigitoVerificador.GenerarDVH(accionDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("Accion", accionDTO.DVH, accionDTO.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<BonoDTO> bonos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var bono in bonos)
                {
                    var bonoDTO = new BonoDTO()
                    {
                        Id = bono.Id,
                        ValorNominal = bono.ValorNominal,
                        TasaInteres = bono.TasaInteres,
                        Id_Activo = bono.Id_Activo,
                        DVH = bono.DVH,
                    };
                    bonoDTO.DVH = DigitoVerificador.GenerarDVH(bonoDTO);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("Bono", bonoDTO.DVH, bonoDTO.Id);
                }
                scope.Complete();
            }
        }

        public void RecalcularTodosDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService, ICompraService compraService, IVentaService ventaService, ICarritoService carritoService, IBitacoraService bitacoraService, IActivoService activoService)
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

            var cuentas = usuarioService.GetCuentas();
            ActualizarTablaDVH(cuentas);
            ActualizarDVV("Cuenta");

            var carrito = carritoService.GetCarritoDTO();
            ActualizarTablaDVH(carrito);
            ActualizarDVV("Carrito");

            var bitacora = bitacoraService.GetAllBitacora();
            ActualizarTablaDVH(bitacora);
            ActualizarDVV("Bitacora");

            var activos = activoService.GetActivoDTO();
            ActualizarTablaDVH(activos);
            ActualizarDVV("Activo");

            var acciones = activoService.GetAccionDTO();
            ActualizarTablaDVH(acciones);
            ActualizarDVV("Accion");

            var bonos = activoService.GetBonoDTO();
            ActualizarTablaDVH(bonos);
            ActualizarDVV("Bono");
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

        public void RecalcularDigitosCuenta(IUsuarioService usuarioService)
        {
            var cuentas = usuarioService.GetCuentas();
            ActualizarTablaDVH(cuentas);
            ActualizarDVV("Cuenta");
        }

        public void RecalcularDigitosCarrito(ICarritoService carritoService)
        {
            var carrito = carritoService.GetCarritoDTO();
            ActualizarTablaDVH(carrito);
            ActualizarDVV("Carrito");
        }

        public void RecalcularDigitosBitacora(IBitacoraService bitacoraService)
        {
            var bitacora = bitacoraService.GetAllBitacora();
            ActualizarTablaDVH(bitacora);
            ActualizarDVV("Bitacora");
        }

        public void RecalcularDigitosActivo(IActivoService activoService)
        {
            var activos = activoService.GetActivoDTO();
            ActualizarTablaDVH(activos);
            ActualizarDVV("Activo");
        }

        public void RecalcularDigitosAcciones(IActivoService activoService)
        {
            var acciones = activoService.GetAccionDTO();
            ActualizarTablaDVH(acciones);
            ActualizarDVV("Accion");
        }

        public void RecalcularDigitosBono(IActivoService activoService)
        {
            var bonos = activoService.GetBonoDTO();
            ActualizarTablaDVH(bonos);
            ActualizarDVV("Bono");
        }

        public void RecalcularDigitosTransaccionCompra(ICompraService compraService)
        {
            var compras = compraService.GetAllCompras();
            ActualizarTablaDVH(compras);
            ActualizarDVV("TransaccionCompra");
        }
    }
}
