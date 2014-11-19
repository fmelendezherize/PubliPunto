using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;


namespace Decktra.PubliPuntoEstacion.SyncAgentModule
{
    public class StateObjClass
    {
        // Used to hold parameters for calls to TimerTask. 
        public int SomeValue;
        public System.Threading.Timer TimerReference;
        public bool TimerCanceled;
    }

    public class SyncAgent
    {
        [Dependency]
        public ILoggerFacade Logger { get; set; }

        private Timer TimerItem;

        private int _idSync = 0;

        public SyncAgent()
        {
        }

        public void StartService()
        {
            StateObjClass stateObj = new StateObjClass();
            TimerCallback timerDelegate = new TimerCallback(StartWebConnection);
            TimerItem = new Timer(timerDelegate, stateObj, 10000, 300000);
        }

        private void StartWebConnection(object arg)
        {
            _idSync++;
            Logger.Log(string.Format("Comenzando Agente de Sincronizacion (Id:{0})", _idSync), Category.Info, Priority.Low);
            //Thread.Sleep(5000);
            RetrieveUsuarios();
        }

        private async Task<ListOfUsuario> RetrieveUsuarios()
        {
            Logger.Log(string.Format("Descargando Usuarios (Id:{0})", _idSync), Category.Info, Priority.Low);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://cuponexpress.com.ve/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/usuarios.json");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsAsync<Object>();
                        ListOfUsuario listOfValues = Newtonsoft.Json.JsonConvert.DeserializeObject<ListOfUsuario>(content.ToString());
                        Logger.Log(string.Format("Usuarios descargados con exito (Id:{0})", _idSync), Category.Info, Priority.Low);
                        return listOfValues;
                    }

                    return null;
                }
                catch (HttpRequestException e)
                {
                    Logger.Log(string.Format("Error descargando Usuarios (Id:{0}): {1}", _idSync, e.InnerException.Message), Category.Info, Priority.Low);
                    return null;
                }
            }
        }
    }
}
