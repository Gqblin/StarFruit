using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject stationUI;
    [SerializeField] private GameObject dateUI;
    [SerializeField] private GameObject datingUI;
    [SerializeField] private GameObject doneButton;
    [SerializeField] private GameObject bell;
    [SerializeField] private GameObject receipt;
    [SerializeField] private List<Image> receiptImages = new List<Image>();

    [SerializeField] private TextMeshProUGUI option1Text;
    [SerializeField] private TextMeshProUGUI option2Text;

    [SerializeField] private GameObject nextDayScreen;
    [SerializeField] private TMP_Text nextDayText;

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private GameObject creditScreen;

    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private GameObject endScreen;

    [SerializeField] private VideoPlayer vidPlayer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public void SetReceiptImage(int index, Sprite sprite)
    {
        receiptImages[index].gameObject.SetActive(true);
        receiptImages[index].sprite = sprite;
    }

    public void DeleteReceipt()
    {
        for(int i = 0; i < receiptImages.Count; i++)
        {
            receiptImages[i].gameObject.SetActive(false);
        }
    }

    public void SwitchToOrderStation()
    {
        if (GameManager.instance.currentClient.doneOrdering)
        {
            GameManager.instance.SwitchStation(stations.orderStation);
            HideDoneButton();
        }
    }

    public void SwitchToBrewingStation()
    {
        if (GameManager.instance.currentClient.doneOrdering)
        {
            GameManager.instance.SwitchStation(stations.brewingStation);
            HideDoneButton();
        }     
    }

    public void SwitchToToppingStation()
    {
        if (GameManager.instance.currentClient.doneOrdering)
        {
            GameManager.instance.SwitchStation(stations.toppingStation);
            HideDoneButton();
        }
    }

    public void SwitchToDecorationStation()
    {
        if (GameManager.instance.currentClient.doneOrdering)
        {
            GameManager.instance.SwitchStation(stations.decorationStation);
            doneButton.SetActive(true);
        }
    }

    public void HideDoneButton()
    {
        doneButton.SetActive(false);
    }

    public void TriggerDone()
    {
        GameManager.instance.Done();
        HideDoneButton();
    }

    public void BellPressed()
    {
        GameManager.instance.CheckIfOrderIsCorrect();
        bell.SetActive(false);
    }

    public void SetReceiptInactive()
    {
        receipt.SetActive(false);
    }

    public void SetReceiptActive()
    {
        receipt.SetActive(true);
    }

    public void SetStationUIInactive()
    {
        stationUI.SetActive(false);
    }

    public void SetStationUIActive()
    {
        stationUI.SetActive(true);
    }

    public void SetDateUIInactive()
    {
        dateUI.SetActive(false);
    }

    public void SetDateUIActive()
    {
        dateUI.SetActive(true);
    }

    public void Accept()
    {
        GameManager.instance.AcceptDate();
    }

    public void Decline()
    {
        GameManager.instance.DeclineDate();
    }

    public void ActivateDateUI(string option1, string option2)
    {
        option1Text.text = option1;
        option2Text.text = option2;
        datingUI.SetActive(true);
    }

    public void DeActivateDateUI()
    {
        datingUI.SetActive(false);
    }

    public void Answer(int responseIndex)
    {
        DeActivateDateUI();
        GameManager.instance.PlayerResponse(responseIndex);
    }

    public void ActivateEndScreen(Sprite screen)
    {
        endScreen.SetActive(true);
        endScreen.GetComponent<Image>().sprite = screen;
    }

    public void ActivateNextDayScreen(int day)
    {
        nextDayScreen.SetActive(true);
        nextDayText.text = "Day: " + day;
    }

    public void DeActivateNextDayScreen()
    {
        nextDayScreen.SetActive(false);
    }

    public void ActivateTutorialScreen()
    {
        tutorialScreen.SetActive(true);
        vidPlayer.time = 0f;
        vidPlayer.Play();
    }

    public void DeActivateTutorialScreen()
    {
        tutorialScreen.SetActive(false);
        vidPlayer.Pause();
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        GameManager.instance.startGame();
    }

    public void ActivateCreditScreen()
    {
        creditScreen.SetActive(true);
    }

    public void DeActivateCreditScreen()
    {
        creditScreen.SetActive(false);
    }

    public void ActivatePauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        GameManager.instance.ResumeGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
