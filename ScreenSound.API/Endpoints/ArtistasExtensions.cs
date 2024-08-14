using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndpointsArtistas(this WebApplication app)
        {
            app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
            {
                var artista = dal.RecuperarPor(artista => artista.Nome.ToUpper().Equals(nome.ToUpper()));
                if (artista is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(artista);
            });

            app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) =>
            {
                dal.Adicionar(artista);
                return Results.Ok();
            });

            app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
            {
                var artista = dal.RecuperarPor(artista => artista.Id.Equals(id));
                if (artista is null)
                {
                    return Results.NotFound();
                }

                dal.Deletar(artista);
                return Results.Ok();
            });

            app.MapPatch("/Artistas/{id}", ([FromServices] DAL<Artista> dal, [FromBody] Artista artistaEditado, int id) =>
            {
                var artista = dal.RecuperarPor(artista => artista.Id.Equals(id));
                if (artista is null)
                {
                    return Results.NotFound();
                }

                artista.Nome = artistaEditado.Nome != "" ? artistaEditado.Nome : artista.Nome;
                artista.Bio = artistaEditado.Bio != "" ? artistaEditado.Bio : artista.Bio;
                artista.FotoPerfil = artistaEditado.FotoPerfil != "" ? artistaEditado.FotoPerfil : artista.FotoPerfil;

                dal.Atualizar(artista);
                return Results.Ok();
            });
        }
    }
}
