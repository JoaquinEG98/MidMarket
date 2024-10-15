namespace MidMarket.Business.Interfaces
{
    public interface IDigitoVerificadorService
    {
        void ActualizarDVV(string tabla);
        bool VerificarInconsistenciaTablas();
        void RecalcularDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService);
    }
}
