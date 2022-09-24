using UnityEngine;

public class ClientState : MonoBehaviour
{
    private __State__Controller _controller;

    void Start()
    {
        _controller = (__State__Controller)FindObjectOfType(typeof(__State__Controller));
    }

    void OnGUI()
    {
        if (GUILayout.Button("Start"))
            _controller.StartExample();
    }
}
