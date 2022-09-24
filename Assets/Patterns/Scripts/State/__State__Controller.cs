using UnityEngine;

public class __State__Controller : MonoBehaviour
{
    /* ============================================================
     * SERIALIZED FIELDS
     * ============================================================*/
    // TODO add serialized fields.

    /* ============================================================
     * PROPERTIES
     * ============================================================*/
    // TODO add properties.

    /* ============================================================
     * STATE PATTERN
     * ============================================================*/
    // TODO add variables tracking all states here.
    private I__State__State _exampleState;

    private __State__StateContext _stateContext;

    /* ============================================================
     * UNITY MESSAGES
     * ============================================================*/
    private void Start()
    {
        // Init __State__StateContext:
        _stateContext = new __State__StateContext(this);

        // TODO init all states here:
        _exampleState = gameObject.AddComponent<__State__ExampleState>();

        // TODO transit to initial state:
        _stateContext.Transition(_exampleState);
    }

    /* ============================================================
     * PUBLIC FUNCTIONS
     * ============================================================*/
    public void StartExample()
    {
        _stateContext.Transition(_exampleState);
    }
}
