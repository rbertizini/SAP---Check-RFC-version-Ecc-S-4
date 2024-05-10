using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKM.LIB.Config;
using SAP.Middleware.Connector;

namespace LKM.SAP.Conn
{
    public class SAPSystemConnect : IDestinationConfiguration
    {

        /// <summary>
        /// Método de alteração
        /// </summary>
        /// <returns>Não se aplica</returns>
        public bool ChangeEventsSupported()
        {
            return false;
        }

        /// <summary>
        /// Evento de configuração
        /// </summary>
        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        /// <summary>
        /// Método de criação parâmetros de conexão
        /// </summary>
        /// <param name="destinationName"></param>
        /// <returns></returns>
        public RfcConfigParameters GetParameters(string destinationName)
        {

            //Instancia do parametro
            RfcConfigParameters parms = new RfcConfigParameters();
            ProcConfig procConf = new ProcConfig();

            //Obtendo ambiente destino
            string ambDest = procConf.ObterConfig(destinationName);
            if (string.IsNullOrEmpty(ambDest))
            {
                destinationName = destinationName.Substring(0, 6);
                ambDest = procConf.ObterConfig(destinationName);
            }

            //Definição dos parâmetros
            parms.Add(RfcConfigParameters.AppServerHost, procConf.ObterConfigRef("sapphost", ambDest));
            if (procConf.ObterConfig("sapprouter") != "")
            {
                parms.Add(RfcConfigParameters.SAPRouter, procConf.ObterConfigRef("sapprouter", ambDest));
            }
            parms.Add(RfcConfigParameters.SystemNumber, procConf.ObterConfigRef("ssystemnumber", ambDest));
            parms.Add(RfcConfigParameters.User, procConf.ObterConfigRef("suser", ambDest));
            parms.Add(RfcConfigParameters.Password, procConf.ObterConfigRef("spass", ambDest));
            parms.Add(RfcConfigParameters.Client, procConf.ObterConfigRef("sclient", ambDest));
            parms.Add(RfcConfigParameters.Language, procConf.ObterConfigRef("slanguage", ambDest));
            parms.Add(RfcConfigParameters.PoolSize, procConf.ObterConfigRef("spoolsize", ambDest));
            parms.Add(RfcConfigParameters.PeakConnectionsLimit, procConf.ObterConfigRef("speakconnection", ambDest));
            //parms.Add(RfcConfigParameters.IdleTimeout, procConf.ObterConfigRef("stimeout", ambDest));

            return parms;
        }

    }
}
