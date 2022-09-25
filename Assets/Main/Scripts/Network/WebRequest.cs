using Newtonsoft.Json;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Metaverse.Network
{
    public class WebRequest
    {
        /* ============================================================
         * EVENTS
         * ============================================================*/
        public Action<string> OnResponseSuccess { get; set; }
        public Action<string> OnResponseError { get; set; }


        /* ============================================================
         * READONLY FIELDS
         * ============================================================*/
        private readonly string URL;
        private readonly string METHOD;


        /* ============================================================
         * CONSTRUCTORS
         * ============================================================*/
        public WebRequest(string url, HTTPMethod method)
        {
            URL = url;
            METHOD = method.ToString();
        }


        /* ============================================================
         * PUBLIC FUNCTIONS
         * ============================================================*/
        public void Send(object json = null)
        {
            NewRequest(json).completed += OnResponse;
        }


        /* ============================================================
         * PRIVATE FUNCTIONS
         * ============================================================*/
        private void OnResponse(AsyncOperation op)
        {
            if (op is UnityWebRequestAsyncOperation operation)
            {
                UnityWebRequest request = operation.webRequest;
                string response = request.downloadHandler.text;
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    long code = request.responseCode;
                    OnResponseError?.Invoke(code.ToString());
                    return;
                }
                OnResponseSuccess?.Invoke(response);
            }
        }

        private UnityWebRequestAsyncOperation NewRequest(object json = null)
        {
            UnityWebRequest webRequest = new UnityWebRequest(URL, METHOD);
            webRequest.timeout = 30;

            if (json != null)
            {
                webRequest.SetRequestHeader("Content-Type", "application/json");
                string text = JsonConvert.SerializeObject(json);
                byte[] bytes = new UTF8Encoding().GetBytes(text);
                webRequest.uploadHandler = new UploadHandlerRaw(bytes);
            }

            webRequest.downloadHandler = new DownloadHandlerBuffer();
            return webRequest.SendWebRequest();
        }
    }
}
