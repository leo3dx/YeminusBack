using AutoMapper;
using Connection.Connection;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Productos
{
    public class GetProductos
    {
        public class Execute : IRequest<List<ProductoDto>>
        {
        }
        public class Managment : IRequestHandler<Execute, List<ProductoDto>>
        {
            private readonly YeminusContext _context;
            private readonly IMapper _mapper;
            public Managment (YeminusContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ProductoDto>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var productos = await _context.Producto
                    .Include(producto => producto.listaDePrecios).ToListAsync();
                var productoDto = _mapper.Map<List<Producto>, List<ProductoDto>>(productos);
                return productoDto;
            }
        }
    }
}
