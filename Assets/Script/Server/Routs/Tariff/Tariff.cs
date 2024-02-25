using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Script.Server.Query;
using UnityEngine;

namespace Script.Server.Routs.Tariff
{
    public class Tariff : MonoBehaviour, ITariff
    {
        private QueryToServer _queryToServer;

        private const string AllTariffs = "tariff/all";
        
        private void Start()
        {
            _queryToServer = gameObject.AddComponent<QueryToServer>();
        }
        
        public List<Model.Tariff.Tariff> GetTariffs()
        {
            var tcs = new TaskCompletionSource<List<Model.Tariff.Tariff>>();
            
            /*StartCoroutine(QueryToServer.Get(new Dictionary<string, object>(), null, AllTariffs, response =>
            {
                if (response.StatusCode == 200)
                {
                    var tariffs = JsonConvert.DeserializeObject<List<Model.Tariff.Tariff>>(response.Body);
                    tcs.SetResult(tariffs);
                }
                else
                {
                    tcs.SetException(new System.Exception($"Failed to get tariffs. Status code: {response.StatusCode}"));
                }
            }));*/
            
            var tariffs = new List<Model.Tariff.Tariff>()
            {
                new() { Id = 1, Name = "Tariff1", Price = 100 },
                new() { Id = 2, Name = "Tariff2", Price = 200 },
                new() { Id = 3, Name = "Tariff3", Price = 300 },
                new() { Id = 4, Name = "Tariff4", Price = 400 },
                new() { Id = 5, Name = "Tariff5", Price = 500 }
            };
            
            return tariffs;
        }
    }
}