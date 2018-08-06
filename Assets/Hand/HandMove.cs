using UnityEngine;

public class HandMove : MonoBehaviour
{
    private Vector3 _previousMousePosition;
    private Rigidbody _rigidBody;

    public void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _previousMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Update()
    {
        var mousePosition = Input.mousePosition;
        var mouseDelta = (mousePosition - _previousMousePosition) * 0.01f;
        _previousMousePosition = mousePosition;

        var height = Input.mouseScrollDelta.y*0.1f;
        _rigidBody.MovePosition(_rigidBody.position + new Vector3(mouseDelta.x, height, mouseDelta.y));

    }
}
