using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ToshyroApp
{
    internal class Maquina
    {
        private List<Decimal> moedasInseridas;
        private Decimal trocoPendente;
        private Dictionary<Decimal, int> moedasDaMaquina;
        private Dictionary<String, (Decimal preco, int quantidade)> produtosDaMaquina;

        private readonly Decimal[] moedasValidas = { 0.01m, 0.05m, 0.10m, 0.25m, 0.50m, 1.00m };

        public Maquina()
        {
            moedasInseridas = new List<Decimal>();
            trocoPendente = 0.0m;

            moedasDaMaquina = new Dictionary<Decimal, int>
            {
                { 0.01m, 10 }, { 0.05m, 10 }, { 0.10m, 10 },
                { 0.25m, 10 }, { 0.50m, 10 }, { 1.00m, 10 }
            };

            produtosDaMaquina = new Dictionary<string, (Decimal preco, int quantidade)>
            {
                { "Coca-Cola",  ( 1.50m, 10 ) },
                { "Água",       ( 1.00m, 10 ) },
                { "Pastelina",  ( 0.30m, 10 ) },
            };
        }

        public void inserirMoedas(Decimal moeda)
        {
            if (moedasValidas.Contains(moeda))
            {
                moedasInseridas.Add(moeda);
                trocoPendente = trocoPendente + moeda; // Nao usei o += por questao de legibilidade...
            }
        }

        public string efetuarCompra(string nomeDoProduto)
        {
            if (!produtosDaMaquina.ContainsKey(nomeDoProduto)
                || produtosDaMaquina[nomeDoProduto].quantidade <= 0
                || trocoPendente < produtosDaMaquina[nomeDoProduto].preco)
            {
                return "NO_PRODUCT";
            }

            var (preco, quantidade) = produtosDaMaquina[nomeDoProduto];
            Decimal trocoTotal = trocoPendente - preco;

            produtosDaMaquina[nomeDoProduto] = (preco, quantidade - 1);

            foreach (var moeda in moedasInseridas)
            {
                moedasDaMaquina[moeda]++;
            }
            moedasInseridas.Clear();
            trocoPendente = trocoTotal;

            return $"{nomeDoProduto} = {formatadorDeMoeda(trocoTotal)}";
        }

        public string obterTroco()
        {
            if (trocoPendente == 0) return "NO_CHANGE";

            List<Decimal> moedasDoTroco = calcularTroco(trocoPendente);

            if (moedasDoTroco == null) return "NO_COIMS";

            trocoPendente = 0;

            return string.Join(" ", moedasDoTroco.Select(formatadorDeMoeda));
        }

        private List<Decimal> calcularTroco(Decimal quantia)
        {
            List<Decimal> troco = new List<Decimal>();
            Decimal restante = Math.Round(quantia, 2);

            var moedasOrdenadas = moedasDaMaquina.Keys.OrderByDescending(x => x).ToList();

            foreach (var moeda in moedasOrdenadas)
            {
                while (restante >= moeda && moedasDaMaquina[moeda] > 0)
                {
                    troco.Add(moeda);
                    moedasDaMaquina[moeda]--;
                    restante -= moeda;
                    restante = Math.Round(restante, 2);
                }
            }

            if (restante > 0)
            {
                foreach (var moeda in troco)
                {
                    moedasDaMaquina[moeda]++;
                }
                return null;
            }
            return troco;
        }

        // Formatador de moeda, ja que no BR usamos "," como separador decimal, essa funcao auxiliar faz isso.
        private string formatadorDeMoeda(Decimal valor)
        {
            return valor.ToString("0.00", CultureInfo.InvariantCulture).Replace(".", ",");
        }

        // Faz o mesmo que o formatadorDeMoedas, mas para entrada do usuario.
        public static bool formatadorDeEntrada (string entrada, out decimal resultado)
        {
            string normalizador = entrada.Replace(',', '.');
            return decimal.TryParse(normalizador, NumberStyles.Any, CultureInfo.InvariantCulture, out resultado);
        }
    }
}