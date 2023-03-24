using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour,ITeleportable
{
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerRotateSpeed;
    private Rigidbody2D _playerRigidbody;
    private Collider2D _collider2D;
    [SerializeField] private float _shootDelay;
    
    private bool IsPressFront => Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    private bool IsPressLeft => Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
    private bool IsPressRight => Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    private bool IsPressShoot => Input.GetMouseButton(0);

    private bool _isAllowShoot = true;

    
    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }
    
    
    void Update()
    {
        InputController();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Asteroid"))
        {
            Debug.Log("Game Over!");
        }
    }


    private void InputController()
    {
        if (IsPressFront)
        {
            _playerRigidbody.AddForce(transform.up * _playerSpeed);
        }

        if (IsPressLeft || IsPressRight)
        {
            var currentRotateSpeed = !IsPressRight ? _playerRotateSpeed : -_playerRotateSpeed;
            transform.Rotate(Vector3.forward * currentRotateSpeed);
        }

        if (IsPressShoot && _isAllowShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _isAllowShoot = false;
        var bullet = Instantiate(bulletPrefab, transform.position, quaternion.identity);
        bullet.Move(transform.up);

        
        
        IEnumerator Do()
        {
            yield return new WaitForSeconds(_shootDelay);
            _isAllowShoot = true;
        }
        
        StartCoroutine(Do());
    }


    public void OnTeleport()
    {
     
        _collider2D.enabled = false;
        IEnumerator Do()
        {
            yield return new WaitForSeconds(.3f);
            _collider2D.enabled = true;
        }

        StartCoroutine(Do());
    }
}
