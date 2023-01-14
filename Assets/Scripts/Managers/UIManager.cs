using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public GameObject toastCanvas;
    public TextMeshProUGUI text;
    [Space]
    public GameObject objectiveCanvas;
    void Awake()
    {
        //creating singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;

        }
        DontDestroyOnLoad(gameObject);

        BlankText();
    }

    public void DisableObjectiveCanvas()
    {
        objectiveCanvas.SetActive(false);
    }

    private void BlankText()
    {
        text.SetText("");
    }    
    public void BattleWon()
    {

        text.SetText("Battle Won");
        StartCoroutine(ClearAfterSeconds(2f));
    }

    public void GameWon()
    {
        text.SetText("You Win!");
        StartCoroutine(ClearAfterSeconds(10f));

    }

    public void GameLost()
    {
        text.SetText("You Lose");
        StartCoroutine(ClearAfterSeconds(10f));     
    }

    private IEnumerator ClearAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        BlankText();
    }
}
