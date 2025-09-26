using PrimeiroPrograma;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Hello World!");

            string nome = "Heverton Souza";
            string idade = "19";
            double peso = 55.78;

            Console.WriteLine(idade);
            Console.WriteLine(peso);
            Console.WriteLine(peso.ToString("F2"));
            Console.WriteLine(peso.ToString("F2", CultureInfo.InvariantCulture)); // xibe o número com ponto "." de separador

            //Console.WriteLine($"{nome} tem {idade} anos e pesa {peso:F2} kg");*/

            /*string cor1 = Console.ReadLine();
            string cor2 = Console.ReadLine();
            string cor3 = Console.ReadLine();

            string[] vetor = Console.ReadLine().Split(' ');

            string a = vetor [0];
            string b = vetor [1];
            string c = vetor [2];*/


            //exercicio01
            /*Console.WriteLine(" Escreva seu  primeiro nome");
            string nome = Console.ReadLine();

            Console.WriteLine("Escreva seu sobrenome");
            string sobrenome = Console.ReadLine();

            Console.WriteLine("Digite sua idade:");
            int idade = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o preço de um produto:");
            double valorProduto = double.Parse(Console.ReadLine());

            Console.WriteLine("Digite seu sobrenome, idade e altura na mesma linha");

            string[] vetor = Console.ReadLine().Split(' ');

            string sn = vetor[0];
            int i = int.Parse(vetor[1]);
            double a = double.Parse(vetor[2]);


            Console.WriteLine($"Olá!, {nome} {sobrenome}");

            if (idade >= 18)
            {
                Console.WriteLine($"Você tem {idade} anos, já é maior de idade");
            }
            else
            {
                Console.WriteLine($"Você tem {idade} anos, jainda é menor de idade");
            }

            Console.WriteLine($"O valor do produto digitado é {valorProduto}");
            Console.WriteLine(sn);
            Console.WriteLine(i);
            Console.WriteLine(a);

            Console.WriteLine("Escreva seu  primeiro nome:");*/

            //else if e while
            /*while (true)
            {
                Console.WriteLine("Digite o número horário atual ex: 10 ou digite 0 para finalizar");

                

                int hora = int.Parse(Console.ReadLine());

                if (hora == 0)
                {
                    Console.WriteLine("Programa finalizado!");
                    break;
                }
                else if (hora >= 6 && hora <= 12)
                {
                    Console.WriteLine("Olá bom dia!");
                }
                else if (hora > 12 && hora <= 18)
                {
                    Console.WriteLine("Boa tarde");
                }
                else
                {
                    {
                        Console.WriteLine("Boa noite!");
                    }
                }
            }*/

            //função 
            /*Console.WriteLine("Digite três números");
            int n1 = int.Parse(Console.ReadLine());
            int n2 = int.Parse(Console.ReadLine());
            int n3 = int.Parse(Console.ReadLine());

            double resultado = Maior(n1, n2, n3);

            Console.WriteLine("Número maior: " + resultado);

            static int Maior(int a, int b, int c)
            {
                int m;
                if (a > b && a > c)
                {
                    m = a;
                }
                else if (b > c)
                {
                    m = b;
                }
                else
                {
                    m = c;
                }
                return m;
            }*/

            //laço for 

            /*
            Console.WriteLine("Quantos números você vai digitar?");
            int n = int.Parse(Console.ReadLine());
            int soma = 0;     

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"Digite o {i}° número");
                int valor = int.Parse(Console.ReadLine());
                soma += valor;
            }

            Console.WriteLine(soma);*/

            //classes e Métodos

            /*Aprendiz h, r, g;

            h = new Aprendiz();
            r = new Aprendiz();
            g = new Aprendiz();

            
            h.Nome = Console.ReadLine();
            h.Funcao = Console.ReadLine();
            h.Empresa = Console.ReadLine();

            Console.WriteLine("Bem vindo à " + h.Empresa + " aprendiz: " + h.Nome);*/

            /*List<Aprendiz> aprendizes = new List<Aprendiz>();

            Console.Write("Quantidade de aprendizes chegaram na equipe: ");
            int quantidade = int.Parse(Console.ReadLine());

            Aprendiz aprendiz = new Aprendiz();

            for (int i = 1; i <= quantidade; i++)
            {
                Console.WriteLine("Digite as informações do " + i + "° aprendiz: ");

                Console.Write("Nome: ");
                aprendiz.Nome = Console.ReadLine();

                Console.Write("Função: ");
                aprendiz.Funcao = Console.ReadLine();

                Console.Write("Empresa: ");
                aprendiz.Empresa = Console.ReadLine();

                aprendizes.Add(aprendiz);

                
            }

            Console.WriteLine("Cadastro finalizado.");
            foreach (var informacao in aprendizes)
            {
                Console.WriteLine("Dados do aprendiz: " + aprendiz);
            }*/


            //Classe Produto com métodos 

            /*Produto produto = new Produto();

            Console.WriteLine("Entre com os dados do produto: ");
            Console.Write("Descrição: ");
            produto.Descrição = Console.ReadLine();
            Console.Write("Preço: ");
            produto.Preco = double.Parse(Console.ReadLine());
            Console.Write("Quantidade no estoque: ");
            produto.Quantidade = int.Parse(Console.ReadLine());

            Console.WriteLine("Dados do produto: " + produto);

            Console.WriteLine();
            Console.Write("Digite o número de produtos que deseja adicionar: ");
            int quantidade = int.Parse(Console.ReadLine());
            produto.AdicionarProduto(quantidade);

            Console.WriteLine();
            Console.WriteLine("Dados atualizados: " + produto);

            Console.WriteLine();
            Console.Write("Digite o número de produtos que deseja remover: ");
            quantidade = int.Parse(Console.ReadLine());
            produto.RemoverProduto(quantidade);

            Console.WriteLine();
            Console.WriteLine("Dados atualizados: " + produto);*/

            //Conversor de moedas 

            /*ConversorDeMoeda conversor = new ConversorDeMoeda();

            Console.Write("Insira a cotação do dólar: ");
            conversor.ValorDolar = double.Parse(Console.ReadLine());

            Console.Write("Insira a quantidade de dólares que deseja comprar: ");
            double quantidade = double.Parse(Console.ReadLine());
            conversor.Calculo(quantidade);

            Console.Write("Valor a ser pago: " + conversor.ValorFinal.ToString("F2"));*/

            //Encapsulamento na classe produto 
            /*
            Produto p = new Produto("TV", 500.00, 10);
            Console.WriteLine(p.Descricao);
            Console.WriteLine(p);

            p.Descricao = "TV 4K";

            Console.WriteLine(p.Descricao);
            Console.WriteLine(p);*/

            // Listas
           /*
            List<string> list = new List<string>();

            list.Add("Heverton Souza");
            list.Add("Inpulsionando o futuro!");
            list.Insert(2, "Unidos pela inovação");
            list.Insert(1, "#SomosWS");
            list.Add("#Supplyy");

            foreach (var nome in list) { 
            Console.WriteLine(nome);
            }

            Console.WriteLine("List count: " + list.Count);

            // Consulta na lista com Lambda

            // Primeira ocorrencia
            string primeiro = list.Find(x => x[1] == 'S');
            Console.WriteLine(primeiro);

            // Ultima ocorrencia 
            string ultimo = list.FindLast(x => x[1] == 'S');
            Console.WriteLine(ultimo);

            // Primeira posição com index
            int pos1 = list.FindIndex(x => x[1] == 'S');
            Console.WriteLine(pos1);

            // Ultima posição com index
            int pos2 = list.FindLastIndex(x => x[1] == 'S');
            Console.WriteLine(pos2);

            // Filtro de lista
            List<string> list2 = list.FindAll(x => x.Length == 8);
            Console.WriteLine("----------------------------");
            foreach (string obj in list2) { 
            Console.WriteLine(obj); 
            }

            Console.WriteLine("----------------------------");

            // Remover algo da lista
            list.Remove("Heverton Souza");
            foreach (string obj in list2)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine("----------------------------");
            list.Add("Dev Tom");

            // Remover tudo que seja igual a condição
            list.RemoveAll(x => x[0] == '#');
            foreach (string obj in list)
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine("----------------------------");

            // Remover algo de acordo com a posição 
            list.RemoveAt(2);
            foreach (string obj in list)
            {
                Console.WriteLine(obj);
            }*/

            //DateTime

            DateTime hora = DateTime.Now;
            Console.WriteLine(hora);



        }
    }
}
