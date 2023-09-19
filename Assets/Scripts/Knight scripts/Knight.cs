
using System.Collections;
using UnityEngine;


public class Knight : MonoBehaviour
{
    [Header("Character Stuff")]
    [SerializeField] bool noBlood = false;

    [Header("Health stuff")]
    public KnightHealth knightHealth;
    public bool isDead;

    private Rigidbody2D rb2;
    private Animator animator;

    [Header("Game stuff")]
    public GameManagerScript gameManager;

    public bool IsFacingRight { get; set; } = true;


    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //DEATH
        if (knightHealth.NoHealth() && !isDead)
        {
            isDead = true;
            animator.SetBool("noBlood", noBlood);
            animator.SetTrigger("Death");
            rb2.bodyType = RigidbodyType2D.Static;
            StartCoroutine(GameOverScreenDelayed());
           // gameManager.gameOver();

        }

        //HURT
        else if (knightHealth.hasTakenDamage && !isDead)
        {
            animator.SetTrigger("Hurt");
            knightHealth.hasTakenDamage = false;
        }        
    }

    private IEnumerator GameOverScreenDelayed()
    {
        yield return new WaitForSeconds(1);
        gameManager.GameOver();
    }
}
