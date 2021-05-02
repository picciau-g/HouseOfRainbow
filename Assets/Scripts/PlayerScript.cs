using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float _normalPlayerSpeed;
    [SerializeField] float _hurtPlayerSpeed;
    [SerializeField] float _runPlayerSpeed;

    private float _currSpeed;
    private Vector3 _playerOrientation; //needed for the avatar

    void Start()
    {
        _normalPlayerSpeed = 2.5f;
        _hurtPlayerSpeed = 1.5f;
        _runPlayerSpeed = 4.0f;

        _playerOrientation = new Vector3(0, 1, 0);

        _currSpeed = _normalPlayerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessKeyboardInput();
    }


    void ProcessKeyboardInput()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 dirOfMov = -transform.right; 
            dirOfMov *= Time.deltaTime;

            transform.Translate(dirOfMov);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 dirOfMov = transform.right;
            dirOfMov *= Time.deltaTime;

            transform.Translate(dirOfMov);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 dirOfMov = transform.forward;
            dirOfMov *= Time.deltaTime;

            transform.Translate(dirOfMov);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 dirOfMov = -transform.forward;
            dirOfMov *= Time.deltaTime;

            transform.Translate(dirOfMov);
        }
    }
}
