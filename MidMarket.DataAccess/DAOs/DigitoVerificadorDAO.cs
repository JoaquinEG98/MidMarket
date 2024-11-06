using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Enums;
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

                case "DETALLEVENTA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "DetalleVenta");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtDetalleVenta = ds.Tables[0];
                    if (dtDetalleVenta.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtDetalleVenta.Rows)
                        {
                            DetalleVenta detalleVenta = new DetalleVenta()
                            {
                                Cantidad = Convert.ToInt32(rows["Cantidad"].ToString()),
                                Precio = Convert.ToDecimal(rows["Precio"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(detalleVenta.DVH);
                        }
                    }

                    break;

                case "TRANSACCIONVENTA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "TransaccionVenta");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtTransaccionVenta = ds.Tables[0];
                    if (dtTransaccionVenta.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtTransaccionVenta.Rows)
                        {
                            var transaccionVenta = new TransaccionVenta()
                            {
                                Fecha = Convert.ToDateTime(rows["Fecha"].ToString()),
                                Total = Convert.ToDecimal(rows["Total"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(transaccionVenta.DVH);
                        }
                    }

                    break;

                case "PERMISO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Permiso");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtPermiso = ds.Tables[0];
                    if (dtPermiso.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtPermiso.Rows)
                        {
                            string permisoString = rows["Permiso"] != null && !string.IsNullOrEmpty(rows["Permiso"].ToString())
                                                    ? rows["Permiso"].ToString()
                                                    : null;

                            var permisoDTO = new PermisoDTO()
                            {
                                Nombre = Convert.ToString(rows["Nombre"].ToString()),
                                Permiso = permisoString != null ? (Permiso)Enum.Parse(typeof(Permiso), permisoString) : default(Permiso),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(permisoDTO.DVH);
                        }
                    }

                    break;

                case "FAMILIAPATENTE":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "FamiliaPatente");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtFamiliaPatente = ds.Tables[0];
                    if (dtFamiliaPatente.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtFamiliaPatente.Rows)
                        {
                            var familiaPatenteDTO = new FamiliaPatenteDTO()
                            {
                                Id_Padre = Convert.ToInt32(rows["Id_Padre"].ToString()),
                                Id_Hijo = Convert.ToInt32(rows["Id_Hijo"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString()),
                            };

                            dvhs.Add(familiaPatenteDTO.DVH);
                        }
                    }

                    break;

                case "CUENTA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Cuenta");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtCuenta = ds.Tables[0];
                    if (dtCuenta.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtCuenta.Rows)
                        {
                            var cuenta = new CuentaDTO()
                            {
                                Id = Convert.ToInt32(rows["Id_Cuenta"].ToString()),
                                Id_Cliente = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                NumeroCuenta = Convert.ToInt64(rows["NumeroCuenta"].ToString()),
                                Saldo = Convert.ToDecimal(rows["Saldo"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString()),
                            };

                            dvhs.Add(cuenta.DVH);
                        }
                    }

                    break;

                case "CARRITO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Carrito");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtCarrito = ds.Tables[0];
                    if (dtCarrito.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtCarrito.Rows)
                        {
                            var carrito = new CarritoDTO()
                            {
                                Id = Convert.ToInt32(rows["Id_Carrito"].ToString()),
                                Id_Activo = Convert.ToInt32(rows["Id_Activo"].ToString()),
                                Id_Cliente = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                Cantidad = Convert.ToInt32(rows["Cantidad"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString()),
                            };

                            dvhs.Add(carrito.DVH);
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

                case "DETALLEVENTA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "DetalleVenta");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtDetalleVenta = ds.Tables[0];
                    if (dtDetalleVenta.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtDetalleVenta.Rows)
                        {
                            var detalleVenta = new DetalleVenta()
                            {
                                Cantidad = Convert.ToInt32(rows["Cantidad"].ToString()),
                                Precio = Convert.ToDecimal(rows["Precio"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(detalleVenta.DVH);
                        }
                    }

                    break;

                case "TRANSACCIONVENTA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "TransaccionVenta");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtTransaccionVenta = ds.Tables[0];
                    if (dtTransaccionVenta.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtTransaccionVenta.Rows)
                        {
                            var transaccionVenta = new TransaccionVenta()
                            {
                                Fecha = Convert.ToDateTime(rows["Fecha"].ToString()),
                                Total = Convert.ToDecimal(rows["Total"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(transaccionVenta.DVH);
                        }
                    }

                    break;

                case "PERMISO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Permiso");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtPermiso = ds.Tables[0];
                    if (dtPermiso.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtPermiso.Rows)
                        {
                            string permisoString = rows["Permiso"] != null && !string.IsNullOrEmpty(rows["Permiso"].ToString())
                                                    ? rows["Permiso"].ToString()
                                                    : null;

                            var permisoDTO = new PermisoDTO()
                            {
                                Nombre = Convert.ToString(rows["Nombre"].ToString()),
                                Permiso = permisoString != null ? (Permiso)Enum.Parse(typeof(Permiso), permisoString) : default(Permiso),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(permisoDTO.DVH);
                        }
                    }

                    break;

                case "FAMILIAPATENTE":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "FamiliaPatente");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtFamiliaPatenteDTO = ds.Tables[0];
                    if (dtFamiliaPatenteDTO.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtFamiliaPatenteDTO.Rows)
                        {
                            var familiaPatenteDTO = new FamiliaPatenteDTO()
                            {
                                Id_Padre = Convert.ToInt32(rows["Id_Padre"].ToString()),
                                Id_Hijo = Convert.ToInt32(rows["Id_Hijo"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString())
                            };

                            dvhs.Add(familiaPatenteDTO.DVH);
                        }
                    }

                    break;

                case "CUENTA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Cuenta");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtCuenta = ds.Tables[0];
                    if (dtCuenta.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtCuenta.Rows)
                        {
                            var cuenta = new CuentaDTO()
                            {
                                Id = Convert.ToInt32(rows["Id_Cuenta"].ToString()),
                                Id_Cliente = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                NumeroCuenta = Convert.ToInt64(rows["NumeroCuenta"].ToString()),
                                Saldo = Convert.ToDecimal(rows["Saldo"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString()),
                            };

                            dvhs.Add(cuenta.DVH);
                        }
                    }

                    break;

                case "CARRITO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITOS_HORIZONTALES, "Carrito");
                    ds = _dataAccess.ExecuteNonReader();

                    DataTable dtCarrito = ds.Tables[0];
                    if (dtCarrito.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dtCarrito.Rows)
                        {
                            var carrito = new CarritoDTO()
                            {
                                Id = Convert.ToInt32(rows["Id_Carrito"].ToString()),
                                Id_Activo = Convert.ToInt32(rows["Id_Activo"].ToString()),
                                Id_Cliente = Convert.ToInt32(rows["Id_Cliente"].ToString()),
                                Cantidad = Convert.ToInt32(rows["Cantidad"].ToString()),
                                DVH = Convert.ToString(rows["DVH"].ToString()),
                            };

                            dvhs.Add(carrito.DVH);
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

                case "DETALLEVENTA":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "DetalleVenta");
                    return _dataAccess.ExecuteNonEscalar();

                case "TRANSACCIONVENTA":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "TransaccionVenta");
                    return _dataAccess.ExecuteNonEscalar();

                case "PERMISO":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "Permiso");
                    return _dataAccess.ExecuteNonEscalar();

                case "FAMILIAPATENTE":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "FamiliaPatente");
                    return _dataAccess.ExecuteNonEscalar();

                case "CUENTA":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "Cuenta");
                    return _dataAccess.ExecuteNonEscalar();

                case "CARRITO":
                    _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Tabla", "Carrito");
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

                case "DETALLEVENTA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "DetalleVenta");
                    break;

                case "TRANSACCIONVENTA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "TransaccionVenta");
                    break;

                case "PERMISO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "Permiso");
                    break;

                case "FAMILIAPATENTE":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "FamiliaPatente");
                    break;

                case "CUENTA":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "Cuenta");
                    break;

                case "CARRITO":
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_DIGITO_VERTICAL, "Carrito");
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

            if (tabla.ToUpper() == "DETALLEVENTA")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Detalle WHERE Id_Detalle = @parId";

            if (tabla.ToUpper() == "TRANSACCIONVENTA")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Venta WHERE Id_Venta = @parId";

            if (tabla.ToUpper() == "PERMISO")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Permiso WHERE Id_Permiso = @parId";

            if (tabla.ToUpper() == "CUENTA")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Cuenta WHERE Id_Cuenta = @parId";

            if (tabla.ToUpper() == "CARRITO")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Carrito WHERE Id_Carrito = @parId";

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@parDVH", nuevoDVH);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@parId", objetoId);

            return _dataAccess.ExecuteNonEscalar();
        }

        public int ActualizarTablaDVHFamiliaPatente(string tabla, string nuevoDVH, int objetoId, int idSecundario)
        {
            if (tabla.ToUpper() == "FAMILIAPATENTE")
                _dataAccess.ExecuteCommandText = $"UPDATE {tabla} SET DVH = @parDVH OUTPUT inserted.Id_Padre WHERE Id_Padre = @parId AND Id_Hijo = @parIdHijo";

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@parDVH", nuevoDVH);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@parId", objetoId);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@parIdHijo", idSecundario);

            return _dataAccess.ExecuteNonEscalar();
        }
    }
}
