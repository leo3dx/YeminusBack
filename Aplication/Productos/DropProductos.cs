using Connection.Connection;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Productos {
    public class DropProductos {
        public class Execute : IRequest {
            public string codigo { get; set; }
        }
        public class Managment : IRequestHandler<Execute> {
            private readonly YeminusContext _context;
            public Managment ( YeminusContext context ) { 
                _context = context;
            }
            public async Task<Unit> Handle ( Execute request, CancellationToken cancellationToken ) {
                var producto = await _context.Producto.Where ( x => x.codigo == request.codigo ).FirstOrDefaultAsync ( );
                if ( producto == null ) {
                    throw new Exception ( "No se encontro el producto" );
                }
                var precios = await _context.Precio.Where ( x => x.ProductoId == producto.id ).ToListAsync ( );
                if ( precios != null ) {
                    foreach ( var precio in precios ) {
                        _context.Precio.Remove ( precio );
                    }
                }
                _context.Producto.Remove ( producto );
                var result = await _context.SaveChangesAsync ( );
                if (result > 0) {
                    return Unit.Value;
                }
                throw new Exception ( "No se pudo eliminar el producto" );
            }
        }
    }
}
