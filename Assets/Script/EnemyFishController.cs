using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyFishController : MonoBehaviour
{
    public float speed = 1f; 
    public float detectionRange = 5f; 
    public float eatCooldown = 1.5f; 
    private Transform target; 
    private Vector2 initialPosition; 
    public GameManager gameManager; 

    private bool isEating = false; 

    void Start()
    {
        initialPosition = new Vector2(0, -3); 
        transform.position = initialPosition;
    }

    void Update()
    {
        if (!isEating) 
        {
            FindClosestFish();
            MoveTowardsTarget();
        }
    }

    void FindClosestFish()
    {
        GameObject[] fishes = GameObject.FindGameObjectsWithTag("Fish");
        float closestDistance = detectionRange; 
        GameObject closestFish = null;

        foreach (GameObject fish in fishes)
        {
            float distance = Vector2.Distance(transform.position, fish.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestFish = fish;
            }
        }

        if (closestFish != null)
        {
            target = closestFish.transform;
        }
        else
        {
            target = null; 
        }
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish") && !isEating) 
        {
            Destroy(other.gameObject); 
            gameManager.AddEnemyScore(); 

            StartCoroutine(EatCooldown());
        }
    }

    System.Collections.IEnumerator EatCooldown()
    {
        isEating = true;
        yield return new WaitForSeconds(eatCooldown); 
        isEating = false; 
    }
}
