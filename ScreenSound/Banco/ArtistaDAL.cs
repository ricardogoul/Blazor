using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco
{
    internal class ArtistaDAL
    {
        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();
            using var connection = new ScreenSoundContext().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM Artistas";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idArtista = Convert.ToInt32(reader["Id"]);
                string nomeArtista = Convert.ToString(reader["Nome"]);
                string bioArtista = Convert.ToString(reader["Bio"]);

                Artista artista = new Artista(nomeArtista, bioArtista)
                {
                    Bio = bioArtista
                };

                lista.Add(artista);
            }
            return lista;
        }

        public void Adicionar(Artista artista)
        {
            using var connection = new ScreenSoundContext().ObterConexao();
            connection.Open();

            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";

            SqlCommand sqlCommand = new SqlCommand(sql, connection);

            sqlCommand.Parameters.AddWithValue("@nome", artista.Nome);
            sqlCommand.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
            sqlCommand.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }
    }
}
