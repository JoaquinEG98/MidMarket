namespace MidMarket.Business.Interfaces
{
    public interface IDigitoVerificadorService
    {
        void ActualizarDVV(string tabla);
        bool VerificarInconsistenciaTablas();
        void RecalcularTodosDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService, ICompraService compraService, IVentaService ventaService, ICarritoService carritoService);
        void RecalcularDigitosUsuario(IUsuarioService usuarioService, IPermisoService permisoService);
        void RecalcularDigitosClienteActivo(ICompraService compraService);
        void RecalcularDigitosPermisoDTO(IPermisoService permisoService);
        void RecalcularDigitosFamiliaPatente(IPermisoService permisoService);
        void RecalcularDigitosCuenta(IUsuarioService usuarioService);
        void RecalcularDigitosCarrito(ICarritoService carritoService);
    }
}
