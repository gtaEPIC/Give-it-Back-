using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed, jumpPower;
    public RuntimeAnimatorController idle, run, jump, fall, attack, attackUp, attackDown, death;
    public GameObject attackPoint, attackPointUp, attackPointDown, groundDetector;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private int[] cooldowns = new int[4]; // 0 = jump, 1 = attack, 2 = attackUp, 3 = attackDown
    private bool _canJump, _attacking, _doubleJumped; // _canJump is true if the player is on the ground or falling,
                                                      // _attacking is true if the player is attacking
                                                      // _doubleJumped is true if the player has double jumped
    private int _touches;
    
    private CameraMovement _cameraMovement;
    
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
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(point.transform.position, point.GetComponent<WireMap>().attackRadius);
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
        _cameraMovement = GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>();
        cooldowns[0] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float aX = Input.GetAxis("FireHorizontal");
        float aY = Input.GetAxis("FireVertical");
        Vector2 oldVelocity = _rigidbody2D.velocity;
        if (oldVelocity.y <= 1) _canJump = true; // Player MAY be able to jump while they are falling
        else _canJump = false;
        if (_touches > 0) _doubleJumped = false;
        float jumpSpeed = 0;
        float moveSpeed = x * speed;
        //Debug.Log(oldVelocity.y + " " + y + " " + grounded + " " + _doubleJumped);
        // Handle jumps and double jumps
        if (y > 0 && _canJump && !_doubleJumped && _touches > 0)
        {
            jumpSpeed = jumpPower;
        }
        else if (y > 0 && _canJump && !_doubleJumped)
        {
            jumpSpeed = oldVelocity.y * -1 + jumpPower;
            _doubleJumped = true;
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Respawn"))
        {
            _cameraMovement.SetRespawnPoint(other.gameObject);
        }else if (other.CompareTag("Ground"))
        {
            _touches++;
            _doubleJumped = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _touches--;
        }
    }
}
