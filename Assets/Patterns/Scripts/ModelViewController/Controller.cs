using UnityEngine;

namespace Patterns.MVC
{
    /// <summary>
    /// BRAIN: controls and decides how data (Model) is displayed (View).
    /// </summary>
    public abstract class Controller : MonoBehaviour
    {
        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private Model m_Model;

        [SerializeField]
        private View m_View;

        /* ============================================================
         * ABSTRACT FUNCTIONS
         * ============================================================*/
        protected abstract void Setup(ref Model model, View view);
        protected abstract void SubscribeToViewEvents(View view);
        protected abstract void UnsubscribeFromViewEvents(View view);
        protected abstract void ReadInputsAndUpdateModel(ref Model model);

        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        protected void OnEnable()
        {
            // Listen to View (User Interface) requests.
            SubscribeToViewEvents(m_View);
        }

        protected void OnDisable()
        {
            // Listen to View (User Interface) requests.
            UnsubscribeFromViewEvents(m_View);
        }

        protected void Start()
        {
            Setup(ref m_Model, m_View);
            m_View.SetupUserInterface(m_Model);
        }

        protected void Update()
        {
            ReadInputsAndUpdateModel(ref m_Model);
            m_View.UpdateUserInterface(m_Model);
        }
    }
}
