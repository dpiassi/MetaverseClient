using System.Collections;
using Metaverse.Building;
using Metaverse.City;
using Metaverse.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Metaverse
{
    public class GameManager : MonoBehaviour
    {
        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private CameraController m_CameraController;

        [SerializeField]
        private Land m_Land;

        [SerializeField]
        private GameObject m_Dialog;


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private IEnumerator Start()
        {
            QueryParameter parameter = new("customerId", "595.080.896-84");
            string url = QueryParameter.Build(Endpoints.GetUserAccounts, parameter);
            WebRequest request = new(url);
            request.OnResponseSuccess += OnGetUserAccountsSuccess;
            request.OnResponseError += OnResponseError;
            request.Send();
            yield return null;
        }


        /* ============================================================
         * EVENTS CALLBACKS
         * ============================================================*/
        private void OnGetUserAccountsSuccess(string response)
        {
            JArray accounts = (JArray)JsonConvert.DeserializeObject(response);
            foreach (JToken account in accounts)
            {
                string organizationID = account["organizationID"].ToString();
                string organizationName = account["organizationName"].ToString();
                CreateNewBuilding(organizationName);
            }
        }

        private void OnResponseError(string errorCode)
        {
            Debug.LogError(errorCode);
        }

        private void CreateNewBuilding(string organizationName)
        {
            BuildingBlueprint blueprint = new(true);
            m_Land.Build(blueprint);
            CityEventBus.Publish(CityEventType.NEW_BUILDING);

            m_CameraController.enabled = false;
            GameObject dialog = Instantiate(m_Dialog);
            UIDocument document = dialog.GetComponent<UIDocument>();
            Texture2D thumb = BuildingFactory.Instance.GetThumbnail(blueprint.Type);

            VisualElement root = document.rootVisualElement;
            root.Q<Label>("Title").text = "Novo Empreendimento!";
            root.Q<Label>("Content").text = $"Você acabou de conectar seus dados da <b>{organizationName.ToUpper()}</b> ao <i>Open Finance</i>. Agora você pode acessar sua conta diretamente de sua cidade!";
            root.Q<VisualElement>("Texture").style.backgroundImage = new StyleBackground(thumb);
            root.Q<Button>("Button").clicked += () =>
            {
                Destroy(dialog);
            };
            
            m_CameraController.enabled = true;
        }
    }
}
