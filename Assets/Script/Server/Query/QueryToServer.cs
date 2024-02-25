using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Script.Model.Server.Response;
using UnityEngine;
using UnityEngine.Networking;

namespace Script.Server.Query
{
    public class QueryToServer: MonoBehaviour
    {
        public IEnumerator Post(Dictionary<string, object> body, [CanBeNull] Dictionary<string, object> header,
            string route, Action<ResultResponse> callback)
        {
            var bodyParams = JsonConvert.SerializeObject(body, Formatting.Indented);

            var request = new UnityWebRequest(Configs.Configuration.BaseUrl + route, "POST");

            var jsonToSend = new System.Text.UTF8Encoding().GetBytes(bodyParams);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Accept", "application/json");
            request.uploadHandler.contentType = "application/json";
            
            if (header != null)
            {
                if (header!.Count > 0)
                {
                    foreach (var rowHeader in header)
                        request.SetRequestHeader(rowHeader.Key, rowHeader.Value.ToString());
                }
            }
            
            yield return request.SendWebRequest();

            var response = new ResultResponse
            {
                StatusCode = (int)request.responseCode,
                Body = request.downloadHandler.text
            };
            
            callback(response);
        }
        
        
        public static IEnumerator Get(Dictionary<string, object> body, [CanBeNull] Dictionary<string, object> header,
            string route, Action<ResultResponse> callback)
        {
            var bodyParams = JsonConvert.SerializeObject(body, Formatting.Indented);

            var request = new UnityWebRequest(Configs.Configuration.BaseUrl + route, "GET");

            var jsonToSend = new System.Text.UTF8Encoding().GetBytes(bodyParams);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Accept", "application/json");
            request.uploadHandler.contentType = "application/json";
            
            if (header != null)
            {
                if (header!.Count > 0)
                {
                    foreach (var rowHeader in header)
                        request.SetRequestHeader(rowHeader.Key, rowHeader.Value.ToString());
                }
            }
            
            yield return request.SendWebRequest();

            var response = new ResultResponse
            {
                StatusCode = (int)request.responseCode,
                Body = request.downloadHandler.text
            };
            
            callback(response);
        }
    }
}