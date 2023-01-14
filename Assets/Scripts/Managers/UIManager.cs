using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public GameObject canvas;
    public TextMeshProUGUI text;
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
        BlankText();

    }

    public void GameLost()
    {
        text.SetText("You Lose");
        StartCoroutine(ClearAfterSeconds(10f));
        BlankText();
    }

    private IEnumerator ClearAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        BlankText();
    }
}
