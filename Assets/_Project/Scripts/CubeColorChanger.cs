using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    [SerializeField] private Color _color;

    public void Change(Renderer renderer)
    {
        renderer.material.color = _color;
    }
}
