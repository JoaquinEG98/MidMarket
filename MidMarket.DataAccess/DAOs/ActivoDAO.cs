using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Helpers;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using System.Collections.Generic;
using System.Data;
using System;

namespace MidMarket.DataAccess.DAOs
{
    public class ActivoDAO : IActivoDAO
    {
        private readonly BBDD _dataAccess;

        public ActivoDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public int AltaAccion(Accion accion)
        {
            _dataAccess.ExecuteCommandText = Scripts.ADD_ACTIVO;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Nombre", accion.Nombre);

            int activoId = _dataAccess.ExecuteNonEscalar();

            _dataAccess.ExecuteParameters.Parameters.Clear();
            _dataAccess.ExecuteCommandText = Scripts.ADD_ACCION;

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Simbolo", accion.Simbolo);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Precio", accion.Precio);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", activoId);

            return _dataAccess.ExecuteNonEscalar();
        }

        public int AltaBono(Bono bono)
        {
            _dataAccess.ExecuteCommandText = Scripts.ADD_ACTIVO;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Nombre", bono.Nombre);

            int activoId = _dataAccess.ExecuteNonEscalar();

            _dataAccess.ExecuteParameters.Parameters.Clear();
            _dataAccess.ExecuteCommandText = Scripts.ADD_BONO;

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@ValorNominal", bono.ValorNominal);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@TasaInteres", bono.TasaInteres);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", activoId);

            return _dataAccess.ExecuteNonEscalar();
        }

        public List<Accion> GetAcciones()
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_ACCIONES);
            DataSet ds = _dataAccess.ExecuteNonReader();

            List<Accion> acciones = new List<Accion>();

            if (ds.Tables[0].Rows.Count > 0)
                acciones = ActivoFill.FillListAccion(ds);

            return acciones;
        }

        public List<Bono> GetBonos()
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_BONOS);
            DataSet ds = _dataAccess.ExecuteNonReader();

            List<Bono> bonos = new List<Bono>();

            if (ds.Tables[0].Rows.Count > 0)
                bonos = ActivoFill.FillListBono(ds);

            return bonos;
        }
    }
}
