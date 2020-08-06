using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    
    private Rigidbody2D _rigidbody;
    private bool _isGround;
    private float _speed = 3f;
    private Vector2 _jumpForce = new Vector2(0, 6);

    private int _coinCount = 0;

    public event UnityAction<int> CoinTaked;
    public event UnityAction PlayerDied;

    private void Start()
    {
         _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(new Vector3(_speed * Time.deltaTime, 0), Space.World);
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _isGround = false;
        _rigidbody.AddForce(_jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
            _isGround = true;
    }

    public void ResetPlayer()
    {
        _coinCount = 0;
        CoinTaked?.Invoke(_coinCount);
        transform.position = new Vector3(0, 1, 0);
    }

    public void Die()
    {
        PlayerDied?.Invoke();
    }

    public void TakeCoin()
    {
        _coinCount++;
        CoinTaked?.Invoke(_coinCount);
    }
}
