using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField]int health = 1;
    [SerializeField] float speed=1;
    [SerializeField] int valorEnemigo=100;

    Vector2 direction; 
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }
    private void Update()
    {
        direction = player.position - transform.position;
        transform.position += (Vector3)direction * Time.deltaTime*speed;
        
    }
    public void TakeDamage() {
        health--;
        if (health <= 0)
        {
            GameManager.Instance.Score += valorEnemigo;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage();

        

        }
    }
}
