using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinButtonBehaviour : MonoBehaviour
{

    //Switch Scene
    public void GoToMainSceneLocal()
    {
        SceneManager.LoadScene("main_scene");
        PlayerPrefs.SetInt("local", 1);
    }

    //Switch Scene
    public void GoToMainSceneOnline()
    {
        SceneManager.LoadScene("main_scene");
        PlayerPrefs.SetInt("local", 0);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
