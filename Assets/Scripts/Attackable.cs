using UnityEngine;

public class Attackable : MonoBehaviour
{
    public int health = 1;
    public bool destroyOnDeath = true;
    public RuntimeAnimatorController damaged, death;
    public float invincibleTime = 0.5f;
    private float _timeStruck;
    private Animator _animator;
    public AudioSource deathAudio;
    public AudioSource enemyAudio;

    private void Start()
    {
        if (damaged == null && death == null) return;
        _animator = GetComponent<Animator>();
    }
    
    public void OnAttack(int damage)
    {
        if (Time.time - _timeStruck < invincibleTime) return;
        _timeStruck = Time.time;
        health -= damage;
        if (health <= 0)
        {
            if (death != null)
            {
                if (gameObject.CompareTag("Player"))
                {
                    gameObject.GetComponent<PlayerMovement>().enabled = false;
                    gameObject.GetComponent<Collider2D>().sharedMaterial = null;
                }
                _animator.runtimeAnimatorController = death;
                deathAudio.Play();
            }
            else
            {
                Destroy(gameObject);
                enemyAudio.Play();
            }
        }
        else
        {
            if (damaged != null)
            {
                _animator.runtimeAnimatorController = damaged;
            }
        }
    }

    public void FinishAnimation()
    {
        Destroy(gameObject);
    }
}
