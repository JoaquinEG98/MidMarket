using System.Linq;
using System.Reflection;
using System.Text;
using System;

namespace MidMarket.Business.Seguridad
{
    public class DigitoVerificador
    {
        public static string GenerarDVH(Object objeto)
        {
            int valor = 0;

            Type tipo = objeto.GetType();
            PropertyInfo[] propiedades = tipo.GetProperties();

            foreach (PropertyInfo propiedad in propiedades)
            {
                if (propiedad.Name != "Id" && propiedad.Name != "DVH" && propiedad.Name != "Items" && propiedad.Name != "Permisos" && propiedad.GetValue(objeto) != null)
                {
                    if (propiedad.SetMethod.IsVirtual)
                    {
                        Object objetoVirtual = propiedad.GetValue(objeto);
                        Type tipoObjeto = objetoVirtual.GetType();
                        PropertyInfo[] propiedadesObjetoVirtual = tipoObjeto.GetProperties();

                        foreach (PropertyInfo item in propiedadesObjetoVirtual)
                        {
                            if (item.Name == "Id")
                            {
                                byte[] valorBytes = Encoding.ASCII.GetBytes(item.GetValue(objetoVirtual).ToString());
                                valorBytes.ToList().ForEach(x => valor += x);
                                break;
                            }
                        }
                    }
                    else
                    {
                        byte[] valorBytes = Encoding.ASCII.GetBytes(propiedad.GetValue(objeto).ToString());
                        valorBytes.ToList().ForEach(x => valor += x);
                    }
                }
            }

            string dvh = Encriptacion.EncriptarAES(valor.ToString());
            return dvh;
        }
    }
}
