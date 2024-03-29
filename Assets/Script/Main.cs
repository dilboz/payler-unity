using Script.Server.Routs.Tariff;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Transform listTariff;
        [SerializeField] private Component.Tariff.Tariff tariffComponent;

        private Tariff _tariff;

        private static int _selectPrice;

        public void Start()
        {
            _tariff = gameObject.AddComponent<Tariff>();

            //get all tariff
            var tariffs = _tariff.GetTariffs();

            var isFirst = true;
            //print name tariffs
            foreach (var tariff in tariffs)
            {
                var tariffObject = Instantiate(tariffComponent, listTariff);

                tariffObject.Id = tariff.Id;
                tariffObject.Name = tariff.Name;
                tariffObject.Price = tariff.Price;
                tariffObject.ToggleGroup = listTariff.GetComponent<ToggleGroup>();
                tariffObject.ToggleStatus = isFirst;
                _selectPrice = tariff.Price;
                
                isFirst = false;
            }
        }

        public static void SetPrice(int price)
        {
            _selectPrice = price;
            
            print("price: " + price);
        }
    }
}