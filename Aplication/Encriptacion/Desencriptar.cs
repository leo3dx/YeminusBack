using MediatR;

namespace Aplication.Encriptacion {
    public class Desencriptar {
        public class Execute : IRequest<string> {
            public int clave { get; set; }
            public string palabra { get; set; }
        }
        public class Managment : IRequestHandler<Execute, string> {
            public async Task<string> Handle ( Execute request, CancellationToken cancellationToken ) {

                var abecedario = "abcdefghijklmnñopqrstuvwxyz".ToCharArray ( 0, 27 );
                string palabra = request.palabra.ToLower ( );
                var palabraEncriptada = "";

                bool espacio = false;
                int posicionFinal = 0;

                for ( int i = 0; i < palabra.Length; i++ ) {
                    for ( int k = 0; k < abecedario.Length; k++ ) {
                        if ( palabra.Substring ( i, 1 ).Equals ( abecedario[k].ToString ( ) ) ) {
                            espacio = false;
                            posicionFinal = k - request.clave;
                            if ( posicionFinal < 0 ) {
                                posicionFinal = 27 + posicionFinal;
                            }
                            break;
                        } else {
                            espacio = true;
                        }
                    }
                    if ( espacio ) {
                        palabraEncriptada += " ";
                    } else {
                        palabraEncriptada += abecedario[posicionFinal];
                    }
                }
                return palabraEncriptada;

            }
        }
    }
}
