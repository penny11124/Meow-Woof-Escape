using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMovement : MonoBehaviour
{
    public float speed = 2.0f; // Movement speed
    private Rigidbody2D rb;
    private bool isFaceCat = true;
    public GameObject catFish;
    public GameObject catBone;
    public GameObject dogFish;
    public GameObject dogBone;
    private string catHoldFish;
    private string catHoldBone;
    private string dogHoldFish;
    private string dogHoldBone;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeBoolRandomly());
    }

    // Update is called once per frame
    void Update()
    {
        catHoldFish = catFish.GetComponent<HoldObj>().catHold;
        catHoldBone = catBone.GetComponent<HoldObj>().catHold;
        dogHoldFish = dogFish.GetComponent<HoldObj>().dogHold;
        dogHoldBone = dogBone.GetComponent<HoldObj>().dogHold;
        //Debug.Log(catHoldFish +" " +catHoldBone);
        if (isFaceCat == true)
        {
            spriteRenderer.flipX = true;
            if (catHoldFish == "fish" && catHoldBone == "None")
            {
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            }
            else if (catHoldFish == "None" && catHoldBone == "bone")
            {
                rb.velocity = new Vector2(1 * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else {
            spriteRenderer.flipX = false;
            if (dogHoldFish == "fish" && dogHoldBone == "None")
            {
                rb.velocity = new Vector2(1 * speed, rb.velocity.y);
            }
            else if (dogHoldFish == "None" && dogHoldBone == "bone")
            {
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    private IEnumerator ChangeBoolRandomly()
    {
        while (true) 
        {
            float waitTime = Random.Range(5.0f, 10.0f); 
            yield return new WaitForSeconds(waitTime); 

            isFaceCat = !isFaceCat; // Âà¦V
        }
    }
}
