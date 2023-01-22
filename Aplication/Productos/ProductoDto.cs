using Aplication.Precios;
using Entities;

namespace Aplication.Productos
{
    public class ProductoDto
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public bool productoParaLaVenta { get; set; }
        public int porcentajeIva { get; set; }
        public ICollection<PrecioDto> listaDePrecios { get; set; }
    }
}
