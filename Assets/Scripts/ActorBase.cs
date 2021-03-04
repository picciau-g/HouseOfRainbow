using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBase : MonoBehaviour
{
    protected float moveSpeed;
    protected bool isRunning;
    [SerializeField]
    protected int maxSightDistance;
    [SerializeField]
    protected double viewAngle;

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

    protected bool PlayerInSight()
    {
        GameObject pObject = GameObject.FindGameObjectWithTag("Player");
        if (pObject == null)
            return false;

        Vector3 distToPlayer = (pObject.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, distToPlayer);
        
        if (angle > viewAngle / 2)
            return false;
        LayerMask msk = LayerMask.GetMask("PlayerLayer");

        if (Physics.Raycast(transform.position, distToPlayer, maxSightDistance, msk))
        {
            Debug.Log("Player in Sight!");
            return true;
        }
        return false;
    }
}
