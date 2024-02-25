using Script.Server.Routs.Tariff;
using UnityEngine;

namespace Script
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Transform listTariff;
        [SerializeField] private Component.Tariff.Tariff tariffComponent;
        
        private Tariff _tariff;
        public async void Start()
        {
            _tariff = gameObject.AddComponent<Tariff>();
            
            //get all tariff
            var tariffs =  await _tariff.GetTariffs();
            
            //print name tariffs
            foreach (var tariff in tariffs)
            {
                var tariffObject = Instantiate(tariffComponent, listTariff);

                tariffObject.Id = tariff.Id;
                tariffObject.Name = tariff.Name;

            }
        }
    }
}
