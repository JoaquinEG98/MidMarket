using MidMarket.DataAccess.Conexion;
using System;
using MidMarket.Entities;
using System.Collections.Generic;
using System.Data;
using MidMarket.DataAccess.Helpers;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Business.Seguridad;

namespace MidMarket.DataAccess.DAOs
{
    public class BitacoraDAO : IBitacoraDAO
    {
        private readonly BBDD _dataAccess;
        private IUsuarioDAO _usuarioDataAccess;

        public BitacoraDAO()
        {
            _dataAccess = BBDD.GetInstance;
            _usuarioDataAccess = new UsuarioDAO();
        }

        public int AltaBitacora(Bitacora bitacora)
        {
            _dataAccess.ExecuteCommandText = Scripts.ALTA_BITACORA;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@ClienteId", bitacora.Cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Descripcion", bitacora.Descripcion);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Criticidad", (int)bitacora.Criticidad);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Fecha", bitacora.Fecha);

            return _dataAccess.ExecuteNonEscalar();
        }

        public List<Bitacora> GetBitacora()
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_BITACORA);
            DataSet ds = _dataAccess.ExecuteNonReader();

            List<Bitacora> bitacora = new List<Bitacora>();
            List<Bitacora> bitacoraFull = new List<Bitacora>();

            if (ds.Tables[0].Rows.Count > 0)
                bitacora = BitacoraFill.FillListBitacora(ds);

            foreach (var item in bitacora)
            {
                Bitacora bitacoraItem = item;
                var cliente = _usuarioDataAccess.GetCliente(item.Cliente.Id);

                var clienteDesencriptado = new Cliente()
                {
                    Id  = cliente.Id,
                    Email = Encriptacion.DesencriptarAES(cliente.Email),
                    RazonSocial = Encriptacion.DesencriptarAES(cliente.RazonSocial),
                    CUIT = Encriptacion.DesencriptarAES(cliente.CUIT),
                    Puntaje = cliente.Puntaje,
                    Cuenta = cliente.Cuenta,
                };
                bitacoraItem.Cliente = clienteDesencriptado;

                bitacoraFull.Add(bitacoraItem);
            }

            return bitacora;
        }
    }
}
