using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class CanvasController : MonoBehaviour
{
    public GlobalInfo info;
    public Image bar, ship;
    public GameObject winScreen, defeatScreen;
    public RectTransform beggining, end;
    public Image cryoIcon, gravityIcon, propulsionIcon, gpsIcon, shieldIcon, monitoringIcon;
    public Sprite fireSprite, virusSprite, circuitSprite, brokenSprite, energiaSprite;
    public GameObject iconParent;
    public ModuleHazardStatus hazardStatus;
    public MusicController musicController;

    void Awake()
    {
        info.victory = false;
        info.defeat = false;
        info.progress = 0;
        info.shipSpeed = 0.02f;
        info.shipSpeedMultiplier = 1f;
        for (int i = 0; i < hazardStatus.status.Count; i++)
        {
            hazardStatus.status[i] = Hazard.Type.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = info.progress;
        ship.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(beggining.anchoredPosition.x,end.anchoredPosition.x,info.progress),
            ship.rectTransform.anchoredPosition.y);

        if(info.defeat)
        {
            if(Input.GetKey(KeyCode.R))
            {
                ButtonRetry();
            }
            if (Input.GetKey(KeyCode.M))
            {
                ButtonMenu();
            }
        }
        if (info.victory)
        {
            if (Input.GetKey(KeyCode.N))
            {
                ButtonNext();
            }
            if (Input.GetKey(KeyCode.M))
            {
                ButtonMenu();
            }
        }

        if (!info.victory)
        {
            ShipProgress();
            UpdateIcons();
            UpdateMusic();
            if (info.defeat && !defeatScreen.activeSelf)
            {
                defeatScreen.SetActive(true);
            }
        }

        if(info.progress >= 1)
        {
            Win();
        }


    }

    void ShipProgress()
    {
        float newProgress = info.progress + info.shipSpeedMultiplier * info.shipSpeed * Time.deltaTime;
        info.progress = Mathf.Clamp(newProgress, 0, 1);
    }

    void UpdateIcons()
    {
        CheckModule(ModuleEnum.cryo, cryoIcon);
        CheckModule(ModuleEnum.gravity, gravityIcon);
        CheckModule(ModuleEnum.propulsion, propulsionIcon);
        CheckModule(ModuleEnum.GPS, gpsIcon);
        CheckModule(ModuleEnum.shield, shieldIcon);
        CheckModule(ModuleEnum.monitoring, monitoringIcon);

        iconParent.SetActive(hazardStatus.status[hazardStatus.modules.IndexOf(ModuleEnum.monitoring)] == Hazard.Type.None);
    }

    void CheckModule(ModuleEnum module, Image icon)
    {

        icon.enabled = true;
        switch (hazardStatus.status[hazardStatus.modules.IndexOf(module)])
        {
            case Hazard.Type.Fire:
                icon.sprite = fireSprite;
                break;
            case Hazard.Type.Virus:
                icon.sprite = virusSprite;
                break;
            case Hazard.Type.ShortCircuit:
                icon.sprite = circuitSprite;
                break;
            case Hazard.Type.BrokenPiece:
                icon.sprite = brokenSprite;
                break;
            case Hazard.Type.LowEnergy:
                icon.sprite = energiaSprite;
                break;
            default:
                icon.enabled = false;
                break;
        }
    }

    void UpdateMusic()
    {
        int countHazards = 0;
        foreach (var status in hazardStatus.status)
        {
            if (status != Hazard.Type.None)
            {
                countHazards += 1;
            }
        }
        if (countHazards > 3)
        {
            musicController.SetFrenetic(true);
        }
        if (countHazards < 2)
        {
            musicController.SetFrenetic(false);
        }
    }

    public void Win()
    {
        winScreen.SetActive(true);
        info.victory = true;
    }

    public void Lose()
    {
        defeatScreen.SetActive(true);
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonNext()
    {
        info.currentStage++;
        SceneManager.LoadScene("Stage" + info.currentStage);
    }

    public void ButtonRetry()
    {
        SceneManager.LoadScene("Stage" + info.currentStage);
    }
}
