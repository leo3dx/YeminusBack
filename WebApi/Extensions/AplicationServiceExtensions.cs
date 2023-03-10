using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Extensions {
    public static class AplicationServiceExtensions {
        public static void ConfigureCors ( this IServiceCollection services ) =>
            services.AddCors ( options => {
                options.AddPolicy ( "CorsPolicy", builder =>
                      builder.AllowAnyOrigin ( )
                      .AllowAnyMethod ( )
                      .AllowAnyHeader ( ) );
            } );
    }
}
