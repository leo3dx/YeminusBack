namespace Entities
{
    public class Producto
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public bool productoParaLaVenta { get; set; }
        public int porcentajeIva { get; set; }
        public ICollection<Precio> listaDePrecios { get; set; }
    }
}
