using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Metaverse.City;

namespace Metaverse.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class HUDController : MonoBehaviour
    {
        /* ============================================================
         * AUXILIAR MEMBERS
         * ============================================================*/
        private UIDocument _uiDocument;
        private VisualElement _root;
        private readonly Dictionary<string, Label> _labels = new();


        /* ============================================================
         * PUBLIC CONSTANTS
         * ============================================================*/
        public static class Labels
        {
            public const string COINS = "Coins";
            public const string CASH = "Cash";
            public const string POPULATION = "Population";
            public const string HEALTH = "Health";
            public const string BUILDINGS = "Buildings";
        }


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;

            // Init references to Label objects: 
            AddLabel(Labels.COINS);
            AddLabel(Labels.CASH);
            AddLabel(Labels.BUILDINGS);
            AddLabel(Labels.POPULATION);
            AddLabel(Labels.HEALTH);

            // Set initial Label text:
            OnCoinsChanged();
            OnCashChanged();
            OnBuildingsChanged();
            OnPopulationChanged();
            OnHealthChanged();
        }

        private void OnEnable()
        {
            CityEventBus.Subscribe(CityEventType.COINS_CHANGED, OnCoinsChanged);
            CityEventBus.Subscribe(CityEventType.CASH_CHANGED, OnCashChanged);
            CityEventBus.Subscribe(CityEventType.BUILDINGS_CHANGED, OnBuildingsChanged);
            CityEventBus.Subscribe(CityEventType.POPULATION_CHANGED, OnPopulationChanged);
            CityEventBus.Subscribe(CityEventType.HEALTH_CHANGED, OnHealthChanged);
        }


        /* ============================================================
         * PUBLIC FUNCTIONS
         * ============================================================*/
        public void SetLabelText(string key, string text)
        {
            _labels[key].text = text;
        }


        /* ============================================================
         * PRIVATE FUNCTIONS
         * ============================================================*/
        private void AddLabel(string key)
        {
            _labels[key] = _root.Q<Label>($"{key}Label");
        }


        /* ============================================================
         * EVENTS CALLBACKS
         * ============================================================*/
        private void OnCoinsChanged()
        {
            SetLabelText(Labels.COINS, "12.345");
        }

        private void OnCashChanged()
        {
            float money = 123.45f;
            SetLabelText(Labels.CASH, money.ToString());
        }

        private void OnBuildingsChanged()
        {
            SetLabelText(Labels.BUILDINGS, "9/10");
        }

        private void OnPopulationChanged()
        {
            SetLabelText(Labels.POPULATION, "1400");
        }

        private void OnHealthChanged()
        {
            SetLabelText(Labels.POPULATION, "10%");
        }
    }
}