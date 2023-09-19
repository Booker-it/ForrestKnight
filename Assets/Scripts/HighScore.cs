
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public Knight knight;

    public FinishLine traguardo;

    public Timer timer;
    
    public TextMeshProUGUI scoreText;
    public float Score;

    public TextMeshProUGUI highscoreText;
    public float Highscore;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteKey("Highscore"); // per cancellare il valore dell'highscore all'avvio

        if (PlayerPrefs.HasKey("Highscore"))
        {
            Highscore = PlayerPrefs.GetFloat("Highscore");
        }
        else
            Highscore = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        highscoreText.text = "Best Time: " + Highscore.ToString();
        timer.DisplayTime(Highscore, highscoreText);
        
        scoreText.text = timer.timerText.text;
        Score = timer.timeValue;

        if (traguardo.hasSuccessfullyEnter)
        {
            Highscore = PlayerPrefs.GetFloat("Highscore");
            SaveHighScore();
            
        }       
    }

    public void SaveHighScore()
    {
        if( Score < PlayerPrefs.GetFloat("Highscore") || PlayerPrefs.GetFloat("Highscore") == 0 )
        {
            PlayerPrefs.SetFloat("Highscore", Score);
        }
    }
}
