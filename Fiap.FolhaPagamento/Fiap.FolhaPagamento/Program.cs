namespace Fiap.FolhaPagamento
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public class INSSFaixa
    {
        public decimal Piso { get; set; }
        public decimal Teto { get; set; }
        public decimal Aliquota { get; set; }

        public bool ContemValor(decimal valor)
        {
            if (valor > Teto)
                return true;

            return valor >= Piso;
            
        }

        public decimal ObterValorFaixa(decimal salario)
        {
            var valorFaixaAnterior = Aliquota > 7.5m ? Piso - 0.01m : Piso;

            if (salario > Teto)
                return Teto - valorFaixaAnterior;
            return salario - valorFaixaAnterior;
            //return salario > Teto ?
            //      Teto - valorFaixaAnterior :
            //      salario - valorFaixaAnterior;
        }
    }

    public class Inss
    {
        public Inss()
        {
            Faixas = new List<INSSFaixa>
            {
                new INSSFaixa {Piso = 0m, Teto = 1412m, Aliquota = 7.5m},
                new INSSFaixa {Piso = 1412.01m, Teto = 2666.68m, Aliquota = 9m},
                new INSSFaixa {Piso = 2666.69m, Teto = 4000.03m, Aliquota = 12m},
                new INSSFaixa {Piso = 4000.04m, Teto = 7786.02m, Aliquota = 14m}
            };
        }
        public List<INSSFaixa> Faixas { get; set; }

        //Calcular desconto progressivo por faixa

        public decimal CalcularDesconto(decimal salarioBruto)
        {
            decimal desconto = 0m;

            foreach (var item in Faixas)
            {
                //if(salarioBruto >= item.Piso && salarioBruto <= item.Teto)
                if(item.ContemValor(salarioBruto))
                {
                    //Somar os descontos de cada faixa
                    desconto = salarioBruto * item.Aliquota / 100;
                    return desconto;
                }
            }
            return 0;
        }
    }
}
