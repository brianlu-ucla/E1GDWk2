using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] int totalJumps = 1;
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    private float _moveHorizontal;
    private int _jumpCount;
    private bool _isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _jumpCount = totalJumps;
    }

    void Update()
    {
        _rb.linearVelocity = new Vector2(_moveHorizontal * moveSpeed, _rb.linearVelocity.y);
        _animator.SetBool("isMoving", _moveHorizontal != 0);
        if (transform.localScale.x * _moveHorizontal < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
    
    void OnMove(InputValue value)
    {
        var v = value.Get<float>();
        _moveHorizontal = v;
    }

    void OnJump()
    {
        if (_jumpCount > 0)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            _jumpCount--;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Platform")) return;
        
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (!_isGrounded && contact.normal.y > 0.5f)
            {
                Debug.Log("Grounded on Platform!");
                _jumpCount = totalJumps;
                _isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (totalJumps == _jumpCount) _jumpCount--;
        _isGrounded = false;
    }

    public void IncrementTotalJumps()
    {
        totalJumps++;
        _jumpCount = totalJumps;
        Debug.Log("Total Jumps: " + totalJumps);
    } 
}
