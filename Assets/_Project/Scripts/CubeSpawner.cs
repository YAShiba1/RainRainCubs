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

            cube.LifetimeExpired += OnCubeLifetimeExpired;

            yield return waitForSeconds;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
    }

    private void OnCubeLifetimeExpired(Cube cube)
    {
        cube.LifetimeExpired -= OnCubeLifetimeExpired;

        _cubePool.ReturnCube(cube);
    }
}
