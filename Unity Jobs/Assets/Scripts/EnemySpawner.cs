using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;

[BurstCompile]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _numberOfEnemiesToSpawn;
    [SerializeField] private float _spawnRadius = 15f;
    [SerializeField] private Transform _playerTransform;

    [Header("Enemy Stuff")]
    [SerializeField] private float DistanceThreshold = 3f;
    [SerializeField] private float _moveSpeed = 2.5f;

    private bool _isDone;

    private TransformAccessArray _accessArray;
    private Transform[] _enemyTransforms;

    private List<GameObject> _enemiesList = new List<GameObject>();

    private void Awake()
    {
        _enemyTransforms = new Transform[_numberOfEnemiesToSpawn];

        for (int i = 0; i < _numberOfEnemiesToSpawn; i++)
        {
            Vector2 spawnPosition = UnityEngine.Random.insideUnitCircle * _spawnRadius;
            GameObject go = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            _enemiesList.Add(go);
            _enemyTransforms[i] = go.transform;
        }

        _accessArray = new TransformAccessArray(_enemyTransforms);

        _isDone = true;
    }


    private void Update()
    {
        if (_isDone)
        {
            ControlEnemyJob job = new ControlEnemyJob()
            {
                PlayerPos = _playerTransform.position,
                DeltaTime = Time.deltaTime,
                DistanceThreshold = DistanceThreshold,
                MoveSpeed = _moveSpeed
            };

            JobHandle jobHandle = job.Schedule(_accessArray);
            jobHandle.Complete();
        }
    }

}

[BurstCompile]
public struct ControlEnemyJob : IJobParallelForTransform
{
    
    [ReadOnly] public float3 PlayerPos;
    [ReadOnly] public float DeltaTime;
    [ReadOnly] public float DistanceThreshold;
    [ReadOnly] public float MoveSpeed;

    public void Execute(int index, TransformAccess transform)
    {
        //face the player
        float3 direction = math.normalize(PlayerPos - (float3)transform.position);
        float angle = math.atan2(direction.y, direction.x);
        angle -= math.radians(90f);

        quaternion lookRot = quaternion.AxisAngle(new float3(0f, 0f, 1f), angle);
        transform.rotation = lookRot;

        //Move away from player
        float distance = math.distance(PlayerPos, (float3)transform.position);
        float3 newPosition = (float3)transform.position - direction * DeltaTime * MoveSpeed;
        float3 maskedPosition = math.select(transform.position, newPosition, distance <= DistanceThreshold);

        transform.position = maskedPosition;
    }
}