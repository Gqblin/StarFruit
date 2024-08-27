using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum stations
{
    orderStation,
    brewingStation,
    toppingStation,
    decorationStation,
    dateRoom
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Camera cam;
    private Cauldron cauldron;

    [SerializeField] private List<GameObject> stationWaypoints = new List<GameObject>();
    [SerializeField] private List<GameObject> cauldronLocations = new List<GameObject>();
    [SerializeField] private List<GameObject> clients = new List<GameObject>();
    [SerializeField] private GameObject doneFlaskLocation;
    [SerializeField] private GameObject clientPrefab;
    [SerializeField] private List<Sprite> clientSprites = new List<Sprite>();
    [SerializeField] private GameObject clientSpawnLocation;
    [SerializeField] private GameObject flaskLocation;

    [SerializeField] private GameObject dateClientLocation;

    public Client currentClient;

    public float money;
    private int day = 1;
    private int alreadyVisitedClientNumber;
    private bool paused;

    public stations currentStation;

    public bool flaskDone;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        cauldron = FindObjectOfType<Cauldron>();
        StartCoroutine(SpawnNewClient());
        Time.timeScale = 0f;
        paused = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Time.timeScale = 0;
            paused = true;
            UIManager.instance.ActivatePauseScreen();
        }
    }

    public void startGame()
    {
        Time.timeScale = 1f;
        paused = false;
        AudioManager.instance.ActivateGameplayMusic();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        paused = false;
    }

    public void CheckIfOrderIsCorrect()
    {
        int counter = 0;

        Flask flask = FindObjectOfType<Flask>();
        currentClient.order.Sort();
        flask.ingredients.Sort();
        if (!flask.stirred)
        {
            counter++;
        }
        
        if(currentClient.order.Count != flask.ingredients.Count)
        {
            if (currentClient.order.Count - flask.ingredients.Count == 1 || currentClient.order.Count - flask.ingredients.Count == -1)
            {
                counter++;
            }
            else if (currentClient.order.Count - flask.ingredients.Count == 2 || currentClient.order.Count - flask.ingredients.Count == -2)
            {
                counter += 2;
            }
            else
            {
                counter += 3;
            }
        }
        else
        {
            for (int i = 0; i < currentClient.order.Count; i++)
            {
                if (currentClient.order[i] != flask.ingredients[i])
                {
                    counter++;
                }
            }
        }

        if(currentClient.order.Count < flask.ingredients.Count)
        {
            for (int i = 0; i < currentClient.order.Count; i++)
            {
                if (currentClient.order[i] != flask.ingredients[i])
                {
                    counter++;
                }
            }
        }
        if (currentClient.order.Count > flask.ingredients.Count)
        {
            for (int i = 0; i < flask.ingredients.Count; i++)
            {
                if (flask.ingredients[i] != currentClient.order[i])
                {
                    counter++;
                }
            }
        }

        for (int i = 0; i < flask.decorations.Count; i++)
        {
            if (flask.decorations[i] != currentClient.wantedDecorations[0] && flask.decorations[i] != currentClient.wantedDecorations[1])
            {
                counter++;
            }
        }

        if (flask.flaskType != currentClient.wantedFlask)
        {
            counter++;
        }
        if(counter == 0)
        {
            currentClient.AddHeart();
            ResetFlask();
            AudioManager.instance.PlayRightOrder();
        }
        if(counter == 1 || counter == 2)
        {
            currentClient.KeepHearts();
            ResetFlask();
            AudioManager.instance.PlayeWrongOrder();
        }
        if(counter >= 3)
        {
            currentClient.LoseHeart();
            ResetFlask();
            AudioManager.instance.PlayeWrongOrder();
        }
    }

    private void ResetFlask()
    {
        Flask flask = FindObjectOfType<Flask>();
        flask.ingredients.Clear();
        flask.decorations.Clear();
        flask.stirred = false;
        foreach (Transform child in flask.transform)
        {
            child.gameObject.SetActive(false);
        }
        flask.transform.position = flaskLocation.transform.position;
        flask.setToEmptyFlask();
        flask.DisableFlask();
        UIManager.instance.DeleteReceipt();
    }

    public void RemoveClient()
    {
        flaskDone = false;
        currentClient = null;
        alreadyVisitedClientNumber++;
        if(alreadyVisitedClientNumber == 3)
        {
            StartCoroutine(StartNextDay());
        }
        else
        {
            StartCoroutine(SpawnNewClient());
        }
    }

    private IEnumerator SpawnNewClient()
    {
        yield return new WaitForSeconds(2);
        //alreadyVisitedClientNumber = Random.Range(0, 3);
        clients[alreadyVisitedClientNumber].SetActive(true);
        currentClient = clients[alreadyVisitedClientNumber].GetComponent<Client>();
        currentClient.ResetOrders();
        currentClient.orderButton.SetActive(true);
        AudioManager.instance.PlayBell();
        //currentClient = Instantiate(clientPrefab, clientSpawnLocation.transform).GetComponent<Client>();
        //currentClient.GetComponent<SpriteRenderer>().sprite = clientSprites[Random.Range(0, clientSprites.Count)];
    }

    public IEnumerator StartNextDay()
    {
        yield return new WaitForSeconds(1);
        day++;
        UIManager.instance.ActivateNextDayScreen(day);
        yield return new WaitForSeconds(2.5f);
        UIManager.instance.DeActivateNextDayScreen();
        alreadyVisitedClientNumber = 0;
        StartCoroutine(SpawnNewClient());
    }

    public void SwitchStation(stations station)
    {
        if (!flaskDone)
        {
            switch (station)
            {
                case stations.orderStation:
                    cam.transform.position = stationWaypoints[0].transform.position;
                    break;
                case stations.brewingStation:
                    cam.transform.position = stationWaypoints[1].transform.position;
                    cauldron.transform.position = cauldronLocations[0].transform.position;
                    break;
                case stations.toppingStation:
                    cam.transform.position = stationWaypoints[2].transform.position;
                    cauldron.transform.position = cauldronLocations[1].transform.position;
                    break;
                case stations.decorationStation:
                    cam.transform.position = stationWaypoints[3].transform.position;
                    cauldron.transform.position = cauldronLocations[2].transform.position;
                    break;
            }
        } 
    }

    public void StartDate()
    {
        currentClient.transform.position = dateClientLocation.transform.position;
        StartCoroutine(FadeToDate());
    }

    private IEnumerator FadeToDate()
    {
        yield return new WaitForSeconds(1);
        cam.transform.position = stationWaypoints[4].transform.position;
        StartCoroutine(currentClient.StartDateDiaglogue());
    }

    public void Done()
    {
        cam.transform.position = stationWaypoints[0].transform.position;
        FindObjectOfType<Flask>().transform.position = doneFlaskLocation.transform.position;
        flaskDone = true;
        StartCoroutine(CheckOrder());
    }

    private IEnumerator CheckOrder()
    {
        yield return new WaitForSeconds(1f);
        CheckIfOrderIsCorrect();
    }

    public void AcceptDate()
    {
        StartCoroutine(currentClient.Accapted());
    }

    public void DeclineDate()
    {
        StartCoroutine(currentClient.Declined());
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void PlayerResponse(int resp)
    {
        currentClient.recivedResponse = resp;
        Time.timeScale = 1f;
    }

}
