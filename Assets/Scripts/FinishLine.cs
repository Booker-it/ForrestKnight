
using UnityEngine;


public class FinishLine : MonoBehaviour
{
    public bool hasSuccessfullyEnter;

    public GameManagerScript gameManager;

    [Header("Audio")]
    [SerializeField] private AudioSource finishSound;

    // Start is called before the first frame update
    void Start()
    {
        hasSuccessfullyEnter = false;
    }

    // Update is called once per frame


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            finishSound.Play();
            hasSuccessfullyEnter = true;
            gameManager.FinishGame();
            //SceneManager.LoadScene("Main Menu");
        }
    }
}
