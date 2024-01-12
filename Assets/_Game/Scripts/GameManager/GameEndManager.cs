using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public TMPro.TMP_Text Text;
    public UIFadeAnimation fadeAnimation;
    // Start is called before the first frame update
    void Start()
    {
        //TODO get string from GameManager
        Text.text = ScoreManagement.GameEndedMessage;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeAnimation.IsFinished)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
