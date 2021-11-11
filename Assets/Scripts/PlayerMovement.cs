using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed, jumpPower;
    private Rigidbody2D _rigidbody2D;
    private int _jumpTimeout;
    private bool _canJump;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_jumpTimeout > 0) _jumpTimeout--;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float aX = Input.GetAxis("FireHorizontal");
        float aY = Input.GetAxis("FireVertical");
        //Debug.Log(x + " " + y + " " + aX + " " + aY);
        Vector2 oldVelocity = _rigidbody2D.velocity;
        float jumpSpeed = jumpPower;
        float moveSpeed = x * speed;
        if (!_canJump || _jumpTimeout > 0 || y <= 0) jumpSpeed = 0;
        else _jumpTimeout = 30;
        Vector2 newVelocity = new Vector2(moveSpeed, oldVelocity.y + jumpSpeed);
        _rigidbody2D.velocity = newVelocity;
         
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        _canJump = true;
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        _canJump = false;
    }
}
