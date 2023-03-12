using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menufunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextscene()
    {
        SceneManager.LoadScene(1);
    }

    public void quitscene()
    {
        Application.Quit();
    }

    

}
