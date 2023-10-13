using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDamage : MonoBehaviour
{

    float maxHealth = 100.0f;
    float currentHealth;

    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        DamageCar();
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float relativeVel = collision.relativeVelocity.magnitude;
        float damage = relativeVel * 1f;

        currentHealth -= damage;
    }

    void DamageCar()
    {
        if (currentHealth <= 100.0f && currentHealth >= 80.0f)
        {
            spriteRenderer.sprite = spriteArray[0];
        }
        
        if (currentHealth > 60.0f && currentHealth < 80.0f)
        {
            spriteRenderer.sprite = spriteArray[1];
        }
        
        if (currentHealth > 40.0f && currentHealth < 60.0f)
        {
            spriteRenderer.sprite = spriteArray[2];
        }
       
        if (currentHealth > 20.0f && currentHealth < 40.0f)
        {
            spriteRenderer.sprite = spriteArray[3];
        }
            
        if (currentHealth > 0.0f && currentHealth < 20.0f)
        {
            spriteRenderer.sprite = spriteArray[4];
        }
            
    }
}
