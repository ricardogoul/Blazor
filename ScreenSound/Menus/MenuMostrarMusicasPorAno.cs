using Microsoft.IdentityModel.Tokens;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicasPorAno : Menu
{
    public override void Executar(DAL<Musica> musicaDAL)
    {
        base.Executar(musicaDAL);
        ExibirTituloDaOpcao("Exibir Musicas Por Ano");
        Console.Write("Digite o ano desejado: ");
        string stringAnoDaMusica = Console.ReadLine()!;
        var anoDaMusica = Convert.ToInt32(stringAnoDaMusica);
        var musicas = musicaDAL.ListarPor(musica => musica.AnoLancamento.HasValue && musica.AnoLancamento.Equals(anoDaMusica));
        ;
        if (!musicas.IsNullOrEmpty())
        {
            Console.WriteLine($"\nMusicas do ano {anoDaMusica}:");
            foreach (var musica in musicas)
            {
                musica.ExibirFichaTecnica();
                Console.WriteLine("\n");
            }

            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO ano {anoDaMusica} não tem nenhuma música!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
