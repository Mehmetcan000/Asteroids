using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour ,ITeleportable
{
    private Rigidbody2D asteroidRigidbody;
    private Collider2D _collider2D;
    private float _speed;

    private void Awake()
    {
        asteroidRigidbody = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }


    public void Move(Vector3 direction, float speed)
    {
        _speed = speed;
        asteroidRigidbody.AddForce(direction*speed);
    }

    public void Split()
    {
        var targetScale = transform.localScale.x / 2;
        if (targetScale>0.6)
        {
            var prefab=AsteroidManager.Instance._asteroidPrefab;
            for (int i = 0; i < 2; i++)
            {
                var asteroidController = Instantiate(prefab, transform.position, quaternion.identity);
                asteroidController.transform.localScale = transform.localScale / 2;

                var direction = asteroidRigidbody.velocity.normalized + new Vector2(Random.value, Random.value);
                asteroidController.Move(direction,_speed*4);
            }
        }
        Destroy(gameObject);
    }


    public void OnTeleport()
    {
        _collider2D.enabled = false;
        IEnumerator Do()
        {
            yield return new WaitForSeconds(2f);
            _collider2D.enabled = true;
        }

        StartCoroutine(Do());
    }
}
