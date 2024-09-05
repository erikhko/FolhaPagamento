using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.FolhaPagamento.Teste
{
    public class INSSFaixaTeste
    {
        public static IEnumerable<object[]> DadosValorFaixa => new List<object[]>
            {
                new object[]{0, 14120.01m, 7.5m, 1422m, 1412m},
                new object[]{1412.01m, 2666.68m, 9m, 2100, 688}
            };

        [Fact] //Fact representa um cenário de teste
        public void INSS_Faixa_Deve_Conter_Valor()
        {
            var faixa = new INSSFaixa
            {
                Piso = 0,
                Teto = 1412
            };

            Assert.True(faixa.ContemValor(1200));
        }

        [Theory]
        [MemberData(nameof(DadosValorFaixa))]
        public void Deve_Obter_CalcularValores(decimal piso, decimal teto, decimal aliquota, decimal salario, decimal esperado)
        {
            //Arrange - configurando teste
            var faixa = new INSSFaixa
            {
                Piso = piso,
                Teto = teto,
                Aliquota = aliquota
            };

            //Act - executa teste
            var resultado = faixa.ObterValorFaixa(salario);

            //Assert - resultado esperado
            Assert.Equal(esperado, resultado);
        }
    }
}
