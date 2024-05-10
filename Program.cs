using LKM.SAP.Conn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check.RFC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("Teste de conexão .Net com SAP");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Antes de proceder, verifique a configuração do ambiente no arquivo conf.xml");
            Console.WriteLine("");
            Console.WriteLine("Pressione uma tecla para iniciar a verificação");
            var key = Console.ReadKey();

            //Validação
            Check();

            Console.WriteLine("");
            Console.WriteLine("Processo finalizado");
            key = Console.ReadKey();
        }

        private static void Check()
        {
            //Controle
            string strSubrc = string.Empty;
            string strErro = string.Empty;
            string strRet = string.Empty;

            //SAP
            SAPRfc rfcSap = new SAPRfc();

            Console.WriteLine("");
            Console.WriteLine("Teste de conexão com ambiente SAP ECC");
            rfcSap.sapExec("ambecc", out strSubrc, out strErro, out strRet);
            Console.WriteLine(string.Format("Retorno: {0}", strSubrc));
            Console.WriteLine(string.Format("Erro (se ocorrer): {0}", strErro));

            Console.WriteLine("");
            Console.WriteLine("Teste de conexão com ambiente SAP S/4");
            rfcSap.sapExec("ambs-4", out strSubrc, out strErro, out strRet);
            Console.WriteLine(string.Format("Retorno: {0}", strSubrc));
            Console.WriteLine(string.Format("Erro (se ocorrer): {0}", strErro));
        }
    }
}
