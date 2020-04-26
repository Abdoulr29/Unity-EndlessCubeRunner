using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rigidbody;
    public float forwardForce = 2000f;
    public float sidewayForce = 500f;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    // Start is called before the first frame update
    // void Start()
    // {
    //     rigidbody.AddForce(0,200,500); 
    // }

    // Update is called once per frame
    void FixedUpdate()
    {

        // add forward force
        rigidbody.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            rigidbody.AddForce(sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

        }

        if (Input.GetKey("a"))
        {
            rigidbody.AddForce(-sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

        }


        // if (Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

        //     if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        //     {
        //         // get the touch position from the screen touch to world point
        //         Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
        //         // lerp and set the position of the current object to that of the touch, but smoothly over time.
        //         transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
        //     }
        // }


        if (rigidbody.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }



        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            rigidbody.AddForce(sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            rigidbody.AddForce(-sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }


        }





    }
}
