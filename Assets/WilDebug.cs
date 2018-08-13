using UnityEngine;

public static class WilDebug
{
    public static void DrawCoordinateSystem(Transform gameObject)
    {
        Debug.DrawLine(gameObject.position, gameObject.position + gameObject.rotation * Vector3.up, Color.red);
        Debug.DrawLine(gameObject.position, gameObject.position + gameObject.rotation * Vector3.forward, Color.blue);
        Debug.DrawLine(gameObject.position, gameObject.position + gameObject.rotation * Vector3.left, Color.green);
    }
}