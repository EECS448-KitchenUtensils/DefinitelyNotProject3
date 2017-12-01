using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinButtonBehaviour : MonoBehaviour
{

    /// <summary>
    /// Start the game in local mode.
    /// </summary>
    public void GoToMainSceneLocal()
    {
        SceneManager.LoadScene("main_scene");
        PlayerPrefs.SetInt("local", 1);
    }

    /// <summary>
    /// Start the game in networked mode.
    /// </summary>
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
