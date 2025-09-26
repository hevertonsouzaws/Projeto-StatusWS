using System;

namespace ExercicioLista
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo(a) à WS!");
            Console.WriteLine();
            Console.WriteLine("Quantos novos WS´s você quer cadastrar? ");
            int n = int.Parse(Console.ReadLine());

            List<Funcionario> list = new List<Funcionario>();

            Console.WriteLine("Entre com as informações solicitadas:");

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine("Funcionario #" + i + ":");

                Console.Write("Id: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Nome: ");
                string nome = (Console.ReadLine());

                Console.Write("Salário: ");
                double salario = double.Parse(Console.ReadLine());

                list.Add(new Funcionario(id, nome, salario));
                Console.WriteLine();
            }

            Console.WriteLine("Digite o ID do WS que você deseja aumentar o salário:");
            Console.Write("Id: ");
            int burcarId = int.Parse(Console.ReadLine());

            Funcionario ws = list.Find( x => x.Id == burcarId);
            if (ws != null)
            {
                Console.Write("Digite a porcentagem: %");
                double porcentgem = double.Parse(Console.ReadLine());
                ws.Aumento(porcentgem);
            }
            else
            {
                Console.WriteLine("erro: Não temos um WS com esse ID ainda!");
            }

            Console.WriteLine();
            Console.WriteLine("Informações atualizadas com sucesso!");
            
            foreach (Funcionario obj in list)
            {
                Console.WriteLine(obj);
            }

        }
    }
}