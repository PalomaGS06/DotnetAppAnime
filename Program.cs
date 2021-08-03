using System;

namespace DIO.Animes
{
    class Program
    {
        static AnimeRepositorio repositorio = new AnimeRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcao();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarAnimes();
						break;
					case "2":
						InserirAnime();
						break;
					case "3":
						AtualizarAnime();
						break;
					case "4":
						ExcluirAnime();
						break;
					case "5":
						VisualizarAnime();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException(); // sai da aplicação, quando não digita o "x"
				}

				opcaoUsuario = ObterOpcao();
			}

			Console.WriteLine("Obrigado(a) por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static void ExcluirAnime()
		{
			Console.Write("Digite o id do anime: ");
			int indiceAnime = int.Parse(Console.ReadLine());

			repositorio.Exclui(indiceAnime);
		}

        private static void VisualizarAnime()
		{
			Console.Write("Digite o id do anime: ");
			int indiceAnime = int.Parse(Console.ReadLine()); //mostra as descrições do anime, após digitar o id.

			var anime = repositorio.RetornaPorId(indiceAnime);

			Console.WriteLine(anime);
		}

        private static void AtualizarAnime()
		{
			Console.Write("Digite o id do anime: ");
			int indiceAnime = int.Parse(Console.ReadLine());

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título do Anime: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início do Anime: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição do Anime: ");
			string entradaDescricao = Console.ReadLine();

			Anime atualizaAnime = new Anime(id: indiceAnime,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceAnime, atualizaAnime);
		}
        private static void ListarAnimes()
		{
			Console.WriteLine("Listar animes");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhum anime cadastrada.");
				return;
			}

			foreach (var anime in lista)
			{
                var excluido = anime.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", anime.retornaId(), anime.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirAnime()
		{
			Console.WriteLine("Inserir novo anime");

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero))) //mostra as opções de genero
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título do Anime: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início do Anime: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição do Anime: ");
			string entradaDescricao = Console.ReadLine();

			Anime novoAnime = new Anime(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novoAnime);
		}

        private static string ObterOpcao()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Animes do seu jeito!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar animes");
			Console.WriteLine("2- Inserir novo anime");
			Console.WriteLine("3- Atualizar o anime");
			Console.WriteLine("4- Excluir o anime");
			Console.WriteLine("5- Visualizar anime");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
