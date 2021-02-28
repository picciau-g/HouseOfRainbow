using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorRotator : ActorBase
{
    [SerializeField]
    float rotationSpeed = 2.0f;
    [SerializeField]
    int rotationDeltaTime = 5;
    float currentTime;
    int[] xCoefficients;
    int[] zCoefficients;

    int currentArrayIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0.0f;
        currentArrayIndex = 0;
        moveSpeed = 2.0f;
        isRunning = false;

        xCoefficients = new int[4] {1, 0, -1, 0};
        zCoefficients = new int[4] {0, 1, 0, -1};

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= rotationDeltaTime)
        {
            currentTime = 0.0f;
            Debug.Log("Rotate " + currentArrayIndex);
            StartCoroutine(RotateActor(Vector3.up, 90, 1.0f));
        }
        else
        {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }

        processKeyboardInput();
    }

    IEnumerator RotateActor(Vector3 pAxis, float pAngle, float pDuration = 1.0f)
    {

        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;

        to *= Quaternion.Euler(pAxis*pAngle);

        float elapsed = 0.0f;

        while(elapsed < pDuration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.transform.rotation = to;

        // currentArrayIndex = (currentArrayIndex+1)%4;
    }
}
