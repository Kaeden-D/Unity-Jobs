using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Collections;

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

    private List<GameObject> _enemiesList = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _numberOfEnemiesToSpawn; i++)
        {
            Vector2 spawnPosition = UnityEngine.Random.insideUnitCircle * _spawnRadius;
            GameObject go = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            _enemiesList.Add(go);
        }

        _isDone = true;
    }

/*
    private void Update()
    {
        for (int i = 0; i < _enemiesList.Count; i++)
        {
            //facing direction
            Vector2 direction = (_playerTransform.position - _enemiesList[i].transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion lookRot = Quaternion.AngleAxis(angle, Vector3.forward);
            _enemiesList[i].transform.rotation = lookRot;

            //position
            float distance = Vector2.Distance(_playerTransform.position, _enemiesList[i].transform.position);
            if (distance <= DistanceThreshold)
            {
                _enemiesList[i].transform.position += -(Vector3)direction * Time.deltaTime * _moveSpeed;
            }
        }
    }*/

}