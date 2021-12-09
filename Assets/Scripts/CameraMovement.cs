using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public GameObject newPlayers;
    private Transform cameraTransform;
    private float _dieTime;
    
    public GameObject spawnPoint;
    private GameObject _currentCheckPoint;

    private void Start()
    {
        cameraTransform = transform;
        _currentCheckPoint = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPosition = target.transform.position;
        cameraTransform.position = new Vector3(targetPosition.x, targetPosition.y, -10);
        if (target.GetComponent<Attackable>().health <= 0)
        {
            if (_dieTime == 0) _dieTime = Time.time;
            else if (_dieTime + 4 < Time.time)
            {
                _dieTime = 0;
                Destroy(target.GetComponent<Rigidbody2D>());
                Destroy(target.GetComponent<CapsuleCollider2D>());
                // Create new player from prefabs
                GameObject newPlayer = Instantiate(newPlayers, _currentCheckPoint.transform.position, Quaternion.identity);
                target = newPlayer;
            }
        }
    }
    
    public void SetRespawnPoint(GameObject checkpoint)
    {
        _currentCheckPoint = checkpoint;
    }
}
