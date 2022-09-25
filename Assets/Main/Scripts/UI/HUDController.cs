using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace Metaverse.UI
{
    public class HUDController : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private VisualElement _root;

        private readonly Dictionary<string, Label> _labels = new();

        public static class Labels
        {
            public const string COINS = "Coins";
        }

        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            _labels[Labels.COINS] = _root.Q<Label>("CoinsLabel");
            SetLabelText(Labels.COINS, "tests");
        }

        private void SetLabelText(string key, string text)
        {
            _labels[key].text = text;
        }

        private static void OnClick(ClickEvent evt)
        {
            var button = evt.currentTarget as Button;
            evt.StopPropagation();
        }

    }
}