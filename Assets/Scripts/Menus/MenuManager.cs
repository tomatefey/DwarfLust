using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    SceneManager manager;

    float counter;
    public float logoScreenTime = 7.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevelName == "Logo")
        {
            counter += Time.deltaTime;

            if(counter >= logoScreenTime)
            {
                goToMainMenu();
            }
        }
    }

    public void goToGameplay()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void goToGroundLevel()
    {
        SceneManager.LoadScene("Ground_Level");
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
