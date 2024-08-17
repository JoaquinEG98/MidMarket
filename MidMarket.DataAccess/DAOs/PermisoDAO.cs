using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Helpers;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MidMarket.DataAccess.DAOs
{
    public class PermisoDAO : IPermisoDAO
    {
        private readonly BBDD _dataAccess;

        public PermisoDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public int GuardarPatenteFamilia(Componente componente, bool familia)
        {
            _dataAccess.ExecuteCommandText = Scripts.GUARDAR_COMPONENTE;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Nombre", componente.Nombre);
            if (familia) _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Permiso", DBNull.Value);
            else _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Permiso", componente.Permiso.ToString());

            return _dataAccess.ExecuteNonEscalar();
        }

        public void GuardarFamiliaCreada(Familia familia)
        {
            _dataAccess.ExecuteCommandText = Scripts.BORRAR_FAMILIA;
            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@PadreId", familia.Id);

            _dataAccess.ExecuteNonQuery();

            foreach (Componente item in familia.Hijos)
            {
                _dataAccess.ExecuteCommandText = Scripts.GUARDAR_FAMILIA;
                _dataAccess.ExecuteParameters.Parameters.Clear();

                _dataAccess.ExecuteParameters.Parameters.AddWithValue("@PadreId", familia.Id);
                _dataAccess.ExecuteParameters.Parameters.AddWithValue("@HijoId", item.Id);
                _dataAccess.ExecuteNonQuery();
            }
        }

        public IList<Familia> GetFamilias()
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_FAMILIAS);
            DataSet ds = _dataAccess.ExecuteNonReader();
            IList<Familia> familias = new List<Familia>();

            if (ds.Tables[0].Rows.Count > 0)
                familias = PermisoFill.FillListFamilia(ds);

            return familias;
        }

        public IList<Patente> GetPatentes()
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_PATENTES);
            DataSet ds = _dataAccess.ExecuteNonReader();

            IList<Patente> patentes = new List<Patente>();

            if (ds.Tables[0].Rows.Count > 0)
                patentes = PermisoFill.FillListPatente(ds);

            return patentes;
        }

        public IList<Componente> TraerFamiliaPatentes(int familiaId)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_FAMILIA_PATENTE, familiaId);
            DataSet ds = _dataAccess.ExecuteNonReader();
            DataTable dt = ds.Tables[0];


            List<Componente> componentes = new List<Componente>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    int padreId = 0;
                    if (rows["Id_Padre"] != DBNull.Value)
                    {
                        padreId = int.Parse(rows["Id_Padre"].ToString());
                    }

                    int Id = int.Parse(rows["Id_Permiso"].ToString());
                    string nombre = rows["Nombre"].ToString();
                    string permiso = string.Empty;
                    if (rows["Permiso"] != DBNull.Value) permiso = rows["Permiso"].ToString();

                    Componente componente;
                    if (string.IsNullOrEmpty(permiso)) componente = new Familia();
                    else componente = new Patente();

                    componente.Id = Id;
                    componente.Nombre = nombre;
                    if (!string.IsNullOrEmpty(permiso)) componente.Permiso = (Permiso)Enum.Parse(typeof(Permiso), permiso);

                    Componente padre = GetComponente(padreId, componentes);
                    if (padre == null) componentes.Add(componente);
                    else padre.AgregarHijo(componente);
                }
            }
            return componentes;
        }

        private Componente GetComponente(int id, IList<Componente> lista)
        {
            Componente componente = lista != null ? lista.Where(i => i.Id.Equals(id)).FirstOrDefault() : null;

            if (componente == null && lista != null)
            {
                foreach (var item in lista)
                {
                    var _lista = GetComponente(id, item.Hijos);
                    if (_lista != null && _lista.Id == id) return _lista;
                    else if (_lista != null) return GetComponente(id, _lista.Hijos);
                }
            }
            return componente;
        }

        public void GetComponenteUsuario(Cliente cliente)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_USUARIO_PERMISO, cliente.Id);
            DataSet ds = _dataAccess.ExecuteNonReader();
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    int id = int.Parse(rows["Id_Permiso"].ToString());
                    string nombre = rows["Nombre"].ToString();
                    string permiso = String.Empty;
                    if (rows["Permiso"].ToString() != String.Empty) permiso = rows["Permiso"].ToString();

                    Componente componente;
                    if (!String.IsNullOrEmpty(permiso))
                    {
                        componente = new Patente();
                        componente.Id = id;
                        componente.Nombre = nombre;
                        componente.Permiso = (Permiso)Enum.Parse(typeof(Permiso), permiso);
                        cliente.Permisos.Add(componente);
                    }
                    else
                    {
                        componente = new Familia();
                        componente.Id = id;
                        componente.Nombre = nombre;

                        var familia = TraerFamiliaPatentes(id);
                        foreach (var f in familia)
                        {
                            componente.AgregarHijo(f);
                        }

                        cliente.Permisos.Add(componente);
                    }
                }
            }
        }

        public void BorrarPermisoUsuario(Cliente cliente)
        {
            _dataAccess.ExecuteCommandText = Scripts.BORRAR_PERMISO_USUARIO;
            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@ClienteId", cliente.Id);

            _dataAccess.ExecuteNonQuery();
        }

        public void GuardarPermiso(Cliente cliente, Componente permiso, string DVH)
        {
            _dataAccess.ExecuteCommandText = Scripts.GUARDAR_PERMISO_USUARIO;
            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@ClienteId", cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@PatenteId", permiso.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@DVH", DVH);
            _dataAccess.ExecuteNonQuery();
        }

        public IList<Familia> GetFamiliasValidacion(int familiaId)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_FAMILIA_VALIDACION, familiaId);
            DataSet ds = _dataAccess.ExecuteNonReader();
            IList<Familia> familias = new List<Familia>();

            if (ds.Tables[0].Rows.Count > 0)
                familias = PermisoFill.FillListFamilia(ds);

            return familias;
        }

        public Componente GetFamiliaArbol(int familiaId, Componente componenteOriginal, Componente componenteAgregar)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_PATENTES_DE_FAMILIA, familiaId);
            DataSet ds = _dataAccess.ExecuteNonReader();
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rows in dt.Rows)
                {
                    int Id = int.Parse(rows["Id_Permiso"].ToString());
                    string nombre = rows["Nombre"].ToString();
                    string permiso = string.Empty;
                    if (rows["Permiso"] != DBNull.Value) permiso = rows["Permiso"].ToString();

                    Componente componente;
                    if (string.IsNullOrEmpty(permiso)) componente = new Familia();
                    else componente = new Patente();

                    componente.Id = Id;
                    componente.Nombre = nombre;
                    if (!string.IsNullOrEmpty(permiso)) componente.Permiso = (Permiso)Enum.Parse(typeof(Permiso), permiso);

                    if (componenteAgregar != null)
                    {
                        if (componente.GetType() == typeof(Patente)) componenteAgregar.AgregarHijo(componente);
                        else if (componente.GetType() == typeof(Familia)) LlenarComponenteFamilia(componente, componenteOriginal, componenteAgregar);
                    }
                    else
                    {
                        if (componente.GetType() == typeof(Patente)) componenteOriginal.AgregarHijo(componente);
                        else if (componente.GetType() == typeof(Familia)) LlenarComponenteFamilia(componente, componenteOriginal, componenteOriginal);
                    }
                }
            }

            return componenteOriginal;
        }

        private void LlenarComponenteFamilia(Componente componente, Componente componenteOriginal, Componente componenteRaiz)
        {
            Componente familia = new Familia();
            familia = componente;

            componenteRaiz.AgregarHijo(familia);

            GetFamiliaArbol(componente.Id, componenteOriginal, familia);
        }

        public Componente GetUsuarioArbol(int usuarioId, Componente componenteOriginal, Componente componenteAgregar)
        {
                _dataAccess.SelectCommandText = String.Format(Scripts.GET_PERMISOS_USUARIO, usuarioId);
                DataSet ds = _dataAccess.ExecuteNonReader();
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rows in dt.Rows)
                    {
                        int Id = int.Parse(rows["Id_Permiso"].ToString());
                        string nombre = rows["Nombre"].ToString();
                        string permiso = string.Empty;
                        if (rows["Permiso"] != DBNull.Value) permiso = rows["Permiso"].ToString();

                        Componente componente;
                        if (string.IsNullOrEmpty(permiso)) componente = new Familia();
                        else componente = new Patente();

                        componente.Id = Id;
                        componente.Nombre = nombre;
                        if (!string.IsNullOrEmpty(permiso)) componente.Permiso = (Permiso)Enum.Parse(typeof(Permiso), permiso);

                        if (componenteAgregar != null)
                        {
                            if (componente.GetType() == typeof(Patente)) componenteAgregar.AgregarHijo(componente);
                            else if (componente.GetType() == typeof(Familia)) LlenarComponenteFamilia(componente, componenteOriginal, componenteAgregar);
                        }
                        else
                        {
                            if (componente.GetType() == typeof(Patente)) componenteOriginal.AgregarHijo(componente);
                            else if (componente.GetType() == typeof(Familia)) LlenarComponenteFamilia(componente, componenteOriginal, componenteOriginal);
                        }
                    }
                }

                return componenteOriginal;
        }
    }
}
