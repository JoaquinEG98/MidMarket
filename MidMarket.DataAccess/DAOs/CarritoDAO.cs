using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
namespace MidMarket.DataAccess.DAOs
{
    public class CarritoDAO : ICarritoDAO
    {
        private readonly BBDD _dataAccess;

        public CarritoDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public void InsertarCarrito(Activo activo, Cliente cliente)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_CARRITO;

            _dataAccess.ExecuteParameters.Parameters.Clear();
            
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", activo.Id);

            _dataAccess.ExecuteNonQuery();
        }
    }
}
