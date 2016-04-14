using Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;


namespace Decktra.PubliPuntoEstacion.KioskoServicesModule
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

        private string _apipwd = String.Format("{0}:{1}", "francisco", "cocuyH)$hfy63(");
        private string _webSyncServerAddress;
        private string _videosPath;

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
            RetrieveRamosComerciales().ContinueWith((t1) =>
            {
                UpdateRamosComerciales(t1.Result);
                RetrieveEntesComerciales().ContinueWith((t2) =>
                {
                    UpdateEntesComerciales(t2.Result);
                    RetrieveKioskoPromociones().ContinueWith((t3) =>
                    {
                        if (t3.Result == null) return;

                        List<Task<Kiosko_Promocion_Detalle>> listOfTaskKioskoPromocionDetalle = new List<Task<Kiosko_Promocion_Detalle>>();
                        foreach (var item in t3.Result.Kiosko_Promociones)
                        {
                            listOfTaskKioskoPromocionDetalle.Add(RetrieveKiosko_Promocion_Detalle(item.Codigo));
                        }
                        Task.WhenAll(listOfTaskKioskoPromocionDetalle).ContinueWith((t4) =>
                        {
                            foreach (var itemTask in listOfTaskKioskoPromocionDetalle)
                            {
                                UpdatePromocionComercial(itemTask.Result);
                            }
                            Logger.Log(string.Format("Finalizando Agente de Sincronizacion (Id:{0})", _idSync), Category.Info, Priority.Low);
                        }, TaskContinuationOptions.NotOnFaulted);
                    }, TaskContinuationOptions.NotOnFaulted);
                }, TaskContinuationOptions.NotOnFaulted);
            }, TaskContinuationOptions.NotOnFaulted);
        }

        private async Task<ListOfUsuario> RetrieveUsuarios()
        {
            Logger.Log(string.Format("Descargando Usuarios (Id:{0})", _idSync), Category.Info, Priority.Low);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_webSyncServerAddress);
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
                client.BaseAddress = new Uri(_webSyncServerAddress);
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

        private async Task<ListOfEnteComercialDTO> RetrieveEntesComerciales()
        {
            Logger.Log(string.Format("Descargando Entes Comerciales (Id:{0})", _idSync), Category.Info, Priority.Low);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_webSyncServerAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/entes_comerciales.json");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsAsync<Object>();
                        ListOfEnteComercialDTO listOfValues = Newtonsoft.Json.JsonConvert.DeserializeObject<ListOfEnteComercialDTO>(content.ToString());
                        Logger.Log(string.Format("Entes Comerciales descargados con exito (Id:{0})", _idSync), Category.Info, Priority.Low);
                        return listOfValues;
                    }

                    return null;
                }
                catch (HttpRequestException e)
                {
                    Logger.Log(string.Format("Error descargando Entes Comerciales (Id:{0}): {1}", _idSync, e.InnerException.Message), Category.Info, Priority.Low);
                    return null;
                }
            }
        }

        private async Task<ListOfKiosko_PromocionesDTO> RetrieveKioskoPromociones()
        {
            Logger.Log(string.Format("Descargando Kiosko Promociones (Id:{0})", _idSync), Category.Info, Priority.Low);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_webSyncServerAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_apipwd)));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/kiosko_promociones.json");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsAsync<Object>();
                        ListOfKiosko_PromocionesDTO listOfValues = Newtonsoft.Json.JsonConvert.DeserializeObject<ListOfKiosko_PromocionesDTO>(content.ToString());
                        Logger.Log(string.Format("Kiosko Promociones descargados con exito (Id:{0})", _idSync), Category.Info, Priority.Low);
                        return listOfValues;
                    }

                    return null;
                }
                catch (HttpRequestException e)
                {
                    Logger.Log(string.Format("Error descargando Kiosko Promociones (Id:{0}): {1}", _idSync, e.InnerException.Message), Category.Info, Priority.Low);
                    return null;
                }
            }
        }

        private async Task<Kiosko_Promocion_Detalle> RetrieveKiosko_Promocion_Detalle(string codigoPromocion)
        {
            Logger.Log(string.Format("Descargando Kiosko Promociones Detalle (Id:{0})(CodigoPromocion:{1})", _idSync, codigoPromocion), Category.Info, Priority.Low);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_webSyncServerAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_apipwd)));
                try
                {
                    HttpResponseMessage response = await client.GetAsync(string.Format("api/kiosko_promociones/{0}.json", codigoPromocion));
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsAsync<Object>();
                        Kiosko_Promocion_Detalle promocionDetalleWeb = Newtonsoft.Json.JsonConvert.DeserializeObject<Kiosko_Promocion_Detalle>(content.ToString());
                        Logger.Log(string.Format("Kiosko Promocion Detalle (Id:{0})(CodigoPromocion:{1}) descargado con exito.", _idSync, codigoPromocion), Category.Info, Priority.Low);
                        return promocionDetalleWeb;
                    }

                    return null;
                }
                catch (HttpRequestException e)
                {
                    Logger.Log(string.Format("Error descargando Kiosko Promocion Detalle (Id:{0})(CodigoPromocion{1}): {2}", _idSync, codigoPromocion, e.InnerException.Message), Category.Info, Priority.Low);
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
                if (String.IsNullOrEmpty(item.ImagenURL.image.url)) continue;
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

        private void UpdateEntesComerciales(ListOfEnteComercialDTO listOfEntesComerciales)
        {
            if (listOfEntesComerciales == null) return;

            //Repositorios
            using (var repository = new EnteComercialRepository())
            {
                foreach (var item in listOfEntesComerciales.Entes_Comerciales)
                {
                    repository.AddOrUpdate(item);
                }
            }

            //Imagenes
            foreach (var item in listOfEntesComerciales.Entes_Comerciales)
            {
                Uri webpath;

                //Imagen
                if (!String.IsNullOrEmpty(item.ImagenURL.image.url))
                {
                    webpath = new Uri("file://localhost" + item.ImagenURL.image.url);
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

                //Logo
                if (!String.IsNullOrEmpty(item.LogoURL.logo.url))
                {
                    webpath = new Uri("file://localhost" + item.LogoURL.logo.url);
                    if (webpath.IsFile)
                    {
                        string filename = System.IO.Path.GetFileName(webpath.LocalPath);
                        string inputfilepath = AppDomain.CurrentDomain.BaseDirectory + "media\\" + filename;
                        if (!System.IO.File.Exists(inputfilepath))
                        {
                            //descargo                    
                            DownloadFileFTP(item.LogoURL.logo.url, inputfilepath);
                        }
                    }
                }

                //Video
                if (!String.IsNullOrEmpty(item.VideoUrl))
                {
                    var path = _videosPath + "\\" + item.VideoUrl;
                    if (System.IO.File.Exists(path))
                    {
                        string inputfilepath = AppDomain.CurrentDomain.BaseDirectory + "videos\\" + item.VideoUrl;
                        if (!System.IO.File.Exists(inputfilepath))
                        {
                            System.IO.File.Copy(path, inputfilepath);
                        }
                    }
                }
            }
        }

        private void UpdatePromocionComercial(Kiosko_Promocion_Detalle promocion)
        {
            using (var repository = new EnteComercialRepository())
            {
                repository.AddOrUpdatePromocion(promocion);
            }

            Uri webpath;

            //Imagen Small
            if (!String.IsNullOrEmpty(promocion.ImagenSmallUrl.card.url))
            {
                webpath = new Uri("file://localhost" + promocion.ImagenSmallUrl.card.url);
                if (webpath.IsFile)
                {
                    string filename = System.IO.Path.GetFileName(webpath.LocalPath);
                    string inputfilepath = AppDomain.CurrentDomain.BaseDirectory + "media\\" + filename;
                    if (!System.IO.File.Exists(inputfilepath))
                    {
                        //descargo                    
                        DownloadFileFTP(promocion.ImagenSmallUrl.card.url, inputfilepath);
                    }
                }
            }

            //Imagen
            if (!String.IsNullOrEmpty(promocion.ImagenUrl.banner.url))
            {
                webpath = new Uri("file://localhost" + promocion.ImagenUrl.banner.url);
                if (webpath.IsFile)
                {
                    string filename = System.IO.Path.GetFileName(webpath.LocalPath);
                    string inputfilepath = AppDomain.CurrentDomain.BaseDirectory + "media\\" + filename;
                    if (!System.IO.File.Exists(inputfilepath))
                    {
                        //descargo                    
                        DownloadFileFTP(promocion.ImagenUrl.banner.url, inputfilepath);
                    }
                }
            }
        }

        private void DownloadFileFTP(string ftpfilepath, string inputfilepath)
        {
            string ftpfullpath = _webSyncServerAddress + ftpfilepath;
            if (CheckFileExists(ftpfullpath))
            {
                Logger.Log(string.Format("Descargando Recurso de ftp (Id:{0}) {1}", _idSync, ftpfullpath), Category.Info, Priority.Low);
                using (var downloader = new WebClient())
                {
                    Uri newUri = new Uri(ftpfullpath);
                    downloader.DownloadFileCompleted += Downloader_DownloadFileCompleted;
                    downloader.DownloadFileAsync(newUri, inputfilepath);
                }
            }
        }

        private void Downloader_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //Logger.Log(string.Format("Recurso descargado de ftp (Id:{0}) {1}", _idSync, sender.ToString()), Category.Info, Priority.Low);
        }

        private bool CheckFileExists(string uriString)
        {
            var request = (HttpWebRequest)WebRequest.Create(uriString);
            try
            {
                request.Credentials = new NetworkCredential("", "");
                request.Method = WebRequestMethods.Http.Head;
                request.GetResponse();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                request.Abort();
            }
        }
    }
}
