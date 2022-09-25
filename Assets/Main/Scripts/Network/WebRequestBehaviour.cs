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

        [SerializeField]
        private QueryParameter[] m_Parameters;


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Start()
        {
            string url = QueryParameter.Build(m_Endpoint, m_Parameters);
            WebRequest request = new(url, m_Method);
            request.OnResponseSuccess += Debug.Log;
            request.OnResponseError += Debug.LogError;
            request.Send();
        }
    }
}