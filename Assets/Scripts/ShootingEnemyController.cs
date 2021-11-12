using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{
    public float arrowX, arrowY, fireRate, firePower;
    public GameObject arrow;
    private float _nextFire;
    
    // Start is called before the first frame update
    void Start()
    {
        _nextFire = 30 * fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time + "/" + nextFire);
        if (_nextFire == 0)
        {
            Debug.Log("Firing");
            _nextFire = fireRate * 30;
            Vector2 position = transform.position;
            GameObject arrow = Instantiate(this.arrow, new Vector2(position.x + arrowX, position.y + arrowY),
                Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(firePower * -1, 0);
        }
        else _nextFire--;
    }
}
