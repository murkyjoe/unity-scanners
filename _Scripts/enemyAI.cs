using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour
{
    float delay = 2.0F;
    GameObject player;
    float distance;
    float wonderingTime = 5.1F;
    float modeTime = 5.1F;
    int ranDirectionx = 0;
    int ranDirectiony = 0;
    bool warned = false;
    bool warned2 = false;
    float warnedReset = 0.0F;
    Vector3 direction;
    int modePick;
    bool Switch = true;
    public AudioClip chaseSound, relaxSound;
    public static AudioSource source;
    float r = Random.Range(0.00f, 1.0f);
    float b = Random.Range(0.00f, 1.0f);
    float g = Random.Range(0.00f, 1.0f);
    

    // Use this for initialization
    void start()
    {
        Color colorStart = GetComponent<SpriteRenderer>().color = new Color(r, g, b, 1);
        GetComponent<SpriteRenderer>().color = colorStart;
        
   
    }


    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("player");
        float dist = Vector2.Distance(player.transform.position, transform.position);
        source = GetComponent<AudioSource>();

        if (dist > 70)
        {
            wonder();
            Switch = true;
        }
        else
        {
            modeTime += Time.deltaTime;

            if (modeTime > 5.0F)
            {
                modePick = (int)Random.Range(0, 4);
             

                modeTime = 0.0F;
            }
            
            if (modePick == 1)
            {
                if(modeTime < 0.1F && Switch == true) { source.PlayOneShot(chaseSound,1.0F); Switch = false; }
                reactToPlayer();
               

            }

            else
            {
                wonder();
                if (modeTime < 0.1F && Switch == false) { source.PlayOneShot(relaxSound, 1.0F); }
                Switch = true;
            }

        }

    }

    //Enemy wonder mode when far from player
    void wonder()
    {
        wonderingTime += Time.deltaTime;
        bool inBounds = true;
        GetComponent<SpriteRenderer>().color = new Color(r, g, b, 1);

        if (wonderingTime >= 5.0F)
        {
            do
            {
                inBounds = true;
                ranDirectionx = (int)Random.Range((transform.position.x - 50), transform.position.x + 50);
                ranDirectiony = (int)Random.Range((transform.position.y - 40), transform.position.y + 40);
                
                wonderingTime = 0.0F;

                if (ranDirectionx > 120 || ranDirectionx < -120 || ranDirectiony > 60 || ranDirectiony < -60)
                {
                    inBounds = false;

                }


            } while (inBounds == false);

        }

        direction = new Vector3(ranDirectionx, ranDirectiony, 0);
        transform.up = Vector3.Lerp(transform.up, (direction - transform.position), 0.2F * Time.deltaTime);
       
        if (wonderingTime >= 1.0F)
        {
            transform.position = Vector3.Lerp(transform.position, direction, 0.8F * Time.deltaTime);
        }
    }

    void reactToPlayer()
    {    
        warnedReset += Time.deltaTime;
        if(warnedReset > 13.0F) { warned = false; warnedReset = 0.0F; }
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
        if (warned == false && warnedReset < 0.1F) { StartCoroutine(warningTime()); }
        if (warned == true)
        {
            transform.up = Vector3.Lerp(transform.up, (player.transform.position - transform.position), 0.2F * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.8F * Time.deltaTime);
        }
      
    }
       
    
    //Detect if player enters scan collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            StartCoroutine(wait(other));
            
        }

    }

    //Kill player after 0.2 seconds
    IEnumerator wait(Collider2D other)
    {

        yield return new WaitForSeconds(0.1F);
        player.GetComponent<playerMovement>().death();
        player.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);

        //yield return new WaitForSeconds(2.0F);
        //Destroy(other.gameObject);
        
        
        //Application.LoadLevel("arena");
       
    }

    
    IEnumerator warningTime()
    {
       yield return new WaitForSeconds(0.5F);
       warned = true;


        

    }
    


}
