using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioLista
{
    internal class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Salario { get; set; }

        public Funcionario(int id, string nome, double salario)
        {
            Id = id;
            Nome = nome;
            Salario = salario;
        }
        public void Aumento(double porcentagen)
        {
            Salario += Salario * porcentagen / 100.0;
        }

        public override string ToString()
        {
            return Id + ", " + Nome + ", " + Salario.ToString("F2");
        }
    }
}


