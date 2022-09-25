using ColorSchemeManager;
using Patterns.Singleton;
using UnityEngine;

namespace Metaverse.Building
{
    public class BuildingFactory : Singleton<BuildingFactory>
    {
        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private GameObject[] m_Prefabs;

        [SerializeField]
        private Texture2D[] m_Thumbnails;


        /* ============================================================
         * PUBLIC CONSTANTS
         * ============================================================*/
        public const int SCHEME_GRID_SIZE = 69;


        /* ============================================================
         * FACTORY METHOD PATERN
         * ============================================================*/
        public GameObject GetBuilding(BuildingBlueprint building)
        {
            if (building.Type == BuildingType.None)
            {
                throw new UnityException("There isn't prefab assigned to None (BuildType).");
            }
            else
            {
                int prefabIndex = (int)building.Type - 1;
                GameObject prefab = m_Prefabs[prefabIndex];
                GameObject instance = Instantiate(prefab);
                ColorSchemeSwitcher switcher = instance.GetComponent<ColorSchemeSwitcher>();
                switcher.SetPreset(building.ColorScheme, true);
                return instance;
            }
        }

        public void AttachBuilding(BuildingBlueprint building, Transform parent)
        {
            GameObject instance = GetBuilding(building);
            instance.transform.parent = parent;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
        }

        public Texture2D GetThumbnail(BuildingType type)
        {
            int index = (int)type - 1;
            return m_Thumbnails[index];
        }
    }
}