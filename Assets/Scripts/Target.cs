using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private int pointValue = 0;

    [SerializeField]
    private ParticleSystem explosionParticles;

 
    private float minSpeed = 12;
    private float maxSpeed = 16;

    private float minTorque = 10;
    private float maxTorque = 10;

    private float spawnRangePosX = 4;
    private float spawnPosY = -2;

    private Rigidbody targetRb;
    private GameManager gameManager;

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(minTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-spawnRangePosX, spawnRangePosX), spawnPosY);
    }
}
