using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausing : MonoBehaviour
{

    public static bool ispaused = false;

    public GameObject pause;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Time.timeScale = 0f;
            pause.SetActive(true);
            ispaused = true;
        }

    }

    public void resume()
    {
        ispaused = false;
        Time.timeScale = 1f;
        pause.SetActive(false);

    }
    public void restart()
    {
        ispaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

    }

    public void menu()
    {
        ispaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }
}
