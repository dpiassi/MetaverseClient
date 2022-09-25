using Metaverse.Building;
using Metaverse.City;
using Metaverse.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Metaverse.Game
{
    [DisallowMultipleComponent]
    public class UserBuildingSteps : AbstractSteps
    {
        private string _organizationID;
        private string _organizationName;
        private Land _land;
        private GameObject _dialog;
        private BuildingBlueprint _buildingBlueprint;

        protected override void InitSteps()
        {
            Steps.Enqueue(SyncUserAccounts__FromServer);
            Steps.Enqueue(SelectLand);
            Steps.Enqueue(ShowDialog);
            Steps.Enqueue(CreateNewBuilding);
            Steps.Enqueue(PropagateEvent);
            Steps.Enqueue(SyncUserBuildings__ToServer);
            Steps.Enqueue(RestoreVariables);
        }

        public void SyncUserAccounts__FromServer()
        {
            BlockThread();
            Debug.Log(nameof(SyncUserAccounts__FromServer));
            QueryParameter parameter = new("customerId", "595.080.896-84"); // TODO get customerId
            string url = QueryParameter.Build(Endpoints.GetUserAccounts, parameter);
            WebRequest request = new(url);
            request.OnResponseSuccess += OnGetUserAccountsSuccess;
            request.OnResponseError += Debug.LogError;
            request.Send();
        }

        private void OnGetUserAccountsSuccess(string response)
        {
            Debug.Log(nameof(OnGetUserAccountsSuccess));
            JArray accounts = (JArray)JsonConvert.DeserializeObject(response);
            foreach (JToken account in accounts)
            {
                _organizationID = account["organizationID"].ToString();
                _organizationName = account["organizationName"].ToString();
                ResumeThread();
            }
        }

        private void SelectLand()
        {
            BlockThread();
            Debug.Log(nameof(SelectLand));
            _land = Manager.City.GetVacantLand();
            ResumeThread();
        }

        private void ShowDialog()
        {
            Debug.Log(nameof(ShowDialog));
            BlockThread();
            Manager.CameraController.enabled = false;
            _dialog = Instantiate(Manager.Dialog);
            UIDocument document = _dialog.GetComponent<UIDocument>();
            Texture2D thumb = BuildingFactory.Instance.GetThumbnail(_land.BuildingType);

            Debug.Log(_land);
            VisualElement root = document.rootVisualElement;
            root.Q<Label>("Title").text = "Novo Empreendimento!";
            root.Q<Label>("Content").text = $"Você conectou seus dados da <b>{_organizationName.ToUpper()}</b> ao Open Finance. Agora você pode acessar sua conta diretamente de sua cidade!";
            root.Q<VisualElement>("Texture").style.backgroundImage = new StyleBackground(thumb);
            root.Q<Button>("Button").clicked += OnDialogButtonClicked;
        }

        private void OnDialogButtonClicked()
        {
            Debug.Log(nameof(OnDialogButtonClicked));
            Manager.CameraController.enabled = true;
            Destroy(_dialog);
            ResumeThread();
        }

        private void CreateNewBuilding()
        {
            BlockThread();
            Debug.Log(nameof(CreateNewBuilding));
            _buildingBlueprint = _land.Build();
            ResumeThread();
        }

        private void PropagateEvent()
        {
            BlockThread();
            Debug.Log(nameof(PropagateEvent));
            CityEventBus.Publish(CityEventType.NEW_BUILDING);
            ResumeThread();
        }

        public void SyncUserBuildings__ToServer()
        {
            BlockThread();
            Debug.Log(nameof(SyncUserBuildings__ToServer));
            QueryParameter[] parameters = {
            new("customerId", "595.080.896-84"), // TODO get customerId
            new("color", _buildingBlueprint.ColorScheme.ToString()),
            new("land", ((int)_buildingBlueprint.Type).ToString())
        };
            string url = QueryParameter.Build(Endpoints.GetUserAccounts, parameters);
            WebRequest request = new(url);
            request.OnResponseSuccess += OnGetUserBuildingsSuccess;
            request.OnResponseError += Debug.LogError;
            request.Send();
        }

        private void OnGetUserBuildingsSuccess(string response)
        {
            Debug.Log(nameof(OnGetUserBuildingsSuccess));
            JArray accounts = (JArray)JsonConvert.DeserializeObject(response);
            foreach (JToken account in accounts)
            {
                ResumeThread();
            }
        }

        private void RestoreVariables()
        {
            Debug.Log(nameof(RestoreVariables));
            BlockThread();
            _organizationID = string.Empty;
            _organizationName = string.Empty;
            _land = null;
            _dialog = null;
            _buildingBlueprint = null;
            ResumeThread();
        }
    }
}