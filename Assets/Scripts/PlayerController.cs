using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] int totalJumps = 1;
    
    private Rigidbody2D _rb;
    private float _moveHorizontal;
    private int _jumpCount;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpCount = totalJumps;
    }

    void Update()
    {
        _rb.linearVelocity = new Vector2(_moveHorizontal * moveSpeed, _rb.linearVelocity.y);
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
        Debug.Log("Collision!");
        if (collision.gameObject.CompareTag("Platform"))
            _jumpCount = totalJumps;
    }
}
