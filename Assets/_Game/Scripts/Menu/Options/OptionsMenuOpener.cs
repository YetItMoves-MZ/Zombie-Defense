using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuOpener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("OptionsMenu"))
        {
            if (!SceneManager.GetSceneByBuildIndex(3).isLoaded)
                SceneManager.LoadScene(3, LoadSceneMode.Additive);
            else
                SceneManager.UnloadSceneAsync(3);
        }
    }
}
