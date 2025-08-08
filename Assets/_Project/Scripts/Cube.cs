using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(CubeColorChanger))]
public class Cube : MonoBehaviour
{
    private CubeColorChanger _cubeColorChanger;
    private Rigidbody _rigidbody;

    private bool _hasTouchedPlatform = false;

    public event UnityAction<Cube> LifetimeExpired;

    private void Awake()
    {
        _cubeColorChanger = GetComponent<CubeColorChanger>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false && collision.transform.TryGetComponent(out Platform platform))
        {
            StartCoroutine(CountdownLifeTime());

            _cubeColorChanger.ChangeColor();

            _hasTouchedPlatform = true;
        }
    }

    public void ResetState()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _cubeColorChanger.SetColorToDefault();
        _hasTouchedPlatform = false;
    }

    private IEnumerator CountdownLifeTime()
    {
        var waitForSeconds = new WaitForSeconds(GetLifeTime());

        yield return waitForSeconds;

        LifetimeExpired?.Invoke(this);
    }

    private int GetLifeTime()
    {
        int minLifeTime = 2;
        int maxLifeTime = 5;

        return Random.Range(minLifeTime, maxLifeTime + 1);
    }
}
