using UnityEngine;

namespace Metaverse.Game
{
    public class GameManager : MonoBehaviour
    {
        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private CameraController m_CameraController;

        [SerializeField]
        private City.City m_City;

        [SerializeField]
        private GameObject m_Dialog;


        /* ============================================================
         * INTERNAL GETTERS
         * ============================================================*/
        internal CameraController CameraController => m_CameraController;
        internal City.City City => m_City;
        internal GameObject Dialog => m_Dialog;


        /* ============================================================
         * REFERENCE TO ATTACHED OBJECTS
         * ============================================================*/
        private UserBuildingSteps _userBuildingSteps;

        private AbstractSteps _runningSteps = null;


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Start()
        {
            _userBuildingSteps = gameObject.AddComponent<UserBuildingSteps>();
            _runningSteps = _userBuildingSteps;
        }

        private void Update()
        {
            if (_runningSteps && !_runningSteps.IsRunning)
            {
                _runningSteps.RunAll(this);
                _runningSteps = null;
            }
        }
    }
}
