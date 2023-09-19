using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{

    public KnightHealth playerHealth;
    public float damageEnter;
    //public float damageStay;

    [Header("Sound Effect")]
    [SerializeField] private AudioSource dmgSound;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dmgSound.Play();
            playerHealth.TakeDamage(damageEnter);
        }
    }

     //private void OnTriggerStay2D(Collider2D collision)
     //{
     //    if (collision.gameObject.CompareTag("Player"))
     //        playerHealth.TakeDamage(damageStay);
     //}
}
