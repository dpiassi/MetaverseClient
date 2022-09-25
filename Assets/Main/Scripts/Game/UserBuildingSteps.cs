using Metaverse.Building;
using Metaverse.City;
using Metaverse.Game;
using Metaverse.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UserBuildingSteps : AbstractSteps
{
    private string _organizationID;
    private string _organizationName;
    private Land _land;
    private GameObject _dialog;

    protected override void InitSteps()
    {
        Steps.Enqueue(SyncUserAccounts);
        Steps.Enqueue(SelectLand);
        Steps.Enqueue(ShowDialog);
        Steps.Enqueue(CreateNewBuilding);
        Steps.Enqueue(PropagateEvent);
    }

    public void SyncUserAccounts()
    {
        BlockThread();
        Debug.Log(nameof(SyncUserAccounts));
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
        root.Q<Label>("Content").text = $"Você acabou de conectar seus dados da <b>{_organizationName.ToUpper()}</b> ao <i>Open Finance</i>. Agora você pode acessar sua conta diretamente de sua cidade!";
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
        _land.Build();
        ResumeThread();
    }

    private void PropagateEvent()
    {
        BlockThread();
        Debug.Log(nameof(PropagateEvent));
        CityEventBus.Publish(CityEventType.NEW_BUILDING);
        ResumeThread();
    }
}
