using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanController : MonoBehaviour
{
    private Animator animator;
    public Sprite idleSprite;
    private SpriteRenderer spriteRenderer;

    private Collider2D currentFish;

    //public Color highlightColor = Color.yellow; 

    private bool isMoving = false; 

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        if (isMoving)
        {
            SetAnimationState(false, true, false); 
        }
        else if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            
            SetAnimationState(false, false, true);

            
            if (currentFish != null)
            {
                FindObjectOfType<GameManager>().AddPlayerScore();
                RemoveFish(currentFish.gameObject);
            }
        }
        else if (!isMoving)
        {
            SetAnimationState(true, false, false); 
        }
    }

    void SetAnimationState(bool isIdle, bool isRowing, bool isFishing)
    {
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isRowing", isRowing);
        animator.SetBool("isFishing", isFishing);

        if (isIdle)
        {
            spriteRenderer.sprite = idleSprite;
        }
    }

    void RemoveFish(GameObject fish)
    {
        Destroy(fish);
        Debug.Log("Peixe removido!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            currentFish = other;
            Debug.Log("Peixe na área!");

           
            /*SpriteRenderer fishRenderer = currentFish.GetComponent<SpriteRenderer>();
            if (fishRenderer != null)
            {
                fishRenderer.color = highlightColor;
            }*/
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            if (currentFish == other)
            {
                SpriteRenderer fishRenderer = currentFish.GetComponent<SpriteRenderer>();
                if (fishRenderer != null)
                {
                    fishRenderer.color = Color.white;
                }

                currentFish = null;
                Debug.Log("Peixe saiu da área!");
            }
        }
    }
}
