using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IDigitoVerificadorService
    {
        void ActualizarDVV(string tabla);
        bool VerificarInconsistenciaTablas(out List<string> tablas, IBitacoraService bitacoraService);
        void RecalcularTodosDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService, ICompraService compraService, IVentaService ventaService, ICarritoService carritoService, IBitacoraService bitacoraService, IActivoService activoService);
        void RecalcularDigitosUsuario(IUsuarioService usuarioService, IPermisoService permisoService);
        void RecalcularDigitosClienteActivo(ICompraService compraService);
        void RecalcularDigitosPermisoDTO(IPermisoService permisoService);
        void RecalcularDigitosFamiliaPatente(IPermisoService permisoService);
        void RecalcularDigitosCuenta(IUsuarioService usuarioService);
        void RecalcularDigitosCarrito(ICarritoService carritoService);
        void RecalcularDigitosBitacora(IBitacoraService bitacoraService);
        void RecalcularDigitosActivo(IActivoService activoService);
        void RecalcularDigitosAcciones(IActivoService activoService);
        void RecalcularDigitosBono(IActivoService activoService);
        void RecalcularDigitosTransaccionCompra(ICompraService compraService);
        bool ValidarDigitosVerificadores(string tabla);
    }
}
