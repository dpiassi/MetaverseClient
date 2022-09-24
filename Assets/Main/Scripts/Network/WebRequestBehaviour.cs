using UnityEngine;

namespace Metaverse.Network
{
    public class WebRequestBehaviour : MonoBehaviour
    {
        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private string m_Endpoint;

        [SerializeField]
        private HTTPMethod m_Method;


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Start()
        {
            WebRequest request = new(m_Endpoint, m_Method);
            request.OnResponseSuccess += Debug.Log;
            request.OnResponseError += Debug.LogError;
            request.Send();
        }
    }
}