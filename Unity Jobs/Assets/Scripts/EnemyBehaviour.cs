using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 2.5f;
    [SerializeField] private float DistanceThreshold = 3f;

    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //look at player
        Vector2 direction = (_playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion lookRot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = lookRot;

        //move towards player
        float distance = Vector2.Distance(_playerTransform.position, transform.position);
        if (distance < DistanceThreshold)
        {
            transform.position += -(Vector3)direction * Time.deltaTime * _moveSpeed;
        }
    }

}
