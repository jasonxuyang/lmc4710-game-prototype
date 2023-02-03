using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 50.0f;
    public float rate = 3f;

    // Update Player 
    void Update()
    {
        GetMovement();
    }

    // Update Player Movement
    private void GetMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        //ORIGINAL SCRIPT FOR MOVEMENT
        //Vector2 movement = new Vector2(speed * inputX, speed * inputY);
        //movement *= Time.deltaTime;

        // Uniform Movement
        if (inputX != 0 && inputY != 0)
        {
            float mag = (float)(Math.Sqrt(Math.Pow(inputX, 2)) * Math.Sqrt(Math.Pow(inputY, 2)));
            float angle = (float)(Math.Acos(0 / mag) / 2);

            inputX *= (float)Math.Cos(angle);
            inputY *= (float)Math.Sin(angle);
        }

        Vector2 movement = new Vector2(inputX * speed, inputY * speed);
        movement *= Time.deltaTime / rate;

        transform.Translate(movement);
    }
}