using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PhysicsController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Collider2D _colider;
    public float VelocityY => _rigidbody2D.velocity.y;
    public float VelocityX => _rigidbody2D.velocity.x;
    public float DefualtGravity { get; private set; }

    public bool Collision
    {
        get
        {
            return Collision;
        }
        set
        {
            _colider.enabled = value;
        }
    }

    public bool IsFalling => VelocityY < 0;

    private void Awake()
    {
        _colider = GetComponentInChildren<Collider2D>(); // The game is't that big for this to become an issue.
        _rigidbody2D = GetComponent<Rigidbody2D>();
        DefualtGravity = _rigidbody2D.gravityScale;
    }

    public void AddForce(float x, float y)
    {
        _rigidbody2D.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
    }

    public void AddForce(Vector2 forceVector)
    {
        _rigidbody2D.AddForce(forceVector, ForceMode2D.Impulse);
    }

    #region Velocity

    public void SetVelocity(float x, float y)
    {
        _rigidbody2D.velocity = new Vector2(x, y);
    }

    public void StopTheBody()
    {
        SetVelocity(0, VelocityY);
    }

    #endregion

    public void SetGravity(float value)
    {
        _rigidbody2D.gravityScale = value;
    }

    public void SetLinerDrag(float value)
    {
        _rigidbody2D.drag = value;
    }
}

