using UnityEngine;

public class __State__ExampleState : MonoBehaviour, I__State__State
{
    /* ============================================================
     * AUXILIAR MEMBERS
     * ============================================================*/
    private __State__Controller _controller;

    /* ============================================================
     * STATE PATTERN
     * ============================================================*/
    public void Handle(__State__Controller controller)
    {
        if (!_controller)
            _controller = controller;

        // TODO set __State__Controller properties directly.
    }

    /* ============================================================
     * UNITY MESSAGES
     * ============================================================*/
    // TODO implement Update, FixedUpdate, LateUpdate...
}
