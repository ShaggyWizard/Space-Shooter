using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _pooler;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;


    private float _lastSpawnTime;

    private void OnValidate()
    {
        _minSpeed = _minSpeed < 0 ? 0 : _minSpeed;
        _maxSpeed = _maxSpeed < _minSpeed ? _minSpeed : _maxSpeed;
    }
    private void Update()
    {
        _lastSpawnTime += Time.deltaTime;
        if (_lastSpawnTime > _spawnRate)
        {
            _lastSpawnTime -= _spawnRate;
            var pooledObject = _pooler.GetPooledObject();

            if (pooledObject != null && _maxSpeed != 0)
            {
                Vector3 left = transform.position + transform.rotation * new Vector3(_boxCollider.center.x - _boxCollider.size.x * .5f, 0, _boxCollider.center.z - _boxCollider.size.z * .5f);
                Vector3 right = left + transform.rotation * new Vector3(_boxCollider.size.x, 0, 0);

                pooledObject.Spawn(Vector3.Lerp(left, right, Random.Range(0f, 1f)), transform.rotation);

                if (pooledObject is IProjectile)
                {
                    IProjectile projectile = pooledObject as IProjectile;

                    projectile.SetDistance(_boxCollider.size.z);
                    projectile.SetSpeed(Random.Range(_minSpeed, _maxSpeed));
                }
            }
        }
    }
}