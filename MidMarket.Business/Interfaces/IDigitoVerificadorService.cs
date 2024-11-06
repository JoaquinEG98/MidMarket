namespace MidMarket.Business.Interfaces
{
    public interface IDigitoVerificadorService
    {
        void ActualizarDVV(string tabla);
        bool VerificarInconsistenciaTablas();
        void RecalcularTodosDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService, ICompraService compraService, IVentaService ventaService);
        void RecalcularDigitosUsuario(IUsuarioService usuarioService, IPermisoService permisoService);
        void RecalcularDigitosClienteActivo(ICompraService compraService);
        void RecalcularDigitosPermisoDTO(IPermisoService permisoService);
    }
}
