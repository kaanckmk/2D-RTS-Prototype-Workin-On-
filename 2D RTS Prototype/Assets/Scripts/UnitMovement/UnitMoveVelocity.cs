using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveVelocity : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 _velocityVector;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocityVector)
    {
        _velocityVector = velocityVector;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _velocityVector * moveSpeed;
        
    }
}
