using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public GameObject toastCanvas;
    public TextMeshProUGUI toastText;
    [Space]
    public GameObject objectiveCanvas;

    public GameObject bossesCanvas;
    public TextMeshProUGUI bossNumText;
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

    private void Update()
    {
        bossNumText.SetText("Remaining Bosses: "+GameManager.instance.GetBossesRemaining().ToString());
    }

    public void DisableObjectiveCanvas()
    {
        objectiveCanvas.SetActive(false);
    }

    private void BlankText()
    {
        toastText.SetText("");
    }    
    public void BattleWon()
    {

        toastText.SetText("Battle Won");
        StartCoroutine(ClearAfterSeconds(2f));
    }

    public void GameWon()
    {
        toastText.SetText("You Win!");
        StartCoroutine(ClearAfterSeconds(10f));

    }

    public void GameLost()
    {
        toastText.SetText("You Lose");
        StartCoroutine(ClearAfterSeconds(10f));     
    }

    private IEnumerator ClearAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        BlankText();
    }
}
