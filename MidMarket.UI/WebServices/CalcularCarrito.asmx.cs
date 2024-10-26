using System.Collections.Generic;
using System.Web.Services;

namespace MidMarket.UI.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class CalcularCarrito : System.Web.Services.WebService
    {

        [WebMethod]
        public decimal CalcularTotalCarrito(List<Entities.Carrito> carrito)
        {
            decimal total = 0;

            foreach (var item in carrito)
            {
                total += item.Total;
            }

            return total;
        }
    }
}
