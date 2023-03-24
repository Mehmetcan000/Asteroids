using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AsteroidManager : MonoBehaviour
{
    public  static  AsteroidManager Instance { get; private set; }
    
    public AsteroidController _asteroidPrefab;

    [SerializeField] private Vector2 spawnDelayRange;
    [SerializeField] private Vector2 speedRange;
    [SerializeField] private  Vector2 scaleRange;

    [SerializeField] private MyPointManager spawnPoint;
    [SerializeField] private MyPointManager targetPoint;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartSpawn();
    }

    public AsteroidController GetAsteroid;
    
    private void StartSpawn()
    {
        IEnumerator Do()
        {
            while (true)
            {
               
                var spawnPosition = spawnPoint.GetPosition();
                var current = Instantiate(_asteroidPrefab, spawnPosition, quaternion.identity);
                current.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
                var speed = Random.Range(speedRange.x, speedRange.y);
                var direction = targetPoint.GetPosition()-spawnPosition; 
                current.Move(direction,speed);
                var delay = Random.Range(spawnDelayRange.x, spawnDelayRange.y);
                yield return new WaitForSeconds(delay);
            }
        }

        StartCoroutine(Do());
    }
    
    

}
