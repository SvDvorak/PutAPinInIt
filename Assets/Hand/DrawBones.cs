// DrawBones.cs
using UnityEngine;

[ExecuteInEditMode]
public class DrawBones : MonoBehaviour
{
    private SkinnedMeshRenderer _renderer;

    public void Start()
    {
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (_renderer == null)
        {
            Debug.LogWarning("No SkinnedMeshRenderer found, script removed");
            Destroy(this);
        }
    }

    public void LateUpdate()
    {
        var bones = _renderer.bones;
        foreach (var B in bones)
        {
            if (B.parent == null)
                continue;
            Debug.DrawLine(B.position, B.parent.position, Color.red);
        }
    }
}
