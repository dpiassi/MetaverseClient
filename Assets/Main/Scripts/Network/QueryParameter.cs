
using System.Collections.Specialized;
using System.Web;
using UnityEngine;

namespace Metaverse.Network
{
    [System.Serializable]
    public class QueryParameter
    {
        public string Key;
        public string Value;

        public QueryParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static string Build(string url, QueryParameter parameter)
        {
            return $"{url}?{parameter.Key}={parameter.Value}";
        }

        public static string Build(string url, QueryParameter[] parameters)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach (var parameter in parameters)
            {
                queryString.Add(parameter.Key, parameter.Value);
            }
            Debug.LogWarning(url);
            Debug.LogWarning(queryString);
            return $"{url}?{queryString}";
        }
    }
}