using Assets;
using UnityEngine;

public class HandMove : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private bool _disable;

    public void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _disable = true;

        if (Input.GetMouseButtonDown(0))
            _disable = false;

        if (GameState.GameOver || _disable)
            return;

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        var height = Input.mouseScrollDelta.y;
        _rigidBody.MovePosition(_rigidBody.position + new Vector3(mouseX, height, mouseY)*0.1f);
    }
}
