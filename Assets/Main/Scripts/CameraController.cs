using UnityEngine;


namespace Metaverse
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        /* ============================================================
         * AUXILIAR MEMBERS
         * ============================================================*/
        private Vector3 _touchStart;


        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [SerializeField]
        private float m_ZoomSpeedMouseWheel = 2f;

        [SerializeField]
        private float m_ZoomSpeedTouch = 0.01f;

        [SerializeField]
        private float m_ZoomOutMin = 1f;

        [SerializeField]
        private float m_ZoomOutMax = 20f;


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Update()
        {
#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount == 1)
            {
                #region Touch | Pan
                Touch touchZero = Input.GetTouch(0);
                if (touchZero.phase == TouchPhase.Began)
                {
                    _touchStart = Camera.main.ScreenToWorldPoint(touchZero.position);
                }
                else if (touchZero.phase == TouchPhase.Moved)
                {
                    Vector3 direction = _touchStart - Camera.main.ScreenToWorldPoint(touchZero.position);
                    Camera.main.transform.position += direction;
                }
                #endregion
            }
            else if (Input.touchCount == 2)
            {
                #region Touch | Zoom
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;
                Zoom(m_ZoomSpeedTouch * difference);
                #endregion

                #region Touch | Pan
                if (touchZero.phase == TouchPhase.Ended)
                {
                    _touchStart = Camera.main.ScreenToWorldPoint(touchOne.position);
                }
                else if (touchOne.phase == TouchPhase.Ended)
                {
                    _touchStart = Camera.main.ScreenToWorldPoint(touchZero.position);
                }
                #endregion
            }
#endif

#if UNITY_EDITOR
            #region Mouse | Pan
            if (Input.GetMouseButtonDown(0))
            {
                _touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = _touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position += direction;
            }
            #endregion

            #region Mouse | Zoom
            Zoom(m_ZoomSpeedMouseWheel * Input.GetAxis("Mouse ScrollWheel"));
            #endregion
#endif
        }


        /* ============================================================
         * PRIVATE FUNCTIONS
         * ============================================================*/
        private void Zoom(float increment)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, m_ZoomOutMin, m_ZoomOutMax);
        }
    }
}