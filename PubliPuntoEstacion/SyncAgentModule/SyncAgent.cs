﻿using Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using System;
using System.Net;
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

            RetrieveUsuarios().ContinueWith((t) => UpdateUsuarios(t.Result));
            RetrieveRamosComerciales().ContinueWith((t) => UpdateRamosComerciales(t.Result));
            //RetrieveEntesComerciales();
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

        private async Task<ListOfRamoComercialDTO> RetrieveRamosComerciales()
        {
            Logger.Log(string.Format("Descargando Ramos Comerciales (Id:{0})", _idSync), Category.Info, Priority.Low);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://cuponexpress.com.ve/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/ramos_comerciales.json");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsAsync<Object>();
                        ListOfRamoComercialDTO listOfValues = Newtonsoft.Json.JsonConvert.DeserializeObject<ListOfRamoComercialDTO>(content.ToString());
                        Logger.Log(string.Format("Ramos Comerciales descargados con exito (Id:{0})", _idSync), Category.Info, Priority.Low);
                        return listOfValues;
                    }

                    return null;
                }
                catch (HttpRequestException e)
                {
                    Logger.Log(string.Format("Error descargando Ramos Comerciales (Id:{0}): {1}", _idSync, e.InnerException.Message), Category.Info, Priority.Low);
                    return null;
                }
            }
        }

        private async Task<ListOfRamoComercialDTO> RetrieveEntesComerciales()
        {
            Logger.Log(string.Format("Descargando Entes Comerciales (Id:{0})", _idSync), Category.Info, Priority.Low);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://cuponexpress.com.ve/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/ramos_comerciales.json");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsAsync<Object>();
                        ListOfRamoComercialDTO listOfValues = Newtonsoft.Json.JsonConvert.DeserializeObject<ListOfRamoComercialDTO>(content.ToString());
                        Logger.Log(string.Format("Ramos Comerciales descargados con exito (Id:{0})", _idSync), Category.Info, Priority.Low);
                        return listOfValues;
                    }

                    return null;
                }
                catch (HttpRequestException e)
                {
                    Logger.Log(string.Format("Error descargando Ramos Comerciales (Id:{0}): {1}", _idSync, e.InnerException.Message), Category.Info, Priority.Low);
                    return null;
                }
            }
        }

        private void UpdateUsuarios(ListOfUsuario listOfUsuarios)
        {
            if (listOfUsuarios == null) return;

            using (var repository = new UsuariosRepository())
            {
                foreach (var item in listOfUsuarios.Usuarios)
                {
                    repository.AddOrUpdate(item);
                }
            }
        }

        private void UpdateRamosComerciales(ListOfRamoComercialDTO listOfRamosComerciales)
        {
            if (listOfRamosComerciales == null) return;

            //Repositorios
            using (var repository = new RamoComercialRepository())
            {
                foreach (var item in listOfRamosComerciales.Ramos_Comerciales)
                {
                    repository.AddOrUpdate(item);
                }
            }

            //Imagenes
            foreach (var item in listOfRamosComerciales.Ramos_Comerciales)
            {
                Uri webpath = new Uri("file://localhost" + item.ImagenURL.image.url);
                if (webpath.IsFile)
                {
                    string filename = System.IO.Path.GetFileName(webpath.LocalPath);
                    string inputfilepath = AppDomain.CurrentDomain.BaseDirectory + "media\\" + filename;
                    if (!System.IO.File.Exists(inputfilepath))
                    {
                        //descargo                    
                        DownloadFileFTP(item.ImagenURL.image.url, inputfilepath);
                    }
                }
            }
        }

        private void DownloadFileFTP(string ftpfilepath, string inputfilepath)
        {
            string ftpfullpath = Properties.Settings.Default.WebAddress + ftpfilepath;
            Uri newUri = new Uri(ftpfullpath);
            WebClient downloader = new WebClient();
            downloader.DownloadFileAsync(newUri, inputfilepath);

            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpfullpath);
            //request.Method = WebRequestMethods.Ftp.DownloadFile;

            //// This example assumes the FTP site uses anonymous logon.
            //request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //Stream responseStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            //Console.WriteLine("Download Complete, status {0}", response.StatusDescription);

            //reader.Close();
            //response.Close();
        }
    }
}