using UnityEngine;

namespace Metaverse.Game
{
    [DisallowMultipleComponent]
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
        private AbstractSteps _runningSteps = null;


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Start()
        {
            if (!PlayerPrefs.HasKey("customerId"))
            {
                PlayerPrefs.SetString("customerId", "797.640.888-18");
                PlayerPrefs.Save();
            }

            RunUserBuildingSteps();
        }

        private void Update()
        {
            if (_runningSteps && !_runningSteps.IsRunning)
            {
                _runningSteps.RunAll(this);
                _runningSteps = null;
            }
        }

        public void RunUserBuildingSteps()
        {
            if (_runningSteps == null || !_runningSteps.IsRunning)
            {
                _runningSteps = gameObject.GetComponent<UserBuildingSteps>();
                if (_runningSteps == null)
                {
                    _runningSteps = gameObject.AddComponent<UserBuildingSteps>();
                }
            }
        }
    }
}
