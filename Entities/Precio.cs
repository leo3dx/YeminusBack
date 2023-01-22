namespace Entities
{
    public class Precio
    {
        public int id { get; set; }
        public decimal precio { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
