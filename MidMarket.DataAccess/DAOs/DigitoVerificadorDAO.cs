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

                case "DETALLECOMPRA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "DetalleCompra");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtDetalleCompra = ds.Tables[0];
                    if (dtDetalleCompra.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtDetalleCompra.Rows)
                        {
                            DetalleCompra detalleCompra = new DetalleCompra()
                            {
                                Cantidad = Convert.ToInt32(rows["Cantidad"].ToString()),
                                Precio = Convert.ToDecimal(rows["Precio"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(detalleCompra.DVH);
                        }
                    }

                    break;

                case "TRANSACCIONCOMPRA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "TransaccionCompra");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtTransaccionCompra = ds.Tables[0];
                    if (dtTransaccionCompra.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtTransaccionCompra.Rows)
                        {
                            TransaccionCompra transaccionCompra = new TransaccionCompra()
                            {
                                Fecha = Convert.ToDateTime(rows["Fecha"].ToString()),
                                Total = Convert.ToDecimal(rows["Total"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(transaccionCompra.DVH);
                        }
                    }

                    break;

                case "CLIENTEACTIVO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Cliente_Activo");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtClienteActivo = ds.Tables[0];
                    if (dtClienteActivo.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtClienteActivo.Rows)
                        {
                            ClienteActivoDTO clienteActivo = new ClienteActivoDTO()
                            {
                                Id_Cliente = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                Id_Activo = Convert.ToInt32(rows["Id_Activo"].ToString()),
                                Cantidad = Convert.ToInt32(rows["Cantidad"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(clienteActivo.DVH);
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

                case "DETALLECOMPRA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "DetalleCompra");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtDetalleCompra = ds.Tables[0];
                    if (dtDetalleCompra.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtDetalleCompra.Rows)
                        {
                            DetalleCompra detalleCompra = new DetalleCompra()
                            {
                                Cantidad = Convert.ToInt32(rows["Cantidad"].ToString()),
                                Precio = Convert.ToDecimal(rows["Precio"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(detalleCompra.DVH);
                        }
                    }

                    break;

                case "TRANSACCIONCOMPRA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "TransaccionCompra");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtTransaccionCompra = ds.Tables[0];
                    if (dtTransaccionCompra.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtTransaccionCompra.Rows)
                        {
                            TransaccionCompra transaccionCompra = new TransaccionCompra()
                            {
                                Fecha = Convert.ToDateTime(rows["Fecha"].ToString()),
                                Total = Convert.ToDecimal(rows["Total"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(transaccionCompra.DVH);
                        }
                    }

                    break;

                case "CLIENTEACTIVO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Cliente_Activo");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtClienteActivo = ds.Tables[0];
                    if (dtClienteActivo.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtClienteActivo.Rows)
                        {
                            ClienteActivoDTO clienteActivo = new ClienteActivoDTO()
                            {
                                Id_Cliente = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                Id_Activo = Convert.ToInt32(rows["Id_Activo"].ToString()),
                                Cantidad = Convert.ToInt32(rows["Cantidad"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(clienteActivo.DVH);
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

                case "DETALLECOMPRA":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "DetalleCompra");
                    return _dataAccess.ExecuteNonEscalar();

                case "TRANSACCIONCOMPRA":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "TransaccionCompra");
                    return _dataAccess.ExecuteNonEscalar();

                case "CLIENTEACTIVO":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "Cliente_Activo");
                    return _dataAccess.ExecuteNonEscalar();

                default: return 0;
            }
        }

        public string ObtenerDVV(string tabla)
        {
            string dvv = "";

            switch (tabla.ToUpper())
            {
                case "CLIENTE":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "Cliente");
                    break;

                case "USUARIOPERMISO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "UsuarioPermiso");
                    break;

                case "DETALLECOMPRA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "DetalleCompra");
                    break;

                case "TRANSACCIONCOMPRA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "TransaccionCompra");
                    break;

                case "CLIENTEACTIVO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "Cliente_Activo");
                    break;
            }

            DataSet ds = _dataAccess.ExecuteNonReader();
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    dvv = rows["Valor"].ToString();
                }
            }

            return dvv;
        }

        public int ActualizarTablaDVH(string tabla, string nuevoDVH, int objetoId)
        {
            if (tabla.ToUpper() == "CLIENTE")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Cliente WHERE Id_Cliente = @parId";

            if (tabla.ToUpper() == "USUARIOPERMISO")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Usuario_Permiso WHERE Id_Usuario_Permiso = @parId";

            if (tabla.ToUpper() == "DETALLECOMPRA")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Detalle WHERE Id_Detalle = @parId";

            if (tabla.ToUpper() == "TRANSACCIONCOMPRA")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Compra WHERE Id_Compra = @parId";

            if (tabla.ToUpper() == "CLIENTEACTIVO")
                _dataAccess.ExecuteCommandText = $"UPDATE Cliente_Activo SET DVH = @parDVH OUTPUT inserted.Id_Cliente_Activo WHERE Id_Cliente_Activo = @parId";

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@parDVH", nuevoDVH);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@parId", objetoId);

            return _dataAccess.ExecuteNonEscalar();
        }
    }
}
