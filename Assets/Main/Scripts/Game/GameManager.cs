using Patterns.Singleton;
using UnityEngine;

namespace Metaverse.Game
{
    [DisallowMultipleComponent]
    public class GameManager : Singleton<GameManager>
    {
        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private CameraController m_CameraController;

        [SerializeField]
        private City.City m_City;

        [SerializeField]
        private GameObject m_NewBuildingDialog;

        [SerializeField]
        private GameObject m_SimpleDialog;


        /* ============================================================
         * INTERNAL GETTERS
         * ============================================================*/
        internal CameraController CameraController => m_CameraController;
        internal City.City City => m_City;
        internal GameObject NewBuildingDialog => m_NewBuildingDialog;
        internal GameObject SimpleDialog => m_SimpleDialog;


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


        /* ============================================================
         * PUBLIC FUNCTIONS
         * ============================================================*/
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

        public void RunBuildingClickSteps()
        {
            if (_runningSteps == null || !_runningSteps.IsRunning)
            {
                _runningSteps = gameObject.GetComponent<BuildingClickSteps>();
                if (_runningSteps == null)
                {
                    _runningSteps = gameObject.AddComponent<BuildingClickSteps>();
                }
            }
        }
    }
}
