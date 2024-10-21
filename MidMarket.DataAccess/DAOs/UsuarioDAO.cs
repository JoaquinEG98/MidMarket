using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Helpers;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using System;
using System.Collections.Generic;
using System.Data;

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
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Bloqueo", cliente.Bloqueo);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@DVH", cliente.DVH);

            return _dataAccess.ExecuteNonEscalar();
        }

        public Cliente Login(string email)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.LOGIN_USUARIO, email);

            DataSet ds = _dataAccess.ExecuteNonReader();
            Cliente cliente = ds.Tables[0].Rows.Count <= 0 ? null : ClienteFill.FillObjectCliente(ds.Tables[0].Rows[0]);

            return cliente;
        }

        public List<Cliente> GetClientes()
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_USUARIOS);
            DataSet ds = _dataAccess.ExecuteNonReader();

            List<Cliente> clientes = new List<Cliente>();

            if (ds.Tables[0].Rows.Count > 0)
                clientes = ClienteFill.FillListCliente(ds);

            return clientes;
        }

        public Cliente GetCliente(int clienteId)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_USUARIO, clienteId);

            DataSet ds = _dataAccess.ExecuteNonReader();
            Cliente cliente = ds.Tables[0].Rows.Count <= 0 ? null : ClienteFill.FillObjectCliente(ds.Tables[0].Rows[0]);

            return cliente;
        }

        public void ActualizarBloqueo(int clienteId)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.UPDATE_BLOQUEO, clienteId);

            DataSet ds = _dataAccess.ExecuteNonReader();
        }

        public void AumentarBloqueo(int clienteId)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.AUMENTAR_BLOQUEO, clienteId);

            DataSet ds = _dataAccess.ExecuteNonReader();
        }
    }
}
