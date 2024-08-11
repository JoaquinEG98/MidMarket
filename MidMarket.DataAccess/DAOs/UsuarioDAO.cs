using MidMarket.DataAccess.Interfaces;
using System;
using MidMarket.Entities;
using MidMarket.DataAccess.Conexion;

namespace MidMarket.DataAccess.DAOs
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private readonly BBDD _dataAccess;

        public UsuarioDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public int RegistrarUsuario(Cliente cliente)
        {
            _dataAccess.ExecuteCommandText = Scripts.REGISTRAR_USUARIO;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Email", cliente.Email);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Password", cliente.Password);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Nombre", cliente.RazonSocial);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@CUIT", cliente.CUIT);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@DVH", cliente.DVH);

            return _dataAccess.ExecuteNonEscalar();
        }
    }
}
