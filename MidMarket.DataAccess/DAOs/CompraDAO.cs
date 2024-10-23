using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Seguridad;

namespace MidMarket.DataAccess.DAOs
{
    public class CompraDAO : ICompraDAO
    {
        private readonly BBDD _dataAccess;

        public CompraDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public int InsertarDetalleCompra(Carrito carrito, int idCompra)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_DETALLE_COMPRA;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", carrito.Activo.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Compra", idCompra);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Cantidad", carrito.Cantidad);


            if (carrito.Activo is Accion accion)
            {
                _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Precio", accion.Precio);
            }
            else if (carrito.Activo is Bono bono)
            {
                _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Precio", bono.ValorNominal);
            }

            return _dataAccess.ExecuteNonEscalar();
        }

        public int InsertarTransaccionCompra(Cliente cliente, decimal total)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_TRANSACCION_COMPRA;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cuenta", cliente.Cuenta.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Fecha", ClockWrapper.Now());
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Total", total);

            return _dataAccess.ExecuteNonEscalar();
        }

        public int InsertarActivoCliente(Cliente cliente, Carrito carrito)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_ACTIVO_CLIENTE;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", carrito.Activo.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Cantidad", carrito.Cantidad);

            return _dataAccess.ExecuteNonEscalar();
        }
    }
}
