using UnityEngine;
using Metaverse.Building;

namespace Metaverse.City
{
    [RequireComponent(typeof(BoxCollider))]
    public class Land : ClickableBehaviour
    {
        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private BuildingBlueprint m_Building = null;


        /* ============================================================
         * PUBLIC FUNCTIONS
         * ============================================================*/
        public bool IsVacant()
        {
            return m_Building == null || m_Building.IsVacant();
        }

        public void Build(BuildingBlueprint building)
        {
            if (IsVacant())
            {
                m_Building = building;
                BuildingFactory.Instance.AttachBuilding(m_Building, transform);
                RefreshColliderBounds();
            }
            else
            {
                throw new UnityException("You can't build over an occupied lot!");
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
            if (!IsVacant())
            {
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
        }
    }
}