using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputs : MonoBehaviour{

    public float maxTime = 0.1f;

    bool PS4_Controller = false;
    PlayerController player;

    [Header("GOD MODE")]
    public KeyCode godMode_Keyboard = KeyCode.F10;
    public KeyCode godMode_Joystick = KeyCode.JoystickButton9;
    public bool godMode;

    [Header("CAMERA MODE")]
    public KeyCode changeCameraMode_Keyboard = KeyCode.Y;
    public KeyCode changeCameraMode_Joystick = KeyCode.JoystickButton9;
    public bool camModeInput;
    float counterCamModeInput = 0;

    [Header("PAUSE")]
    public KeyCode pause_Keyboard = KeyCode.Escape;
    public KeyCode pause_Joystick = KeyCode.JoystickButton7;
    public bool pauseInput;
    GameObject pauseUI;

    [Header("POTION")]
    public KeyCode potion_Keyboard = KeyCode.Escape;
    public KeyCode potion_Joystick = KeyCode.JoystickButton3;
    public bool potionInput;

    [Header("CHANGE TARGET")]
    public string changeTarget_Keyboard = ("Mouse ScrollWheel");
    public string changeTarget_Joystick = ("Mouse X");
    public bool changeTargetInput;
    public float counterChangeTargetInput;

    [Header("MELEE ATTACK")]
    public string meleeAttack_Axis = ("Fire2");
    public bool meleeAttackInput;

    [Header("DODGE | RUN")]
    public KeyCode run_Keyboard = KeyCode.LeftShift;
    public KeyCode dodgeRun_Joystick = KeyCode.JoystickButton1;
    public KeyCode dodge_Keyboard = KeyCode.LeftAlt;
    public bool runInput;
    public bool dodgeInput;
    float counterRunInput = 0;

    [Header("MOVEMENT")]
    public string move_Horizontal = ("Horizontal");
    public string move_Vertical = ("Vertical");
    public bool isMoving;

    public float horizontal;
    public float vertical;

    // Use this for initialization
    void Start () {
        player = GetComponent<PlayerController>();

        pauseUI = GameObject.FindGameObjectWithTag("Pause_UI");

        camModeInput = false;
        isMoving = false;
        runInput = false;
        dodgeInput = false;
    }
	
	// Update is called once per frame
	void Update () {
        CamModeUpdate();
        RunUpdate();
        ChangeTargetUpdate();
        MoveUpdate();
        MeleeAttackUpdate();
        PauseUpdate();
        GodModeUpdate();
        PotionUpdate();
	}

    void CamModeUpdate()
    {
        if (Input.GetKey(changeCameraMode_Keyboard) || Input.GetKey(changeCameraMode_Joystick))
        {
            counterCamModeInput += Time.deltaTime;

        }
        else if ((Input.GetKeyUp(changeCameraMode_Keyboard) || Input.GetKeyUp(changeCameraMode_Joystick)) && counterCamModeInput > maxTime)
        {
            counterCamModeInput = 0;
            camModeInput = true;
        }
        else
        {
            counterCamModeInput = 0;
            camModeInput = false;
        }
    }

    void IsControllerConnected()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length > 0)
            {
                PS4_Controller = true;
            }
            else
            {
                PS4_Controller = false;
            }
        }
    }

    void RunUpdate()
    {
        IsControllerConnected();

        if (PS4_Controller)
        {
            if (Input.GetKey(dodgeRun_Joystick))
            {
                counterRunInput += Time.deltaTime;
                if (counterRunInput > maxTime)
                {
                    runInput = true;
                }
            }
            else if (Input.GetKeyUp(dodgeRun_Joystick) && counterRunInput > 0 && !runInput)
            {
                dodgeInput = true;
                runInput = false;
                counterRunInput = 0;
            }
            else 
            {
                runInput = false;
                dodgeInput = false;
                counterRunInput = 0;
            }
        }
        else
        {
            if (Input.GetKey(run_Keyboard))
            {
                runInput = true;
            }
            else
            {
                runInput = false;
            }

            if (Input.GetKey(dodge_Keyboard))
            {
                dodgeInput = true;
            }
            else
            {
                dodgeInput = false;
            }
        }

        
    }

    void MoveUpdate()
    {
        horizontal = Input.GetAxis(move_Horizontal);
        vertical = Input.GetAxis(move_Vertical);

        if((-0.1 < horizontal && horizontal < 0.1f) && (-0.1 < vertical && vertical < 0.1f))
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
    
    void MeleeAttackUpdate()
    {
        if (Input.GetAxis(meleeAttack_Axis) != 0)
        {
            meleeAttackInput = true;
        }
        else
        {
            meleeAttackInput = false;
        }
    }

    void ChangeTargetUpdate()
    {
        if (PS4_Controller)
        {
            if (Input.GetAxis(changeTarget_Joystick) != 0 && !changeTargetInput)
            {
                counterChangeTargetInput += Time.deltaTime;
                if (counterChangeTargetInput > 0.8f)
                {
                    counterChangeTargetInput = 0;

                    changeTargetInput = true;
                }  
            }
            else
            {
                counterChangeTargetInput = 0;
                changeTargetInput = false;
            }
        }
        else
        {
            if (Input.GetAxisRaw(changeTarget_Keyboard) != 0)
            {
                changeTargetInput = true;
            }
            else
            {
                changeTargetInput = false;
            }
        }

    }

    void GodModeUpdate()
    {
        if(Input.GetKeyUp(godMode_Keyboard) || Input.GetKeyUp(godMode_Joystick))
        {
            godMode = !godMode;
        }
    }

    void PauseUpdate()
    {
        if (Input.GetKeyDown(pause_Keyboard) || Input.GetKeyDown(pause_Joystick))
        {
            pauseInput = !pauseInput;
        }

        if (pauseInput)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    void PotionUpdate()
    {
        if ((Input.GetKeyUp(potion_Keyboard) || Input.GetKeyUp(potion_Joystick)))
        {
            potionInput = true;
        }
        else
        {
            potionInput = false;
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        pauseInput = false;
    }
}
