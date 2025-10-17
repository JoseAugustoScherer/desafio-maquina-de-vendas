using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshyroApp
{
    internal class Maquina
    {
        private List<Double> moedasInseridas;

        private Double trocoPendente;

        private Dictionary<Double, int> moedasDaMaquina;
        private Dictionary<String, (Double preco, int quantidade)> produtosDaMaquina;
        
        private readonly Double[] moedasValidas = { 0.01, 0.05, 0.10, 0.25, 0.50, 1.00 };

        public Maquina()
        {
            moedasInseridas = new List<Double>();

            trocoPendente = 0.0;

            moedasDaMaquina = new Dictionary<Double, int>
            {
                { 0.01, 10 }, { 0.05, 10 }, { 0.10, 10 },
                { 0.25, 10 }, { 0.50, 10 }, { 1.00, 10 }
            };

            produtosDaMaquina = new Dictionary<string, (double preco, int quantidade)>
            {
                { "Coca-cola",  ( 1.50, 10 ) },
                { "Água",       ( 1.00, 10 ) },
                { "Pastelina",  ( 0.30, 10 ) },
            };
        }
    }
}
