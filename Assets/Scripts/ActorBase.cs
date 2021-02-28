using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBase : MonoBehaviour
{
    protected float moveSpeed;
    protected bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2.0f;
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        processKeyboardInput();
    }


    protected void WalkToRun()
    {
        isRunning = !isRunning;

        if (isRunning)
            moveSpeed = 4.0f;
        else
            moveSpeed = 2.0f;
    }

    protected void processKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            WalkToRun();
            print("Walk to Run");
        }
    }
}
