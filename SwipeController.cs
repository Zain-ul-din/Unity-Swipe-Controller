using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  USAGE:
     float x = TouchController.Instance.x;
     float y = TouchController.Instance.y;
     
*/

public class TouchController : MonoBehaviour
{
    [HideInInspector] public float x , y;
    public static TouchController Instance {get; private set;}
    
    [SerializeField]private float playerspeed = 13f;// Player Speed for X-Y axis

    // Swipe Var
    [SerializeField] private float MinSwipeDistance = 50f;// In -> px
    [SerializeField] private float MaxSwipeTime = 0.5f;// Max Time Requried to move 
    
    private float  SwipeTime;// Total Swipe Time
  
    // Swipe Time

     private float SwipeEndTime;// Time at Swipe End
     private float SwipeStartTime;// Time at Swipe Start
     private float swipelenght;// Lenght of Swipe
    // Swipe Pos

     private Vector2 StartSwipePos;// Swipe Start pos
     private Vector2 EndSwipePos;// End pos
    
    private void Awake ()
    {
      Instance = this;
    }
    
    void Update()
    {
        GetInput();// Geting Input for touch
    }

    private void GetInput ()
    {
       
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Takes 1st Touch by User
            if(touch.phase==TouchPhase.Began)// Toch starts 
            {
                SwipeStartTime = Time.time;
                StartSwipePos = touch.position;
                // Get Time & Position of touch
            }
            else if(touch.phase==TouchPhase.Ended)// ?? Touch ended
            {
                SwipeEndTime = Time.time;
                EndSwipePos = touch.position;
                // Get Time & pos where Touch ended

                SwipeTime = SwipeEndTime - SwipeStartTime;//Check how long User Swipe
                swipelenght = (EndSwipePos - StartSwipePos).magnitude;// Check Lenght
                
                if(SwipeTime < MaxSwipeTime && swipelenght > MinSwipeDistance)// Time & Distance 
                {
                    SwipeControl();
                }
            }
            else
            {
                 x = 0;
                 y = 0;
            }
        }
        else
        {
           x = 0;
           y = 0;
        }

    }// getinput <-

    void SwipeControl()
    {
       
        Vector2 Distance = EndSwipePos - StartSwipePos;// get pos of Touch 
        float x_Distance = Mathf.Abs(Distance.x);
        float Y_Distance = Mathf.Abs(Distance.y);
        // Abs return's Postive value

        // -> for x Movment
        if(x_Distance > Y_Distance)
        {
            if(Distance.x > 0)// Swipe Right
            {
                x = 1;
               // this.gameObject.transform.Translate(playerspeed * Time.deltaTime, 0, 0);
            }
            if(Distance.x<0)// Swipe Left
            {
               x = -1;
               // this.gameObject.transform.Translate(-playerspeed * Time.deltaTime, 0, 0);
            }       
        }

        // -> for Y Movement
        if(Y_Distance > x_Distance)
        {
            if(Distance.y > 0)// Swipe up
            {
               y = 1;
               // this.gameObject.transform.Translate(0, playerspeed * Time.deltaTime, 0);
            }
            if(Distance.y < 0)// Swipe Down
            {
               y = -1;
               // this.gameObject.transform.Translate(0, -playerspeed * Time.deltaTime, 0);
            }
        }
    }// <- Swipe control
}
