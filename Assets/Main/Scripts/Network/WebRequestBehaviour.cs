using System.Collections.Specialized;
using System.Web;
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
        private Parameter[] m_Parameters;


        /* ============================================================
         * SERIALIZABLE CLASS
         * ============================================================*/
        [System.Serializable]
        public class Parameter
        {
            public string Key;
            public string Value;
        }


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Start()
        {
            WebRequest request = new(BuildURL(), m_Method);
            request.OnResponseSuccess += Debug.Log;
            request.OnResponseError += Debug.LogError;
            request.Send(m_Parameters);
        }

        private string BuildURL()
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach (var parameter in m_Parameters)
            {
                queryString.Add(parameter.Key, parameter.Value);
            }
            Debug.LogWarning(queryString.ToString());
            return $"{m_Endpoint}?{queryString}";
        }
    }
}