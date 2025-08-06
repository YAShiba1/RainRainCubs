using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Renderer), typeof(CubeColorChanger))]
public class Cube : MonoBehaviour
{
    private CubeColorChanger _cubeColorChanger;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Color _defaultColor;

    private bool _hasTouchedPlatform = false;

    public event UnityAction<Cube> TouchedPlatform;

    private void Awake()
    {
        _cubeColorChanger = GetComponent<CubeColorChanger>();
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _defaultColor = _renderer.material.color;
    }

    public int GetLifeTime()
    {
        int minLifeTime = 2;
        int maxLifeTime = 5;

        return Random.Range(minLifeTime, maxLifeTime + 1);
    }

    public void ResetState()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _renderer.material.color = _defaultColor;
        _hasTouchedPlatform = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false && collision.transform.TryGetComponent(out Platform platform))
        {
            TouchedPlatform?.Invoke(this);
            _cubeColorChanger.Change(_renderer);

            _hasTouchedPlatform = true;
        }
    }
}
