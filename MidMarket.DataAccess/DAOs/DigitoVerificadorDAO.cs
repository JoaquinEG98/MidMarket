using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Data;

namespace MidMarket.DataAccess.DAOs
{
    public class DigitoVerificadorDAO : IDigitoVerificadorDAO
    {
        private readonly BBDD _dataAccess;

        public DigitoVerificadorDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public List<string> ObtenerDVH(string tabla)
        {
            List<string> dvhs = new List<string>();
            DataSet ds = new DataSet();

            switch (tabla.ToUpper())
            {
                case "CLIENTE":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Cliente");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtCliente = ds.Tables[0];
                    if (dtCliente.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtCliente.Rows)
                        {
                            Cliente cliente = new Cliente()
                            {
                                Id = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                Email = rows["Email"].ToString(),
                                Password = rows["Password"].ToString(),
                                RazonSocial = rows["Nombre"].ToString(),
                                CUIT = rows["CUIT"].ToString(),
                                Puntaje = Convert.ToDouble(rows["Puntaje"].ToString())
                            };
                            cliente.DVH = DigitoVerificador.GenerarDVH(cliente);

                            dvhs.Add(cliente.DVH);
                        }
                    }
                    break;

                case "USUARIOPERMISO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "UsuarioPermiso");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtUsuarioPermiso = ds.Tables[0];
                    if (dtUsuarioPermiso.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtUsuarioPermiso.Rows)
                        {
                            UsuarioPermisoDTO usuarioPermiso = new UsuarioPermisoDTO()
                            {
                                UsuarioId = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                PermisoId = Convert.ToInt32(rows["Id_Patente"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(usuarioPermiso.DVH);
                        }
                    }

                    break;
            }

            return dvhs;
        }

        public List<string> ObtenerDVHActuales(string tabla)
        {
            List<string> dvhs = new List<string>();
            DataSet ds = new DataSet();

            switch (tabla.ToUpper())
            {
                case "CLIENTE":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Cliente");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtCliente = ds.Tables[0];
                    if (dtCliente.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtCliente.Rows)
                        {
                            Cliente cliente = new Cliente()
                            {
                                Id = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                Email = rows["Email"].ToString(),
                                Password = rows["Password"].ToString(),
                                RazonSocial = rows["Nombre"].ToString(),
                                CUIT = rows["CUIT"].ToString(),
                                Puntaje = Convert.ToDouble(rows["Puntaje"].ToString()),
                                DVH = rows["DVH"].ToString(),
                            };

                            dvhs.Add(cliente.DVH);
                        }
                    }
                    break;

                case "USUARIOPERMISO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "UsuarioPermiso");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtUsuarioPermiso = ds.Tables[0];
                    if (dtUsuarioPermiso.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtUsuarioPermiso.Rows)
                        {
                            UsuarioPermisoDTO usuarioPermiso = new UsuarioPermisoDTO()
                            {
                                UsuarioId = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                PermisoId = Convert.ToInt32(rows["Id_Patente"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(usuarioPermiso.DVH);
                        }
                    }

                    break;
            }

            return dvhs;
        }

        public int ActualizarDVV(string tabla, string nuevoDVV)
        {
            _dataAccess.ExecuteCommandText = Scripts.UPDATE_DIGITO_VERTICAL;
            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Valor", nuevoDVV);

            switch (tabla.ToUpper())
            {
                case "CLIENTE":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "Cliente");
                    return _dataAccess.ExecuteNonEscalar();

                case "USUARIOPERMISO":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "UsuarioPermiso");
                    return _dataAccess.ExecuteNonEscalar();

                default: return 0;
            }
        }
    }
}
