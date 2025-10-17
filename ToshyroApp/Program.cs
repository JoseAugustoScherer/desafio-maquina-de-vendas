using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshyroApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Maquina maquina = new Maquina();

            Console.WriteLine("Insira moedas e solicite um produto: ");
            string entrada = Console.ReadLine();

            // validacao basica para entrada vazia. Se true, o programa encerra.
            if (string.IsNullOrEmpty(entrada))
            {
                Console.WriteLine("Entrada vazia!");
                return;
            }

            string[] partes = entrada.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var saidas = new System.Collections.Generic.List<string>();

            foreach (string parte in partes) 
            {
                if (Maquina.formatadorDeEntrada(parte, out decimal moeda))
                {
                    maquina.inserirMoedas(moeda);
                }
                else if (parte.Equals("CHANGE", StringComparison.OrdinalIgnoreCase))
                {
                    saidas.Add(maquina.obterTroco());
                }
                else
                {
                    saidas.Add(maquina.efetuarCompra(parte));
                }
            }
            Console.WriteLine(string.Join(" ", saidas));
        }
    }
}
