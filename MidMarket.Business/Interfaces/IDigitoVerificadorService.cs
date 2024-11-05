namespace MidMarket.Business.Interfaces
{
    public interface IDigitoVerificadorService
    {
        void ActualizarDVV(string tabla);
        bool VerificarInconsistenciaTablas();
        void RecalcularTodosDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService, ICompraService compraService);
        void RecalcularDigitosUsuario(IUsuarioService usuarioService, IPermisoService permisoService);
    }
}
