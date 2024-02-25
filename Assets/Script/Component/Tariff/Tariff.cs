using UnityEngine;
using UnityEngine.UI;

namespace Script.Component.Tariff
{
    public class Tariff: MonoBehaviour
    {
        [SerializeField] private new Text name;

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

        public int Price { get; set; }

        public ToggleGroup ToggleGroup
        {
            set => gameObject.GetComponent<Toggle>().group = value;
        }

        public bool ToggleStatus
        {
            set => gameObject.GetComponent<Toggle>().isOn = value;
        }
    }
}