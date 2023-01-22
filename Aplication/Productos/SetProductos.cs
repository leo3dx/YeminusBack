using Connection.Connection;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Productos
{
    public class SetProductos
    {
        public class Execute : IRequest
        {
            public string descripcion { get; set; }
            public string imagen { get; set; }
            public bool productoParaLaVenta { get; set; }
            public List<decimal>? listaDePrecios { get; set; }
            public int porcentajeIva { get; set; }
            public string codigo { get; set; }
        }
        public class Managment : IRequestHandler<Execute>
        {
            private readonly YeminusContext _context;
            public Managment (YeminusContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var lastProducto = await _context.Producto.OrderByDescending(t => t.id).FirstOrDefaultAsync();
                var lastPrecio = await _context.Precio.OrderByDescending ( t => t.id ).FirstOrDefaultAsync ( );
                int idPrecio = lastPrecio == null ? 1 : lastPrecio.id +1;

                var result = await _context.Producto.Where(x => x.codigo == request.codigo).FirstOrDefaultAsync();
                if (result != null)
                {
                    throw new Exception("Producto ya se encuentra registrado");
                }
                var producto = new Producto
                {
                    id = lastProducto == null? 1 : lastProducto.id +1,
                    descripcion = request.descripcion,
                    imagen = request.imagen,
                    porcentajeIva = request.porcentajeIva,
                    codigo = request.codigo,
                    productoParaLaVenta = request.productoParaLaVenta
                };
                _context.Producto.Add(producto);

                if (request.listaDePrecios != null)

                    foreach (var valor in request.listaDePrecios)
                    {
                        var precio = new Precio
                        {
                            id = idPrecio,
                            precio = valor,
                            ProductoId = producto.id
                        };
                        _context.Precio.Add(precio);
                        idPrecio += 1;
                    }
                
                var save = await _context.SaveChangesAsync();
                if (save > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo realizar la inserción");
            }
        }
    }
}
