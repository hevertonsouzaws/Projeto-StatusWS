

namespace PrimeiroPrograma
{
    public class Aprendiz
    {
        public string Nome { get; set; }
        public string Funcao { get; set; }
        public string Empresa { get; set; }

        /*
        public List<Aprendiz> CadastrarAprendizes()
        {
            List<Aprendiz> aprendizes = new List<Aprendiz>();

            Console.WriteLine("Quantidade de aprendizes que chegara na equipe: ");
            int quantidade = int.Parse(Console.ReadLine());

            for (int c = 1; c <= quantidade; c++)
            {
                Aprendiz aprendiz = new Aprendiz();

                Console.WriteLine("\nDigite as informações do " + c + "° aprendiz: ");
                Console.Write("\nNome: ");
                aprendiz.Nome = Console.ReadLine();
                Console.Write("Função: ");
                aprendiz.Funcao = Console.ReadLine();
                Console.Write("Empresa: ");
                aprendiz.Empresa = Console.ReadLine();

                aprendizes.Add(aprendiz);

                Console.WriteLine("\nCadastro Finalizado.");
            }

            return aprendizes;
        }*/


        public override string ToString()
        {
            return Nome + "\n cargo: " + Funcao + "\n empresa: " + Empresa;
        }
    }

}
