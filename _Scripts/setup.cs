using UnityEngine;
using System.Collections;

public class setup : MonoBehaviour {

    GameObject player;
    GameObject enemy;
    GameObject pause;
    GameObject menuStart;
    bool goodSpawn;
    Vector3 spawnPos;
    SpriteRenderer colour;
    int enemyCount;
    
    

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("player");
        enemy = (GameObject)Resources.Load("Prefabs/enemy", typeof(GameObject)) as GameObject;
        pause = GameObject.FindGameObjectWithTag("Finish");
        menuStart = GameObject.FindGameObjectWithTag("Respawn");
        pause.SetActive(false);
        enemyCount = 1;
        menuStart.SetActive(true);

    }
	
	// Update is called once per frame
	void FixedUpdate () {


       // if (Input.GetKey(KeyCode.Return))
       // {
            menuStart.SetActive(false);
            setScore.score = 0;
            Time.timeScale = 1;

        //}
      
        pause.SetActive(false);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemyCount = enemies.Length;


        if(Input.GetKey(KeyCode.Escape) && menuStart.activeSelf == false)
        {
            Time.timeScale = 0;
            pause.SetActive(true);
            

        }

        while (enemyCount < 7 && menuStart.activeSelf == false)
        {
            do
            {
                goodSpawn = true;
                spawnPos = new Vector2(Random.Range(-120, 120), Random.Range(-60, 60));
                float safeDist = Vector2.Distance(player.transform.position, spawnPos);

                for( int i = 0; i < enemyCount; i++)
                {
                        float safeEnemyDist = Vector2.Distance(enemies[i].transform.position, spawnPos);
                    
                        if (safeEnemyDist < 40)
                        {
                          goodSpawn = false;
                       }
                    

                }

                if (safeDist < 70)
                {
                    goodSpawn = false;
                }

               
            } while (goodSpawn == false);

            Instantiate(enemy, spawnPos, Quaternion.identity);
            enemyCount++;
        }

	
	}
 

    public void resumeGame()
    {
        Time.timeScale = 1.0F;
    }

    public void LoAndBeholdTheUserWantsToStopPlayingThisShit()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.Save();
        Application.Quit();

    }


    
}
