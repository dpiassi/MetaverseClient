using UnityEngine;

namespace Metaverse
{
    public abstract class ClickableBehaviour : MonoBehaviour
    {
        /* ============================================================
         * CONSTANTS
         * ============================================================*/
        private const float CLICK_THRESHOLD = 0.25f;


        /* ============================================================
         * AUXILIAR MEMBERS
         * ============================================================*/
        private float _mouseDownTime = 0f;


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void OnMouseDown()
        {
            _mouseDownTime = Time.time;
        }

        private void OnMouseUpAsButton()
        {
            bool isClick = Time.time - _mouseDownTime < CLICK_THRESHOLD;
            if (isClick)
            {
                OnMouseClick();
            }
        }


        /* ============================================================
         * VIRTUAL FUNCTIONS
         * ============================================================*/
        protected abstract void OnMouseClick();
    }
}
