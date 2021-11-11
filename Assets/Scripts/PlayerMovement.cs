using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed, jumpPower;
    public AnimatorController idle, run, jump, fall, attack, attackUp, attackDown, death;
    public GameObject attackPoint, attackPointUp, attackPointDown;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private int[] cooldowns = new int[4]; // 0 = jump, 1 = attack, 2 = attackUp, 3 = attackDown
    private bool _canJump;
    private bool _attacking;
    private enum Attacks
    {
        none,
        attackRight,
        attackLeft,
        attackUp, 
        attackDown
    };
    
    public void FinishedAttack()
    {
        _attacking = false;
    }

    private void Attack(GameObject point)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(point.transform.position, attackPoint.GetComponent<WireMap>().attackRadius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Attackable"))
            {
                hitCollider.gameObject.GetComponent<Attackable>().OnAttack(1);
            }
        }
    }
    
    public void Strike(string direction)
    {
        switch (direction)
        {
            case "up":
                Attack(attackPointUp);
                break;
            case "down":
                Attack(attackPointDown);
                break;
            case "straight":
                Attack(attackPoint);
                break;
        }
    }
    

    private void HandleAnimation(Vector2 velocity, Attacks attackType)
    {
        if (_attacking)
        {
            return;
        }
        if (attackType == Attacks.none)
        {
            if (velocity.y > 1) _animator.runtimeAnimatorController = jump;
            else if (velocity.y < -1) _animator.runtimeAnimatorController = fall;
            else if (velocity.x != 0) _animator.runtimeAnimatorController = run;
            else _animator.runtimeAnimatorController = idle;
            if (velocity.x < 0) gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            else if (velocity.x > 0) gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            _attacking = true;
            switch (attackType)
            {
                case Attacks.attackRight:
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _animator.runtimeAnimatorController = attack;
                    break;
                case Attacks.attackLeft:
                    gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    _animator.runtimeAnimatorController = attack;
                    break;
                case Attacks.attackUp:
                    _animator.runtimeAnimatorController = attackUp;
                    break;
                case Attacks.attackDown:
                    _animator.runtimeAnimatorController = attackDown;
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Count down cooldowns
        for (int i = 0; i < cooldowns.Length; i++)
        {
            if (cooldowns[i] > 0) cooldowns[i]--;
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float aX = Input.GetAxis("FireHorizontal");
        float aY = Input.GetAxis("FireVertical");
        //Debug.Log(x + " " + y + " " + aX + " " + aY);
        Vector2 oldVelocity = _rigidbody2D.velocity;
        float jumpSpeed = jumpPower;
        float moveSpeed = x * speed;
        if (!_canJump || cooldowns[0] > 0 || y <= 0) jumpSpeed = 0;
        else cooldowns[0] = 30;
        Vector2 newVelocity = new Vector2(moveSpeed, oldVelocity.y + jumpSpeed);
        _rigidbody2D.velocity = newVelocity;
        Attacks attackType = Attacks.none;
        if (aX != 0)
        {
            if (aX > 0) attackType = Attacks.attackRight;
            else attackType = Attacks.attackLeft;
        }
        else if (aY != 0)
        {
            if (aY > 0) attackType = Attacks.attackUp;
            else attackType = Attacks.attackDown;
        }
        HandleAnimation(newVelocity, attackType);
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
