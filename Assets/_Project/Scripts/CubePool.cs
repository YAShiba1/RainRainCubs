using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolCapacity = 10;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => CreateCube(),
            actionOnGet: (cube) => cube.gameObject.SetActive(true),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolCapacity
            );

        PrewarmPool();
    }

    public Cube GetCube() => _pool.Get();

    public void ReturnCube(Cube cube) => _pool.Release(cube);

    private Cube CreateCube()
    {
        return Instantiate(_cubePrefab);
    }

    private void PrewarmPool()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            Cube cube = _pool.Get();
            _pool.Release(cube);
        }
    }
}
