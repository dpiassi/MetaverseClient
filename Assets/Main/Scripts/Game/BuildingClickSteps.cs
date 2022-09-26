using Metaverse.Network;
using UnityEngine;
using UnityEngine.UIElements;

namespace Metaverse.Game
{
    [DisallowMultipleComponent]
    public class BuildingClickSteps : AbstractSteps
    {
        private string _organizationID;
        private string _organizationName;
        private GameObject _dialog;

        protected override void InitSteps()
        {
            Steps.Enqueue(SyncAccountBalance__FromServer);
            Steps.Enqueue(ShowDialog);
            Steps.Enqueue(RestoreVariables);
        }

        public void SyncAccountBalance__FromServer()
        {
            BlockThread();
            Debug.Log(nameof(SyncAccountBalance__FromServer));
            QueryParameter parameter = new("customerId", PlayerPrefs.GetString("customerId"));
            string url = QueryParameter.Build(Endpoints.ACCOUNT_BALANCE, parameter);
            WebRequest request = new(url);
            request.OnResponseSuccess += OnGetUserAccountsSuccess;
            request.OnResponseError += Debug.LogError;
            request.Send();
        }

        private void OnGetUserAccountsSuccess(string body)
        {
            Debug.Log(nameof(OnGetUserAccountsSuccess));
            Debug.Log(body);
            ResumeThread();
            // JArray accounts = (JArray)JsonConvert.DeserializeObject(body);
            // foreach (JToken account in accounts)
            // {
            //     _organizationID = account["organizationID"].ToString();
            //     _organizationName = account["organizationName"].ToString();
            //     ResumeThread();
            // }
        }

        private void ShowDialog()
        {
            Debug.Log(nameof(ShowDialog));
            BlockThread();
            Manager.CameraController.enabled = false;
            _dialog = Instantiate(Manager.SimpleDialog);
            UIDocument document = _dialog.GetComponent<UIDocument>();

            VisualElement root = document.rootVisualElement;
            root.Q<Button>("Button").clicked += OnDialogButtonClicked;
        }

        private void OnDialogButtonClicked()
        {
            Debug.Log(nameof(OnDialogButtonClicked));
            Manager.CameraController.enabled = true;
            Destroy(_dialog);
            ResumeThread();
        }

        private void RestoreVariables()
        {
            Debug.Log(nameof(RestoreVariables));
            BlockThread();
            _organizationID = string.Empty;
            _organizationName = string.Empty;
            _dialog = null;
            ResumeThread();
        }
    }
}