using UnityEngine;
using UnityEngine.UI;

namespace Script.Component.Tariff
{
    public class Tariff: MonoBehaviour
    {
        [SerializeField] private Text name;

        public int Id
        {
            get => int.Parse(gameObject.name);
            set => gameObject.name = value.ToString();
        }

        public string Name
        {
            get => name.text;
            set => name.text = value;
        }
    }
}