using UnityEngine;

namespace Metaverse.Utils
{
    public class Screenshot : MonoBehaviour
    {
#if UNITY_EDITOR
        private bool _done = false;

        private void Update()
        {
            if (!_done)
            {
                ScreenCapture.CaptureScreenshot("Screenshot.png");
                _done = true;
            }
        }
#endif
    }
}