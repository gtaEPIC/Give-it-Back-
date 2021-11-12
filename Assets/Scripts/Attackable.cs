using UnityEngine;

public class Attackable : MonoBehaviour
{
    public int health = 1;
    
    public void OnAttack(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
