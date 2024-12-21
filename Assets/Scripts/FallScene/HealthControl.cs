using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthControl : MonoBehaviour
{
    public GameObject[] hearts;
    public GameObject failScene;
    private Image[] heartsImage;
    [SerializeField] private int health;
    private float invincibilityTime = 1f; // 無敵時間
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI timer;
    private int currentTime;
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
        currentTime = int.Parse(timer.text);
        UpdateHeartImage(health);
    }
    void UpdateHeartImage(int health) {
        for (int i = 0; i < health; i++) {
            heartsImage[i].color = new Color(255f, 255f, 255f, 255f);
        }
        for (int i = heartsImage.Length-1; i > health-1; i--) {
            heartsImage[i].color = new Color(0f, 0f, 0f, 255f);
        }
        if (health == 0) {
            StartCoroutine(FadeIn());
        }
    }
    private IEnumerator FadeIn()
    {
        float fadeDuration = 2.0f;
        
        failScene.SetActive(true);

        
        Image image = failScene.GetComponent<Image>();
        Color color = image.color;

        
        color.a = 0;
        image.color = color;

        float elapsedTime = 0;

        while (elapsedTime < fadeDuration) //慢慢調整透明度
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration); 
            color.a = alpha; 
            image.color = color; 
            yield return null; 
        }

        
        color.a = 1;
        image.color = color;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FallingBlock"))
        {
            if (!isInvincible) // 無敵時間不受傷
            {
                if (health > 0 && currentTime > 0)
                {
                    health--;
                    StartCoroutine(HandleInvincibility());
                    Debug.Log("Character damaged. Health: " + health);
                }
            }
        }
        if (collision.gameObject.CompareTag("GoldenHeart"))
        {
            
            if (health < 3 && currentTime > 0)
            {
                 health++;
                 Debug.Log("Character damaged. Health: " + health);
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
    public int Gethealth() 
    {
        return health;
    }
}
