using UnityEngine;
using System.Linq;

namespace Metaverse.City
{
    public class City : MonoBehaviour
    {
        [SerializeField]
        private Land[] m_Lands;

        private void Start()
        {

        }

        public Land GetVacantLand()
        {
            return m_Lands.First(land => !land.IsBuilt);
        }
    }
}