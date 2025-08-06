using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        var waitForSeconds = new WaitForSeconds(0.9f);

        while (true)
        {
            Cube cube = _cubePool.GetCube();

            cube.ResetState();

            cube.transform.position = GetRandomSpawnPosition();

            cube.TouchedPlatform += OnCubeTouchedPlatform;

            yield return waitForSeconds;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
    }

    private void OnCubeTouchedPlatform(Cube cube)
    {
        StartCoroutine(DestroyCubeWithDelay(cube));
    }

    private IEnumerator DestroyCubeWithDelay(Cube cube)
    {
        var waitForSeconds = new WaitForSeconds(cube.GetLifeTime());

        yield return waitForSeconds;

        cube.TouchedPlatform -= OnCubeTouchedPlatform;

        _cubePool.ReturnCube(cube);
    }
}
