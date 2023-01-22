using AutoMapper;
using Connection.Connection;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Productos {
    public class GetProductosById {
        public class Execute : IRequest<ProductoDto> {
            public string codigo { get; set; }
        }
        public class Managment : IRequestHandler<Execute,ProductoDto> {
            private readonly YeminusContext _context;
            private readonly IMapper _mapper;
            public Managment ( YeminusContext context, IMapper mapper ) { 
                _context = context;
                _mapper = mapper;
            }

            public async Task<ProductoDto> Handle ( Execute request, CancellationToken cancellationToken ) {
                var producto = await _context.Producto.Include(producto => producto.listaDePrecios).Where ( x => x.codigo == request.codigo).FirstOrDefaultAsync();
                if ( producto == null ) {
                    throw new Exception ( "No se encontro el producto" );
                }
                var productoDto = _mapper.Map<Producto, ProductoDto> ( producto);
                return productoDto;
            }
        }
    }
}
