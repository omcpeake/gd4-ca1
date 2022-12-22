using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject battleCam;

    private CamState currentCam;

    public static CamManager instance { get; private set; }
    // Start is called before the first frame update

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
    }

    public void SetMainCam()
    {
        
        mainCam.SetActive(true);
        battleCam.SetActive(false);
        currentCam = CamState.MAINCAM;
    }

    public void SetBattleCam()
    {
        mainCam.SetActive(false);
        battleCam.SetActive(true);
        currentCam= CamState.BATTLECAM;
    }

    public CamState getCurrentCam()
    {
        return currentCam;
    }

}
