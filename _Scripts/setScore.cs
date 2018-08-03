using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class setScore : MonoBehaviour
{

    public static int score;
    Text text;
    int highScore = PlayerPrefs.GetInt("HighScore");

    // Use this for initialization
    void Awake()
    {

        text = GetComponent<Text>();
        score = PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    public void Update()
    {

        text.text = "Score: " + score;
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", setScore.score);
            PlayerPrefs.Save();
            //Color colorStart = GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            //GetComponent<SpriteRenderer>().color = colorStart;
            
        }

    }

    public int getscore()
    {
        return score;
    }

}
