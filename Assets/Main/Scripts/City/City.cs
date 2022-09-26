using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

namespace Metaverse.City
{
    public class City : MonoBehaviour
    {
        [SerializeField]
        private Land[] m_Lands;

        private void Start()
        {
            Random.InitState((int)DateTime.Now.Ticks);
        }

        public Land GetVacantLand()
        {
            List<Land> available = new();
            foreach (Land land in m_Lands)
            {
                if (!land.IsBuilt)
                {
                    available.Add(land);
                }
            }

            if (available.Count > 0)
            {
                int index = Random.Range(0, available.Count);
                return available[index];
            }
            else
            {
                return null;
            }
        }
    }
}