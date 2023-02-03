using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 50.0f;
    public float rate = 3f;

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        //ORIGINAL SCRIPT FOR MOVEMENT
        //Vector2 movement = new Vector2(speed * inputX, speed * inputY);
        //movement *= Time.deltaTime;

        
        // Uniform Movement
        float x;
        float y;
        if (inputX != 0 && inputY != 0)
        {
            float mag = (float)(Math.Sqrt(Math.Pow(inputX, 2)) * Math.Sqrt(Math.Pow(inputY, 2)));
            float angle = (float)(Math.Acos(0 / mag) / 2);

            x = (float)(Math.Cos(angle) * inputX);
            y = (float)(Math.Sin(angle) * inputY);
        }
        else
        {
            x = inputX; y = inputY;
        }

        Vector2 movement = new Vector2(x * speed, y * speed);
        movement *= Time.deltaTime / rate;

        transform.Translate(movement);
    }
}