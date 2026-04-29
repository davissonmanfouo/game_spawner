using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Obstacle ObstaclePrefab;

    [SerializeField]
    private Vector2 SpawnBounds;

    [SerializeField]
    private Vector2 SpawnDelay;

    [SerializeField]
    private GameManager GameManager;

    private float _nextSpawn;

    private void Awake()
    {
        if (GameManager == null)
        {
            GameManager = FindFirstObjectByType<GameManager>();
        }
    }

    void Update()
    {
        if(Time.time > _nextSpawn)
        {
            SpawnSphere();

            float difficulty = GetDifficulty();
            float delay = Random.Range(SpawnDelay.x, SpawnDelay.y) / difficulty;
            _nextSpawn = Time.time + delay;
        }
    }

    private void SpawnSphere()
    {
        Obstacle o = Instantiate(ObstaclePrefab, transform);
        o.transform.localPosition = new Vector3(
            Random.Range(-SpawnBounds.x, SpawnBounds.x),
            Random.Range(-SpawnBounds.y, SpawnBounds.y),
            0);
        o.SetSpeedMultiplier(GetDifficulty());
    }

    private float GetDifficulty()
    {
        if (GameManager == null)
        {
            return 1f;
        }

        return GameManager.Difficulty;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawCube(transform.position, (Vector3)SpawnBounds);
    }
}
