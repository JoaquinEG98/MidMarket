using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Helpers;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Data;

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
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@DVH", bitacora.DVH);

            return _dataAccess.ExecuteNonEscalar();
        }

        public List<Bitacora> ObtenerBitacora()
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
                    Id = cliente.Id,
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

        public List<BitacoraDTO> GetAllBitacora()
        {
            var bitacora = new List<BitacoraDTO>();

            _dataAccess.SelectCommandText = String.Format(Scripts.GET_ALL_BITACORA);
            DataSet ds = _dataAccess.ExecuteNonReader();

            bitacora = new List<BitacoraDTO>();

            if (ds.Tables[0].Rows.Count > 0)
                bitacora = BitacoraFill.FillListBitacoraDTO(ds);

            return bitacora;
        }

        public List<Bitacora> LimpiarBitacora()
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.LIMPIAR_BITACORA);
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
                    Id = cliente.Id,
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
