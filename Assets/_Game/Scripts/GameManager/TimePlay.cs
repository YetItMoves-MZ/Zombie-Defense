using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimePlay : MonoBehaviour
{
    [SerializeField] Image pause;
    [SerializeField] Image play;
    [SerializeField] Image fast;
    [SerializeField] Image megaFast;

    [SerializeField] Color selectedColor;

    float fixedDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
        ChangeColorOfButton(play, selectedColor);
    }

    void ChangeColorOfButton(Image button, Color color)
    {
        button.color = color;
        for (int i = 0; i < button.transform.childCount; i++)
        {
            button.transform.GetChild(i).GetComponent<Image>().color = color;
        }
    }


    void DeselectAllButtons()
    {
        ChangeColorOfButton(pause, Color.white);
        ChangeColorOfButton(play, Color.white);
        ChangeColorOfButton(fast, Color.white);
        ChangeColorOfButton(megaFast, Color.white);
    }

    void OnButtonClick(Image button, float timeScale)
    {
        DeselectAllButtons();
        ChangeColorOfButton(button, selectedColor);
        Time.timeScale = timeScale;
    }
    public void OnPauseClick()
    {
        OnButtonClick(pause, 0.000001f);
        print(Time.timeScale);
    }
    public void OnPlayClick()
    {
        OnButtonClick(play, 1f);
    }
    public void OnFastClick()
    {
        OnButtonClick(fast, 2f);
    }
    public void OnMegaFastClick()
    {
        OnButtonClick(megaFast, 5f);
    }

}
