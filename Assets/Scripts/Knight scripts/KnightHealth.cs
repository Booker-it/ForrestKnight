using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KnightHealth : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public Image healthBar;

    public bool hasTakenDamage;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health > maxHealth)
            health = maxHealth;

        if (health < 0)
            health = 0;

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hasTakenDamage = true;
    }

    public bool NoHealth()
    {
        return (health == 0 || health < 0);
    }

}
