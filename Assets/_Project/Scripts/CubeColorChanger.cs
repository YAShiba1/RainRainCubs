using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeColorChanger : MonoBehaviour
{
    [SerializeField] private Color _color;

    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void SetColorToDefault() => _renderer.material.color = _defaultColor;

    public void ChangeColor() => _renderer.material.color = _color;
}
