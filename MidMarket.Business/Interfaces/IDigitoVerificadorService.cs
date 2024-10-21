namespace MidMarket.Business.Interfaces
{
    public interface IDigitoVerificadorService
    {
        void ActualizarDVV(string tabla);
        bool VerificarInconsistenciaTablas();
        void RecalcularTodosDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService);
        void RecalcularDigitosUsuario(IUsuarioService usuarioService, IPermisoService permisoService);
    }
}
