using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{

    private Rigidbody2D _bulletRigidbody; 
    [SerializeField] private float _bulletSpeed;
    private float asteroidDieTime=4f;
    
    
    private void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 direction)
    {
        _bulletRigidbody.AddForce(_bulletSpeed * direction);

        IEnumerator Do()
        {
            yield return new WaitForSeconds(asteroidDieTime);
            Destroy(gameObject);
        }

        StartCoroutine(Do());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Asteroid"))
        {
            var asteroid = col.GetComponent<AsteroidController>();
            asteroid.Split();
            
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
