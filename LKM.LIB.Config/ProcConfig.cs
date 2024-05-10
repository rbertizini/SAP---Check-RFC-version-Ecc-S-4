using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LKM.LIB.Config
{
    public class ProcConfig
    {
        /// <summary>
        /// Obter configuração sem referência
        /// </summary>
        /// <param name="nomeConfig">Nome da configuração</param>
        /// <returns>Valor da configuração</returns>
        public string ObterConfig(string nomeConfig)
        {
            //Variáveis loais
            string infoConfig = string.Empty;

            //Carregando XML
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conf.xml")))
            {
                //Obtendo parametrização
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conf.xml"));
                XmlNodeList xmlElem = xmlDoc.GetElementsByTagName(nomeConfig);
                if (xmlElem.Count > 0)
                {
                    infoConfig = xmlElem[0].InnerXml;
                }
                else
                {
                    infoConfig = "";
                }

                //Liberando objeto
                xmlDoc = null;
            }

            //Rerotno
            return infoConfig;
        }

        /// <summary>
        /// Obter configuração com referência
        /// </summary>
        /// <param name="nomeConfig">Nome da configuração</param>
        /// <param name="nomeRef">Referência</param>
        /// <returns>Valor da configuração</returns>
        public string ObterConfigRef(string nomeConfig, string nomeRef)
        {
            //Variáveis loais
            string infoConfig = string.Empty;

            //Carregando XML
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conf.xml")))
            {
                //Obtendo parametrização
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conf.xml"));
                XmlNodeList xmlElem = xmlDoc.GetElementsByTagName(nomeConfig);

                for (int i = 0; i < xmlElem.Count; i++)
                {
                    if (xmlElem[i].ParentNode.Attributes[0].Value == nomeRef)
                    {
                        infoConfig = xmlElem[i].InnerXml;
                        return infoConfig;
                    }
                }

                //Liberando objeto
                xmlDoc = null;
            }

            //Rerotno
            return infoConfig;
        }
    }
}
