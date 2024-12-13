using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthControl : MonoBehaviour
{
    public GameObject[] hearts;
    private Image[] heartsImage;
    private int health;
    private float invincibilityTime = 1f; // 無敵時間
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 3;
        heartsImage = new Image[hearts.Length];

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null) 
            {
                heartsImage[i] = hearts[i].GetComponent<Image>();
            }
            else
            {
                Debug.LogError($"Heart GameObject at index {i} is null.");
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        UpdateHeartImage(health);
    }
    void UpdateHeartImage(int health) {
        for (int i = 0; i < health; i++) {
            heartsImage[i].color = new Color(255f, 255f, 255f, 255f);
        }
        for (int i = heartsImage.Length-1; i > health-1; i--) {
            heartsImage[i].color = new Color(0f, 0f, 0f, 255f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FallingBlock"))
        {
            if (!isInvincible) // 無敵時間不受傷
            {
                if (health > 0)
                {
                    health--;
                    StartCoroutine(HandleInvincibility());
                    Debug.Log("Character damaged. Health: " + health);
                }
            }
        }
    }
    private IEnumerator HandleInvincibility()
    {
        isInvincible = true;

        float elapsedTime = 0f;

        while (elapsedTime < invincibilityTime) //無敵時間有閃爍效果
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(0.15f); 
            elapsedTime += 0.1f;
        }

        // 確認最後角色是可見的
        spriteRenderer.enabled = true;
        isInvincible = false;
    }
}
