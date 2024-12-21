using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIcontrolTrain : MonoBehaviour
{
    public GameObject targetUI;
    private bool player1InContact = false; // 玩家1是否接觸物件
    private bool player2InContact = false; // 玩家2是否接觸物件
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (player1InContact||player2InContact){
            targetUI.SetActive(true);
        }
        else {
            targetUI.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            player1InContact = true;
        }
        else if(other.CompareTag("Dog"))
        {
            player2InContact = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cat"))
        {
            player1InContact = false;
        }
        else if (other.CompareTag("Dog"))
        {
            player2InContact = false;
        }
    }
}