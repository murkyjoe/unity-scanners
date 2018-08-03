using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour
{

    public float moveSpeed;
    public Vector3 input;
    public Vector3 inputCalibrate;
    public Rigidbody2D ballMovement;
    float growRate = 1.0F;
    float scale = 1.0F;
    bool space;
    bool dead = false;
    bool shrink = false;
    float dodgeDelay = 5.1F;



    void Start()
    {

        ballMovement = GetComponent<Rigidbody2D>();
        inputCalibrate = new Vector3(Input.acceleration.x * 4, Input.acceleration.y * 4,0);

    }

    void FixedUpdate()
    {
        //Player Movement
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0F);
        transform.Translate(Input.acceleration.x *4, (Input.acceleration.y*4) - inputCalibrate.y, 0);

        dodgeDelay += Time.deltaTime;

        if(Input.GetKey(KeyCode.LeftAlt) && dodgeDelay > 3.0F && dead == false || Input.touchCount == 2 && dodgeDelay > 3.0F && dead == false)
        {
            moveSpeed = 1000;
            dodgeDelay = 0.0F;
        }

        else if(dodgeDelay > 0.1F && dodgeDelay < 5.0F && dead == false) { moveSpeed = 150; }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S)&& dead == false))
        {
            ballMovement.AddForce(input * moveSpeed);

        }

        if(dead == true)
        {
            transform.localScale = Vector3.one * -scale;
            scale = growRate * Time.deltaTime* 30;
            
        }

    }

    public void death()
    {
        dead = true;
        moveSpeed = 10;
        StartCoroutine(waitToDie());

    }

    IEnumerator waitToDie()
    {

        yield return new WaitForSeconds(2.0F);
        Destroy(this);

        Application.LoadLevel("arena");

    }

}




