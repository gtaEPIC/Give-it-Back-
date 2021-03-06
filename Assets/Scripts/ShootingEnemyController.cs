using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{
    public float arrowX, arrowY, fireRate, firePower;
    public GameObject arrow;
    public AudioSource arrowSFX;
    public GameObject range;
    private float _nextFire;
    
    // Start is called before the first frame update
    void Start()
    {
        _nextFire = fireRate;
    }

    bool InRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(range.transform.position, range.GetComponent<WireMap>().attackRadius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player")) return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time + "/" + nextFire);
        if (_nextFire <= Time.time)
        {
            if (!InRange()) return;
            Debug.Log("Firing");
            arrowSFX.Play();
            _nextFire = Time.time + fireRate;
            Vector2 position = transform.position;
            GameObject arrow = Instantiate(this.arrow, new Vector2(position.x + arrowX, position.y + arrowY),
                Quaternion.identity);
            float fireDirection = firePower;
            if (transform.rotation.y == 0) fireDirection *= -1;
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(fireDirection, 0);
        }
    }
}
