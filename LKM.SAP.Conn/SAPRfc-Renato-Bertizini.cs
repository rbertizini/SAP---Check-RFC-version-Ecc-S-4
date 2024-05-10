using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKM.LIB.Config;
using SAP.Middleware.Connector;

namespace LKM.SAP.Conn
{
    public class SAPRfc
    {
        //Variáveis globais
        SAPSystemConnect sapConn = new SAPSystemConnect();

        public void sapExec(string strAmb, out string strSubrc, out string strErro, out string strLic)
        {
            //Configuração
            ProcConfig procConf = new ProcConfig();

            //Inicializando
            strSubrc = string.Empty;
            strErro = string.Empty;
            strLic = string.Empty;

            //Instância de conexão
            try
            {
                RfcDestinationManager.RegisterDestinationConfiguration(this.sapConn);
                RfcDestination sapDest = RfcDestinationManager.GetDestination(strAmb);
                RfcRepository sapRepo = sapDest.Repository;
                IRfcFunction fmProc = sapRepo.CreateFunction(procConf.ObterConfig("sfunproc"));

                //fmProc.SetValue("/LKMTLIC/LIC_CHECK", "");

                //Obtendo ambiente destino
                string ambdest = procConf.ObterConfig(strAmb);

                fmProc.SetValue("PE_SYSID", ambdest.Substring(0,3));
                fmProc.SetValue("PE_PRODT", "ARDFE");

                //Realizando chamanda
                fmProc.Invoke(sapDest);

                //Obtendo retorno
                var subrc = fmProc.GetValue("PS_SUBRC");
                strSubrc = subrc.ToString();

                var tbl = fmProc.GetValue("TS_FORN");
                strLic = tbl.ToString();
            }
            catch (RfcCommunicationException e)
            {
                //Retorno de erro de comunicação
                strErro = e.ToString();
            }
            catch (RfcLogonException e)
            {
                //Retorno de erro de login
                strErro = e.ToString();
            }
            catch (RfcAbapRuntimeException e)
            {
                //Retorno de erro de exception dentro do SAP - runtime
                strErro = e.ToString();
            }
            catch (RfcAbapBaseException e)
            {
                //Retorno de erro de exception dentro do SAP - não runtime
                strErro = e.ToString();
            }
            catch (Exception e)
            {
                //Outros erros
                strErro = e.ToString();
            }
            finally
            {
                //Finalizando conexão
                RfcDestinationManager.UnregisterDestinationConfiguration(sapConn);
            }
        }
    }
}
