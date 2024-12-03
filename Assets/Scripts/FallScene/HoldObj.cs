using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HoldObj : MonoBehaviour
{
    public GameObject dog;
    public GameObject cat;
    private bool isHolding = false;
    private bool player2KeyPressed = false;
    private string holder = "None";
    private Vector3 objPosition;
    private Vector3 dogPosition;
    private Vector3 holdingPosition;
    private Rigidbody2D dogRigid;
    private Rigidbody2D catRigid;
    // Start is called before the first frame update
    void Start()
    {
        objPosition = transform.position;
        dogRigid = dog.GetComponent<Rigidbody2D>();
        catRigid = cat.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        dogPosition = dog.transform.position;
        Debug.Log(isHolding + " " + holder);
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        holder = collision.name;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        holder = "None";
    }
    public void dogInteraction(InputAction.CallbackContext context)
    {
        if (context.started)  // 當按下按鍵時
        {
            Debug.Log("Here");
            if (holder == "Dog" && isHolding == false)
            {
                isHolding = true;
                transform.position = dogPosition + new Vector3(0,0.9f,0);
                dogRigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            }
            else if (holder == "Dog" && isHolding == true) {
                isHolding = false;
                transform.position = objPosition;
                dogRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        if (context.canceled)  // 當放開按鍵時
        {
        }
    }
    public void catInteraction(InputAction.CallbackContext context)
    {
        if (context.started)  // 當按下按鍵時
        {
            player2KeyPressed = true;
        }

        if (context.canceled)  // 當放開按鍵時
        {
            player2KeyPressed = false;
        }
    }
}
