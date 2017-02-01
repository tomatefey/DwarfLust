using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Orbit : MonoBehaviour {

    GameInputs input;
    HandleNearEnemies nearEnemies;
    PlayerController player;

    public enum states { MOVING, TARGETING}
    [Header("State")]
    public states camState = states.MOVING;

    [Header("Distance")]
    Transform playerTarget;
    Transform enemyTarget;
    Transform target;
    public float distance;
    public float desiredDist = 3;
    float modifiedDist;
    public float minDist = 2;
    public float maxDist = 7;

    [Header("Speed Rotation")]
    public float xSpeed = 120;
    public float ySpeed = 120;

    public float yMinLimit = -10;
    public float yMaxLimit = 20;
    private float test; 
    
    private float x;
    private float y;

    Quaternion rotation;
    Vector3 position;

    [Header("No Walls")]
    public bool isColliding;
    public float correctionSpeed = 0.5f;
    public float correctionSteps;

    private Vector3 angles;

    [Header("Targeting")]
    public bool isTargeting;
    private Vector3 enemyGO;
    public float turnSpeed;

    void Start()
    {
        angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTarget = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();

        input = GameObject.FindGameObjectWithTag("Player").GetComponent<GameInputs>();
        nearEnemies = GameObject.FindGameObjectWithTag("Player").GetComponent<HandleNearEnemies>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        distance = desiredDist;
    }

    void FixedUpdate()
    {
        if (input.camModeInput) isTargeting = !isTargeting;

        if (player.lockTarget)
        {

            Vector3 lookPos = nearEnemies.target;

            Vector3 lookDir = lookPos - playerTarget.position;

            lookDir.y = 0;

            Quaternion rot = Quaternion.LookRotation(lookDir);
            rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
            position = rotation * new Vector3(0.0f, 2.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;

        }
        else
        {
            target = playerTarget;

            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            DontCollide();
            rotation = Quaternion.Slerp(rotation, Quaternion.Euler(y, x, 0), Time.deltaTime * (turnSpeed * 4));
            position = rotation * new Vector3(0.0f, 2.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;

            DontCollide();


            isColliding = false;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public Vector2 MouseInput()
    {
        Vector2 input = new Vector2(x , y);
        return input;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag != "Player")isColliding = true;
    }

    void DontCollide()
    {
        if (isColliding)
        {
            distance -= correctionSpeed * Time.deltaTime;
            if (distance < 1.3f)
            {
                distance = 1.3f;
            }
        }
        if (!isColliding)
        {
            if (distance < desiredDist) distance += correctionSpeed * Time.deltaTime;

            if (distance < minDist)
            {
                distance = minDist;
            }
            if (distance > maxDist)
            {
                distance = maxDist;
            }
        }
    }

    Vector3 MidPoint(Vector3 v1, Vector3 v2)
    {
        return new Vector3((v1.x + v2.x)/2, (v1.y + v2.y) / 2, (v1.z + v2.z) / 2);
    }
    
}
