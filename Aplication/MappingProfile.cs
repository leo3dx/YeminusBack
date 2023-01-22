using Aplication.Precios;
using Aplication.Productos;
using AutoMapper;
using Entities;

namespace Aplication
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Producto, ProductoDto>()
                .ForMember(productoDto => productoDto.listaDePrecios,
                           producto => producto.MapFrom( x => x.listaDePrecios) );
            CreateMap<Precio, PrecioDto>();
        }
    }
}
