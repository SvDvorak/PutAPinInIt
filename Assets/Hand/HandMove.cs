using Assets;
using UnityEngine;

public class HandMove : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private bool _disable;
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    private float _minZ;
    private float _maxZ;

    public void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _minX = -6.7f;
        _maxX = 4.3f;
        _minY = 0.2f;
        _maxY = 4f;
        _minZ = -9.7f;
        _maxZ = -2f;
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
        //_rigidBody.MovePosition(new Vector3(_rigidBody.position.x, _height, _rigidBody.position.z) + movement);
        var position = ClampToPlaySpace(_rigidBody.position + new Vector3(mouseX, height, mouseY) * 0.1f);

        _rigidBody.MovePosition(position);
    }

    private Vector3 ClampToPlaySpace(Vector3 position)
    {
        var newX = Mathf.Clamp(position.x, _minX, _maxX);
        var newY = Mathf.Clamp(position.y, _minY, _maxY);
        var newZ = Mathf.Clamp(position.z, _minZ, _maxZ);

        return new Vector3(newX, newY, newZ);
    }
}