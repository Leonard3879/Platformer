using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMover : MonoBehaviour
{
public Transform startMarker;
public Transform endMarker;

public Vector2 startPos;
public Vector2 endPos;


public float speed = 1.0f;

private float startTime;
private float journeyLength;
private bool facingRight = true;

void Start()
{
    startTime = Time.time;
    journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
}

void FixedUpdate()
{
}

void Update()
    {
        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));


    }
   

public float getXAxis()
{
    return transform.position.x;
   
}

void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

}
