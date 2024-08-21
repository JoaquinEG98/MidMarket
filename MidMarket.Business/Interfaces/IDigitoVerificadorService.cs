using MidMarket.Business.Services;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IDigitoVerificadorService
    {
        void ActualizarDVV(string tabla);
        bool ValidarDigitosVerificadores(string tabla);
        void RecalcularDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService);
    }
}
