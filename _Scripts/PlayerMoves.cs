using UnityEngine;
using System.Collections;

public class PlayerMoves : MonoBehaviour
{
    float growRate = 1.0F;
    float scale = 1.0F;
    bool space;
    bool shrink = false;
    GameObject enemy;
    bool enemyInRange;
    float attackDelay = 3.1F;
    float dodgeDelay = 2.1F;
    int highScore = PlayerPrefs.GetInt("HighScore");

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        //3 second attack cooldown
        attackDelay += Time.deltaTime;

        //Player attack anim
        if (attackDelay > 3.0F && Input.GetKey(KeyCode.Space) || (attackDelay > 3.0F && Input.touchCount >0) || space == true)
        {
            
            if (scale < 3.0F && shrink == false)
            {
                transform.localScale = Vector3.one * scale;
                scale += growRate * Time.deltaTime * 6;
               
                space = true;

            }
            else if (scale > 3.0F)
            {
                shrink = true;
            }

            if (scale > 1.0F && shrink == true)
            {
                transform.localScale = Vector3.one * scale;
                scale -= growRate * Time.deltaTime * 6;
                if (scale < 1.0F)
                {
                    transform.localScale = Vector3.one;
                    shrink = false;
                    space = false;
                    attackDelay = 0.0F;
                }
            }

        }

    }

    //Player kills enemy
    void OnTriggerEnter2D(Collider2D other)
    {
       
        //Checks that space has been pressed, and correct collider attacked
        if (other.tag == "enemy" && scale > 1.0F && other.GetType() == typeof(CircleCollider2D))
        {
           
           Destroy(other.gameObject);
           setScore.score += 10;
            

        }

    }


    

}
