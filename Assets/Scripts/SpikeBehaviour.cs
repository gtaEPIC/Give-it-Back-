using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{

    public float timeAllowedOnSpike;
    private float _timeOnSpike;
    private Attackable _playerHealth;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_playerHealth != null && Time.time - _timeOnSpike > timeAllowedOnSpike)
        {
            _playerHealth.OnAttack(1);
        }
    }
    
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _timeOnSpike = Time.time;
            _playerHealth = collision.gameObject.GetComponent<Attackable>();
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth = null;
        }
    }
}
