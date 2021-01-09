using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public GameObject menuSet;

    private bool opActiveSelf = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&opActiveSelf ==false )
        {
            Time.timeScale = 0;
            menuSet.SetActive(true);
            opActiveSelf = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)&& opActiveSelf ==true) 
        {
            Time.timeScale = 1;
            menuSet.SetActive(false);
            opActiveSelf = false;
        }


    }

    public void OnClickRetBtn() 
    {
        Time.timeScale = 1;
        menuSet.SetActive(false);
        opActiveSelf = false;

    }
    public void OnClickResBtn() 
    {
        Time.timeScale = 1;
        menuSet.SetActive(false);
        opActiveSelf = false;
        SceneManager.LoadScene(0);
    }
}
