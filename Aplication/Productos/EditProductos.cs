using Connection.Connection;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Productos {
    public class EditProductos {
        public class Execute : IRequest {
            public string codigo { get; set; }
            public string descripcion { get; set; }
            public string imagen { get; set; }
            public bool? productoParaLaVenta { get; set; }
            public int? porcentajeIva { get; set; }
            public List<decimal>? listaDePrecios { get; set; }
        }
        public class Managment : IRequestHandler<Execute> {
            private readonly YeminusContext _context;
            public Managment ( YeminusContext context ) { 
                _context = context;
            }
            public async Task<Unit> Handle ( Execute request, CancellationToken cancellationToken ) {
                var producto = await _context.Producto.Where ( x => x.codigo == request.codigo).FirstOrDefaultAsync();
                var lastPrecio = await _context.Precio.OrderByDescending ( t => t.id ).FirstOrDefaultAsync ( );
                int idPrecio = lastPrecio == null ? 1 : lastPrecio.id + 1;
                if ( producto == null ) {
                    throw new Exception ( "No se encontro el producto" );
                }
                producto.descripcion = request.descripcion ?? producto.descripcion;
                producto.imagen = request.imagen ?? producto.imagen;
                producto.productoParaLaVenta = request.productoParaLaVenta ?? producto.productoParaLaVenta;
                producto.porcentajeIva = request.porcentajeIva ?? producto.porcentajeIva;
                if ( request.listaDePrecios != null ) {
                    if ( request.listaDePrecios.Count > 0 ) {
                        var precios = await _context.Precio.Where ( x => x.ProductoId == producto.id ).ToListAsync ( );
                        if ( precios != null ) {
                            foreach ( var precio in precios ) {
                                _context.Precio.Remove ( precio );
                            }
                        }
                        foreach ( var precio in request.listaDePrecios ) {
                            var newPrecio = new Precio {
                                id = idPrecio,
                                precio = precio,
                                ProductoId = producto.id
                            };
                            idPrecio += 1;
                            _context.Precio.Add ( newPrecio);
                        }
                    }
                } else {
                    var precios = await _context.Precio.Where ( x => x.ProductoId == producto.id ).ToListAsync ( );
                    if ( precios != null ) {
                        foreach ( var precio in precios ) {
                            _context.Precio.Remove ( precio );
                        }
                    }
                }
                var result = await _context.SaveChangesAsync ();
                if ( result > 0 ) {
                    return Unit.Value;
                }
                throw new Exception ( "No se pudo actualizar el producto" );
            }
        }
    }
}
