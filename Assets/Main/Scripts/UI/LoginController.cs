using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace Metaverse.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class LoginController : MonoBehaviour
    {
        /* ============================================================
        * AUXILIAR MEMBERS
        * ============================================================*/
        private UIDocument _uiDocument;
        private VisualElement _root;
        private TextField _inputText;

        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();
            _root = _uiDocument.rootVisualElement;
            
            Button joinButton = _root.Q<Button>("Join");
            Button randomizeButton = _root.Q<Button>("Change");
            _inputText = _root.Q<TextField>("CPF");

            joinButton.clicked += OnJoinButtonClicked;
            randomizeButton.clicked += OnChangeButtonClicked;
            _inputText.SetValueWithoutNotify(MockUsers.GetCurrent());
        }

        public void OnChangeButtonClicked()
        {
            _inputText.SetValueWithoutNotify(MockUsers.GetNext());
        }

        public void OnJoinButtonClicked()
        {
            PlayerPrefs.SetString("customerId", MockUsers.GetCurrent());
            PlayerPrefs.Save();
            SceneManager.LoadScene("City");
        }

    }
}

