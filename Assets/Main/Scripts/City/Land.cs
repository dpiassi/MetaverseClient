using UnityEngine;
using Metaverse.Building;
using Metaverse.Utils;
using Metaverse.Game;

namespace Metaverse.City
{
    [RequireComponent(typeof(BoxCollider))]
    public class Land : ClickableBehaviour
    {
        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private bool m_Built = false;

        [SerializeField]
        private BuildingBlueprint m_Building;


        /* ============================================================
         * PUBLIC PROPERTIES
         * ============================================================*/
        public bool IsBuilt => m_Built;
        public BuildingType BuildingType => m_Building.Type;


        /* ============================================================
         * PUBLIC FUNCTIONS
         * ============================================================*/
        public BuildingBlueprint Build()
        {
            if (m_Built)
            {
                throw new UnityException("You can't build over an occupied lot!");
            }
            else
            {
                m_Building.RandomizeColor();
                BuildingFactory.Instance.AttachBuilding(m_Building, transform);
                RefreshColliderBounds();
                m_Built = true;
                return m_Building;
            }
        }


        /* ============================================================
         * PRIVATE FUNCTIONS
         * ============================================================*/
        private void RefreshColliderBounds()
        {
            MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.size = meshRenderer.bounds.size;
            boxCollider.center = new(0f, boxCollider.size.y / 2f, 0f);
        }


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Start()
        {
            if (m_Built)
            {
                m_Building.RandomizeColor();
                BuildingFactory.Instance.AttachBuilding(m_Building, transform);
                RefreshColliderBounds();
            }
        }


        /* ============================================================
         * OVERRIDED FUNCTIONS
         * ============================================================*/
        protected override void OnMouseClick()
        {
            Debug.Log($"{gameObject.name} clicked.");
            GameManager.Instance.RunBuildingClickSteps();
        }
    }
}