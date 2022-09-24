using ColorSchemeManager;
using Patterns.Singleton;
using UnityEngine;

namespace Metaverse.Building
{
    public class BuildingFactory : Singleton<BuildingFactory>
    {
        [SerializeField]
        private GameObject[] m_Prefabs;

        private const int SCHEME_GRID_SIZE = 64;

        public GameObject GetBuilding(BuildingBlueprint building)
        {
            if (building.IsVacant())
            {
                throw new UnityException("There isn't prefab assigned to Vacant (BuildType).");
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
    }
}