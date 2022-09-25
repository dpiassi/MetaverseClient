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
        private Camera _camera;


        /* ============================================================
         * SERIALIZED FIELDS
         * ============================================================*/
        [Header("Common")]
        [SerializeField]
        private float m_ZoomOutMin = 1f;

        [SerializeField]
        private float m_ZoomOutMax = 20f;

        [Header("Desktop")]
        [SerializeField]
        private float m_MouseWheelSpeed = 2f;

        [Header("Mobile")]
        [SerializeField]
        private float m_TouchPinchSpeed = 0.01f;


        /* ============================================================
         * UNITY MESSAGES
         * ============================================================*/
        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

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
                    Pan(_touchStart - Camera.main.ScreenToWorldPoint(touchZero.position));
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
                Zoom(m_TouchPinchSpeed * difference);
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
                Pan(_touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            #endregion

            #region Mouse | Zoom
            Zoom(m_MouseWheelSpeed * Input.GetAxis("Mouse ScrollWheel"));
            #endregion
#endif
        }


        /* ============================================================
         * PRIVATE FUNCTIONS
         * ============================================================*/
        private void Pan(Vector3 direction)
        {
            Vector3 newPosition = _camera.transform.position + direction;
            _camera.transform.position = newPosition;
        }

        private void Zoom(float increment)
        {
            _camera.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, m_ZoomOutMin, m_ZoomOutMax);
        }
    }
}