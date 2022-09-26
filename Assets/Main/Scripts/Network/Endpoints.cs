
namespace Metaverse.Network
{
    public static class Endpoints
    {
        private const string BASE_URL = "https://k3tw1821cb.execute-api.sa-east-1.amazonaws.com/v-1/";
        public const string ACCOUNT_BALANCE = BASE_URL + "account-balance"; // GET
        public const string COINS = BASE_URL + "coins"; // POST, DELETE
        public const string EXPERIENCE = BASE_URL + "experience"; // POST
        public const string HEALTH_STATUS = BASE_URL + "health-status"; // GET
        public const string USER_INFORMATION = BASE_URL + "user-information"; // GET
        public const string USER_BUILDINGS = BASE_URL + "user-buildings"; // GET, POST
    }
}