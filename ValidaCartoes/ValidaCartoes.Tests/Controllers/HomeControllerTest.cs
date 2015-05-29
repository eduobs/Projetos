using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidaCartoes;
using ValidaCartoes.Controllers;
using ValidaCartoes.Models;

namespace ValidaCartoes.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        #region -- Teste da Action Index --

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Teste_valor_nulo()
        {
            HomeController controller = new HomeController();
            ViewResult result = (ViewResult)controller.Index(new CartaoCredito());
            var model = (CartaoCredito)result.Model;

            Assert.AreEqual(false, model.valido);
            Assert.AreEqual(bandeira.DESCONHECIDO, model.bandeiraCartao);
        }

        [TestMethod]
        public void Teste_valor_com_espacos_valido()
        {
            HomeController controller = new HomeController();
            ViewResult result = (ViewResult)controller.Index(new CartaoCredito() { numeroCartao = " 4111111111111111 " });
            var model = (CartaoCredito)result.Model;

            Assert.AreEqual(true, model.valido);
        }

        [TestMethod]
        public void Teste_valor_com_espacos_invalido()
        {
            HomeController controller = new HomeController();
            ViewResult result = (ViewResult)controller.Index(new CartaoCredito() { numeroCartao = " 411111111111111 " });
            var model = (CartaoCredito)result.Model;

            Assert.AreNotEqual(true, model.valido);
        }

        [TestMethod]
        public void Teste_cartao_valido_visa()
        {
            HomeController controller = new HomeController();
            ViewResult result = (ViewResult)controller.Index(new CartaoCredito() { numeroCartao = "4111111111111111" });
            var model = (CartaoCredito)result.Model;

            Assert.AreEqual(true, model.valido);
            Assert.AreEqual(bandeira.VISA, model.bandeiraCartao);
        }

        [TestMethod]
        public void Teste_cartao_invalido_visa()
        {
            HomeController controller = new HomeController();
            ViewResult result = (ViewResult)controller.Index(new CartaoCredito() { numeroCartao = "411111111111111" });
            var model = (CartaoCredito)result.Model;

            Assert.AreEqual(false, model.valido);
            Assert.AreEqual(bandeira.VISA, model.bandeiraCartao);
        }

        [TestMethod]
        public void Teste_cartao_valido_master()
        {
            HomeController controller = new HomeController();
            ViewResult result = (ViewResult)controller.Index(new CartaoCredito() { numeroCartao = "5105105105105100" });
            var model = (CartaoCredito)result.Model;

            Assert.AreEqual(true, model.valido);
            Assert.AreEqual(bandeira.MASTERCARD, model.bandeiraCartao);
        }

        [TestMethod]
        public void Teste_cartao_invalido_master()
        {
            HomeController controller = new HomeController();
            ViewResult result = (ViewResult)controller.Index(new CartaoCredito() { numeroCartao = "5105105105105106 " });
            var model = (CartaoCredito)result.Model;

            Assert.AreEqual(false, model.valido);
            Assert.AreEqual(bandeira.MASTERCARD, model.bandeiraCartao);
        }

        [TestMethod]
        public void Teste_cartao_desconhecido()
        {
            HomeController controller = new HomeController();
            ViewResult result = (ViewResult)controller.Index(new CartaoCredito() { numeroCartao = "9111111111111111" });
            var model = (CartaoCredito)result.Model;

            Assert.AreEqual(bandeira.DESCONHECIDO, model.bandeiraCartao);
        }

        #endregion -- Teste da Action Index --

        #region -- Teste do método validarNumero --

        [TestMethod]
        public void Teste_Sequencia_de_numeros_zero()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("000000000000");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Teste_Sequencia_de_numeros_cinco()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("5555555555555");
            
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Teste_Validar_tamanho_com_1_caracteres()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("5");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Teste_Validar_tamanho_com_12_caracteres()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("550020201011");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Teste_Validar_tamanho_com_14_caracteres()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("12345678911234");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Teste_Validar_tamanho_com_17_caracteres()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("12345678911234567");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Teste_Validar_tamanho_com_20_caracteres()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("12345678911234567892");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Teste_Validar_somente_numeros()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("1234567891aaabbb");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Teste_Validar_caracter_vazio()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("             ");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Teste_Validar_positivo_com_13_caracteres()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("1234567891234");

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Teste_Validar_positivo_com_15_caracteres()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("123456789123456");

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Teste_Validar_positivo_com_16_caracteres()
        {
            HomeController controller = new HomeController();
            bool result = controller.validarNumero("1234567891234567");

            Assert.AreEqual(true, result);
        }

        #endregion -- Teste do método validarNumero --

        #region -- Teste do método inverteSequenciaNumeros --

        [TestMethod]
        public void Teste_Inverter_sequencia_simples()
        {
            HomeController controller = new HomeController();
            string result = controller.inverteSequenciaNumeros("arara");

            Assert.AreEqual("arara", result);
        }

        [TestMethod]
        public void Teste_Inverter_sequencia_com_erro()
        {
            HomeController controller = new HomeController();
            string result = controller.inverteSequenciaNumeros("casa");

            Assert.AreNotEqual("casa", result);
        }

        [TestMethod]
        public void Teste_Inverter_sequencia_de_numeros_na_string()
        {
            HomeController controller = new HomeController();
            string result = controller.inverteSequenciaNumeros("0011");

            Assert.AreEqual("1100", result);
        }

        #endregion -- Teste do método inverteSequenciaNumeros --

        #region -- Teste do método verificaBandeiraCartao --

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_AMEX_iniciada_com_34()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("340123456789123");

            Assert.AreEqual(bandeira.AMEX, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_AMEX_erro()
        {
            HomeController controller = new HomeController();

            bandeira result = controller.verificaBandeiraCartao("300123456789123");
            Assert.AreNotEqual(bandeira.AMEX, result);

            result = controller.verificaBandeiraCartao("601123456789123");
            Assert.AreNotEqual(bandeira.AMEX, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_AMEX_iniciada_com_37()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("370123456789123");

            Assert.AreEqual(bandeira.AMEX, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_Discover()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("601123456789123");

            Assert.AreEqual(bandeira.DISCOVER, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_Discover_com_Erro()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("600123456789123");

            Assert.AreNotEqual(bandeira.DISCOVER, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_MasterCard_iniciada_com_51()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("510123456789123");

            Assert.AreEqual(bandeira.MASTERCARD, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_MasterCard_iniciada_com_52()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("520123456789123");

            Assert.AreEqual(bandeira.MASTERCARD, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_MasterCard_iniciada_com_53()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("530123456789123");

            Assert.AreEqual(bandeira.MASTERCARD, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_MasterCard_iniciada_com_54()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("540123456789123");

            Assert.AreEqual(bandeira.MASTERCARD, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_MasterCard_iniciada_com_55()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("510123456789123");

            Assert.AreEqual(bandeira.MASTERCARD, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_MasterCard_com_Erro()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("500123456789123");

            Assert.AreNotEqual(bandeira.MASTERCARD, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_VISA()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("40000000000000");

            Assert.AreEqual(bandeira.VISA, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_VISA_com_Erro()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("500123456789123");

            Assert.AreNotEqual(bandeira.VISA, result);
        }

        [TestMethod]
        public void Teste_verifica_bandeira_cartao_Desconhecida()
        {
            HomeController controller = new HomeController();
            bandeira result = controller.verificaBandeiraCartao("9100000000000");

            Assert.AreEqual(bandeira.DESCONHECIDO, result);
        }

        #endregion -- Teste do método verificaBandeiraCartao --

        #region-- Teste do método validarNumeroCartao --

        [TestMethod]
        public void Teste_Validar_numero_cartao_valido()
        {
            HomeController controller = new HomeController();

            bool result = controller.validarNumeroMultiploDe10("4111111111111111");
            Assert.AreEqual(true, result);

            result = controller.validarNumeroMultiploDe10("4012888888881881");
            Assert.AreEqual(true, result);

            result = controller.validarNumeroMultiploDe10("378282246310005");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Teste_Validar_numero_cartao_invalido()
        {
            HomeController controller = new HomeController();

            bool result = controller.validarNumeroMultiploDe10("4111111111111");
            Assert.AreNotEqual(true, result);

            result = controller.validarNumeroMultiploDe10("5105105105105106");
            Assert.AreNotEqual(true, result);
        }

        #endregion-- Teste do método validarNumeroCartao --
    }
}
