using UnityEngine;

namespace Metaverse.Building
{
    [System.Serializable]
    public class BuildingBlueprint
    {
        /* ============================================================
         * PUBLIC PROPERTIES
         * ============================================================*/
        public int ColorScheme => m_ColorScheme;
        public BuildingType Type => m_Type;


        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private int m_ColorScheme;

        [SerializeField]
        private BuildingType m_Type;


        /* ============================================================
         * PUBLIC FUNCTIONS
         * ============================================================*/
        public bool IsVacant()
        {
            return m_Type == BuildingType.Vacant;
        }
    }
}