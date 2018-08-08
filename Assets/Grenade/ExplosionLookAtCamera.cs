using UnityEngine;
using UnityEngine.Animations;

public class ExplosionLookAtCamera : MonoBehaviour
{
    public void Start()
    {
        var lookAtConstraint = gameObject.AddComponent<LookAtConstraint>();
        var camera = Camera.main.transform;
        lookAtConstraint.AddSource(new ConstraintSource() { sourceTransform = camera, weight = 1 });
        lookAtConstraint.constraintActive = true;
        var toCamera = camera.position - transform.position;
        transform.position += toCamera.normalized*2;
    }
}