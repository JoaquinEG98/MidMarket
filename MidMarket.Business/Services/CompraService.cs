using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Enums;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class CompraService : ICompraService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IBitacoraService _bitacoraService;
        private readonly ICompraDAO _compraDataAccess;
        private readonly IUsuarioService _usuarioService;
        private readonly ITraduccionService _traduccionService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;

        public CompraService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
            _compraDataAccess = DependencyResolver.Resolve<ICompraDAO>();
            _usuarioService = DependencyResolver.Resolve<IUsuarioService>();
            _traduccionService = DependencyResolver.Resolve<ITraduccionService>();
            _digitoVerificadorService = DependencyResolver.Resolve<IDigitoVerificadorService>();
        }

        public int RealizarCompra(List<Carrito> carrito)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                var saldoActual = _usuarioService.GetCliente(clienteLogueado.Id).Cuenta.Saldo;
                var total = carrito.Sum(x => x.Total);

                ValidarSaldo(saldoActual, total);

                TransaccionCompra compra = new TransaccionCompra()
                {
                    Cuenta = clienteLogueado.Cuenta,
                    Cliente = clienteLogueado,
                    Fecha = ClockWrapper.Now(),
                    Total = total
                };
                compra.DVH = GenerarDVHTransaccionCompra(compra);

                int compraId = _compraDataAccess.InsertarTransaccionCompra(compra);

                foreach (var item in carrito)
                {
                    DetalleCompra detalle = new DetalleCompra()
                    {
                        Activo = item.Activo,
                        Cantidad = item.Cantidad,
                    };

                    if (item.Activo is Accion accion)
                    {
                        detalle.Precio = accion.Precio;
                    }
                    else if (item.Activo is Bono bono)
                    {
                        detalle.Precio = bono.ValorNominal;
                    }
                    detalle.DVH = GenerarDVHDetalleCompra(detalle, compraId);

                    _compraDataAccess.InsertarDetalleCompra(detalle, compraId);

                    ClienteActivoDTO clienteActivo = new ClienteActivoDTO()
                    {
                        Id_Cliente = clienteLogueado.Id,
                        Id_Activo = item.Activo.Id,
                        Cantidad = item.Cantidad
                    };

                    _compraDataAccess.InsertarActivoCliente(clienteActivo);
                }

                _usuarioService.ActualizarSaldo(total, false);

                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) realizó la Compra con Id: ({compraId}) por un total de (${total})", Criticidad.Media, clienteLogueado);

                _digitoVerificadorService.ActualizarDVV("DetalleCompra");
                //_digitoVerificadorService.ActualizarDVV("TransaccionCompra");
                _digitoVerificadorService.RecalcularDigitosTransaccionCompra(this);
                _digitoVerificadorService.RecalcularDigitosClienteActivo(this);

                scope.Complete();

                return compraId;
            }
        }

        private string GenerarDVHTransaccionCompra(TransaccionCompra compra)
        {
            TransaccionCompraDTO compraDTO = new TransaccionCompraDTO()
            {
                Id_Cliente = compra.Cliente.Id,
                Id_Cuenta = compra.Cuenta.Id,
                Total = compra.Total,
                Fecha = compra.Fecha,
            };
            compraDTO.DVH = DigitoVerificador.GenerarDVH(compraDTO);

            return compraDTO.DVH;
        }

        private string GenerarDVHDetalleCompra(DetalleCompra detalle, int compraId)
        {
            var detalleDTO = new DetalleCompraDTO()
            {
                Id_Activo = detalle.Activo.Id,
                Id_Compra = compraId,
                Cantidad = detalle.Cantidad,
                Precio = detalle.Precio,
            };
            detalleDTO.DVH = DigitoVerificador.GenerarDVH(detalleDTO);

            return detalleDTO.DVH;
        }

        public void ValidarSaldo(decimal saldo, decimal total)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (total > saldo)
                throw new System.Exception($"{_traduccionService.ObtenerMensaje(idioma, "ERR_18")}");
        }

        public List<TransaccionCompra> GetCompras(bool historico)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            List<TransaccionCompra> compras = _compraDataAccess.GetCompras(clienteLogueado, historico);

            return compras;
        }

        public List<TransaccionCompraDTO> GetAllCompras()
        {
            List<TransaccionCompraDTO> compras = _compraDataAccess.GetAllCompras();

            return compras;
        }

        public List<DetalleCompraDTO> GetAllComprasDetalle()
        {
            List<DetalleCompraDTO> detalle = _compraDataAccess.GetAllComprasDetalle();

            return detalle;
        }

        public List<ClienteActivoDTO> GetAllClienteActivo()
        {
            List<ClienteActivoDTO> clienteActivo = _compraDataAccess.GetAllClienteActivo();

            return clienteActivo;
        }
    }
}
