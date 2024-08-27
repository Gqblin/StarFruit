using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Client : MonoBehaviour
{
    [Header("client info")]
    [SerializeField] private Characters clientName;

    [Header("Order Information")]
    [SerializeField] private int numberOfIngredients = 4;
    [SerializeField] private int numberOfDecorations = 3;
    [SerializeField] public List<Ingredients> order = new List<Ingredients>();
    [SerializeField] public List<Decorations> wantedDecorations = new List<Decorations>();
    [SerializeField] public Flasks wantedFlask;
    public bool doneOrdering;

    [Header("UI")]
    [SerializeField] private GameObject orderBubble;
    [SerializeField] TMP_Text orderText;
    [SerializeField] private GameObject dateBubble;
    [SerializeField] private TMP_Text dateText;
    [SerializeField] public GameObject orderButton;
    [SerializeField] private GameObject heartBar;
    [SerializeField] private List<GameObject> heartsUI = new List<GameObject>();

    [Header("Sprites")]
    [SerializeField] private List<Sprite> ingredientSprites = new List<Sprite>();
    [SerializeField] private GameObject ingrediensprite;
    [SerializeField] private List<Sprite> decorationSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> flaskSprites = new List<Sprite>();

    [SerializeField] private Sprite happyPose;
    [SerializeField] private Sprite neutralPose;
    [SerializeField] private Sprite sadPose;
    [SerializeField] private Sprite shyPose;
    [SerializeField] private Sprite akwardPose;

    [SerializeField] private Sprite EndScreen;

    [Header("Dialogue")]
    [SerializeField] private string requestText;
    [SerializeField] private List<string> rightOrderRecivedText = new List<string>();
    [SerializeField] private List<string> wrongOrderRecivedText = new List<string>();

    [SerializeField] private List<string> askOutForDateText = new List<string>();
    [SerializeField] private List<string> dateAcceptedText = new List<string>();
    [SerializeField] private List<string> dateDeclinedText = new List<string>();

    [SerializeField] private List<string> firstDialogue = new List<string>();
    [SerializeField] private List<string> playerFirstQuestionsOptions = new List<string>();
    [SerializeField] private List<string> firstQuestionResp1 = new List<string>();
    [SerializeField] private List<string> firstQuestionResp2 = new List<string>();

    [SerializeField] private List<string> secondDialogue = new List<string>();
    [SerializeField] private List<string> playerSecondQuestionsOptions = new List<string>();
    [SerializeField] private List<string> secondQuestionResp1 = new List<string>();
    [SerializeField] private List<string> secondQuestionResp2 = new List<string>();

    [SerializeField] private List<string> thirdDialogue = new List<string>();
    [SerializeField] private List<string> playerThirdQuestionsOptions = new List<string>();
    [SerializeField] private List<string> thirdQuestionResp1 = new List<string>();
    [SerializeField] private List<string> thirdQuestionResp2 = new List<string>();

    [SerializeField] private List<string> fourthDialogue = new List<string>();
    [SerializeField] private List<string> playerFourthQuestionsOptions = new List<string>();

    [SerializeField] private List<string> lastDialogue = new List<string>();

    public int hearts;
    private bool declined;
    private WaitForSeconds delay = new WaitForSeconds(1f);
    private WaitForSeconds dateTextDelay = new WaitForSeconds(2.5f);
    public int recivedResponse;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartOrder()
    {
        orderButton.SetActive(false);
        orderBubble.SetActive(true);
        StartCoroutine(GenerateAndPrintRandomOrder());
    }

    private IEnumerator GenerateAndPrintRandomOrder()
    {
        Ingredients randomIngredient = Ingredients.water;
        orderText.gameObject.SetActive(true);
        orderText.text = requestText;
        yield return delay;
        orderText.gameObject.SetActive(false);
        ingrediensprite.SetActive(true);
        for (int i = 0; i < numberOfIngredients; i++)
        {
            if(i == 0 || i == 1)
            {
                randomIngredient = (Ingredients)UnityEngine.Random.Range(0, 4);
                if(i == 1 && randomIngredient == order[0])
                {
                    while(randomIngredient == order[0])
                    {
                        randomIngredient = (Ingredients)UnityEngine.Random.Range(0, 4);
                    }
                }
            }
            if(i == 2 || i == 3)
            {
                randomIngredient = (Ingredients)UnityEngine.Random.Range(4, 7);
                if(i == 3 && randomIngredient == order[2])
                {
                    while(randomIngredient == order[2])
                    {
                        randomIngredient = (Ingredients)UnityEngine.Random.Range(4, 7);
                    }
                }
            }

            switch (randomIngredient)
            {
                case Ingredients.water:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = ingredientSprites[0];
                    UIManager.instance.SetReceiptImage(i, ingredientSprites[0]);
                    break;
                case Ingredients.wine:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = ingredientSprites[1];
                    UIManager.instance.SetReceiptImage(i, ingredientSprites[1]);
                    break;
                case Ingredients.ghostPepperExtract:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = ingredientSprites[2];
                    UIManager.instance.SetReceiptImage(i, ingredientSprites[2]);
                    break;
                case Ingredients.coffee:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = ingredientSprites[3];
                    UIManager.instance.SetReceiptImage(i, ingredientSprites[3]);
                    break;
                case Ingredients.eyeball:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = ingredientSprites[4];
                    UIManager.instance.SetReceiptImage(i, ingredientSprites[4]);
                    break;
                case Ingredients.starfruit:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = ingredientSprites[5];
                    UIManager.instance.SetReceiptImage(i, ingredientSprites[5]);
                    break;
                case Ingredients.horn:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = ingredientSprites[6];
                    UIManager.instance.SetReceiptImage(i, ingredientSprites[6]);
                    break;
            }
            order.Add(randomIngredient);
            yield return delay;
        }

        for(int i = 0; i < numberOfDecorations; i++)
        {
            Decorations deco = GetRandomEnum<Decorations>();
            switch (deco)
            {
                case Decorations.tie:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = decorationSprites[0];
                    UIManager.instance.SetReceiptImage(i + 4, decorationSprites[0]);
                    break;
                case Decorations.heart:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = decorationSprites[1];
                    UIManager.instance.SetReceiptImage(i + 4, decorationSprites[1]);
                    break;
                case Decorations.star:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = decorationSprites[2];
                    UIManager.instance.SetReceiptImage(i + 4, decorationSprites[2]);
                    break;
                case Decorations.rope:
                    ingrediensprite.GetComponent<SpriteRenderer>().sprite = decorationSprites[3];
                    UIManager.instance.SetReceiptImage(i + 4, decorationSprites[3]);
                    break;
            }
            wantedDecorations.Add(deco);
            yield return delay;
        }

        wantedFlask = (Flasks)UnityEngine.Random.Range(0, 4);
        ingrediensprite.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1f);
        switch (wantedFlask)
        {
            case Flasks.heartFlask:
                ingrediensprite.GetComponent<SpriteRenderer>().sprite = flaskSprites[0];
                UIManager.instance.SetReceiptImage(6, flaskSprites[0]);
                break;
            case Flasks.tallFlask:
                ingrediensprite.GetComponent<SpriteRenderer>().sprite = flaskSprites[1];
                UIManager.instance.SetReceiptImage(6, flaskSprites[1]);
                break;
            case Flasks.plantFlask:
                ingrediensprite.GetComponent<SpriteRenderer>().sprite = flaskSprites[2];
                UIManager.instance.SetReceiptImage(6, flaskSprites[2]);
                break;
            case Flasks.swanFlask:
                ingrediensprite.GetComponent<SpriteRenderer>().sprite = flaskSprites[3];
                UIManager.instance.SetReceiptImage(6, flaskSprites[3]);
                break;

        }
        yield return delay;
        ingrediensprite.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        ingrediensprite.SetActive(false);
        orderBubble.SetActive(false);
        doneOrdering = true;
    }

    private IEnumerator ThankEmployee()
    {
        dateBubble.SetActive(true);
        //orderText.gameObject.SetActive(true);
        spriteRenderer.sprite = happyPose;
        for (int i = 0; i < rightOrderRecivedText.Count; i++)
        {
            dateText.text = rightOrderRecivedText[i];
            yield return dateTextDelay;
        }
        dateBubble.SetActive(false);
        if (!declined)
        {
            StartCoroutine(ShowAddedHearts());
        }
        else
        {
            StartCoroutine(WalkAway());
        }
        
    }

    private IEnumerator ShowAddedHearts()
    {
        heartBar.SetActive(true);
        for(int i = 0; i < hearts - 1; i++)
        {
            heartsUI[i].SetActive(true);
        }
        yield return delay;
        for(int i = hearts - 1; i < hearts; i++)
        {
            heartsUI[i].SetActive(true);
            yield return delay;
        }
        yield return new WaitForSeconds(.5f);
        heartBar.SetActive(false);
        if(hearts == 3)
        {
            StartCoroutine(AskPlayerOutForDate());
        }
        else
        {
            StartCoroutine(WalkAway());
        } 

    }

    private IEnumerator GetMad()
    {
        dateBubble.SetActive(true);
        spriteRenderer.sprite = sadPose;
        for(int i = 0; i < wrongOrderRecivedText.Count; i++)
        {
            dateText.text = wrongOrderRecivedText[i];
            yield return dateTextDelay;
        }
        dateBubble.SetActive(false);
        StartCoroutine(ShowRemovedHearts());
    }

    private IEnumerator GetMadKeepHearts()
    {
        dateBubble.SetActive(true);
        spriteRenderer.sprite = sadPose;
        for (int i = 0; i < wrongOrderRecivedText.Count; i++)
        {
            dateText.text = wrongOrderRecivedText[i];
            yield return dateTextDelay;
        }
        dateBubble.SetActive(false);
        StartCoroutine(ShowHearts());
    }

    private IEnumerator ShowHearts()
    {
        heartBar.SetActive(true);
        for (int i = 0; i < hearts; i++)
        {
            heartsUI[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        heartBar.SetActive(false);
        StartCoroutine(WalkAway());
    }

    private IEnumerator ShowRemovedHearts()
    {
        heartBar.SetActive(true);
        for (int i = 0; i < hearts; i++)
        {
            heartsUI[i].SetActive(true);
        }
        if (hearts > 0)
        {
            hearts--;
        }
        yield return delay;
        for (int i = hearts; i >= hearts; i--)
        {
            heartsUI[i].SetActive(false);
            yield return delay;
        }
        yield return delay;
        heartBar.SetActive(false);
        StartCoroutine(WalkAway());
    }

    private IEnumerator WalkAway()
    {
        yield return delay;
        GameManager.instance.RemoveClient();
        spriteRenderer.sprite = neutralPose;
        doneOrdering = false;
        this.gameObject.SetActive(false);
    }

    public void AddHeart()
    {
        hearts++;
        StartCoroutine(ThankEmployee());
    }

    public void LoseHeart()
    {
        StartCoroutine(GetMad());
    }

    public void KeepHearts()
    {
        StartCoroutine(GetMadKeepHearts());
    }

    private T GetRandomEnum<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    public void ResetOrders()
    {
        order.Clear();
        wantedDecorations.Clear();
    }

    private IEnumerator AskPlayerOutForDate()
    {
        dateBubble.SetActive(true);
        UIManager.instance.SetStationUIInactive();
        UIManager.instance.SetReceiptInactive();
        switch (clientName)
        {
            case Characters.aster:
                spriteRenderer.sprite = shyPose;
                break;
            case Characters.pseuderos:
                spriteRenderer.sprite = neutralPose;
                break;
            case Characters.helios:
                spriteRenderer.sprite = akwardPose;
                break;
        }
        
        for(int i = 0; i < askOutForDateText.Count; i++)
        {
            dateText.text = askOutForDateText[i];
            yield return delay;
        }
        UIManager.instance.SetDateUIActive();
    }

    public IEnumerator Accapted()
    {
        UIManager.instance.SetDateUIInactive();
        switch (clientName)
        {
            case Characters.aster:
                spriteRenderer.sprite = happyPose;
                break;
            case Characters.pseuderos:
                spriteRenderer.sprite = neutralPose;
                break;
            case Characters.helios:
                spriteRenderer.sprite = happyPose;
                break;
        }
        for (int i = 0; i < dateAcceptedText.Count; i++)
        {
            dateText.text = dateAcceptedText[i];
            yield return dateTextDelay;
        }
        dateBubble.SetActive(false);
        yield return delay;
        GameManager.instance.StartDate();
    }

    public IEnumerator Declined()
    {
        hearts = 0;
        declined = true;
        UIManager.instance.SetDateUIInactive();
        switch (clientName)
        {
            case Characters.aster:
                spriteRenderer.sprite = sadPose;
                break;
            case Characters.pseuderos:
                spriteRenderer.sprite = neutralPose;
                break;
            case Characters.helios:
                spriteRenderer.sprite = akwardPose;
                break;
        }
        for (int i = 0; i < dateDeclinedText.Count; i++)
        {
            dateText.text = dateDeclinedText[i];
            yield return dateTextDelay;
        }
        UIManager.instance.SetStationUIActive();
        UIManager.instance.SetReceiptActive();
        StartCoroutine(WalkAway());
    }

    public IEnumerator StartDateDiaglogue()
    {
        spriteRenderer.sprite = neutralPose;
        yield return delay;
        dateBubble.SetActive(true);
        for(int i = 0; i < firstDialogue.Count; i++)
        {
            dateText.text = firstDialogue[i];
            yield return dateTextDelay;
        }
        UIManager.instance.ActivateDateUI(playerFirstQuestionsOptions[0], playerFirstQuestionsOptions[1]);
        GameManager.instance.StopTime();
        yield return delay;
        if(recivedResponse == 0)
        {
            switch (clientName)
            {
                case Characters.aster:
                    spriteRenderer.sprite = neutralPose;
                    break;
                case Characters.pseuderos:
                    spriteRenderer.sprite = neutralPose;
                    break;
                case Characters.helios:
                    spriteRenderer.sprite = happyPose;
                    break;
            }
            for (int i = 0; i < firstQuestionResp1.Count; i++)
            {
                dateText.text = firstQuestionResp1[i];
                yield return dateTextDelay;
            }
        }
        else
        {
            switch (clientName)
            {
                case Characters.aster:
                    spriteRenderer.sprite = shyPose;
                    break;
                case Characters.pseuderos:
                    spriteRenderer.sprite = neutralPose;
                    break;
                case Characters.helios:
                    spriteRenderer.sprite = neutralPose;
                    break;
            }
            for (int i = 0; i < firstQuestionResp2.Count; i++)
            {
                dateText.text = firstQuestionResp2[i];
                yield return dateTextDelay;
            }
        }
        switch (clientName)
        {
            case Characters.aster:
                spriteRenderer.sprite = shyPose;
                break;
            case Characters.pseuderos:
                spriteRenderer.sprite = neutralPose;
                break;
            case Characters.helios:
                spriteRenderer.sprite = happyPose;
                break;
        }
        for (int i = 0; i < secondDialogue.Count; i++)
        {
            dateText.text = secondDialogue[i];
            yield return dateTextDelay;
        }

        UIManager.instance.ActivateDateUI(playerSecondQuestionsOptions[0], playerSecondQuestionsOptions[1]);
        GameManager.instance.StopTime();
        yield return new WaitForSeconds(.5f);
        if (recivedResponse == 0)
        {
            switch (clientName)
            {
                case Characters.aster:
                    spriteRenderer.sprite = happyPose;
                    break;
                case Characters.pseuderos:
                    spriteRenderer.sprite = neutralPose;
                    break;
                case Characters.helios:
                    spriteRenderer.sprite = sadPose;
                    break;
            }
            for (int i = 0; i < secondQuestionResp1.Count; i++)
            {
                dateText.text = secondQuestionResp1[i];
                yield return dateTextDelay;
            }
        }
        else
        {
            switch (clientName)
            {
                case Characters.aster:
                    spriteRenderer.sprite = neutralPose;
                    break;
                case Characters.pseuderos:
                    spriteRenderer.sprite = neutralPose;
                    break;
                case Characters.helios:
                    spriteRenderer.sprite = happyPose;
                    break;
            }
            for (int i = 0; i < secondQuestionResp2.Count; i++)
            {
                dateText.text = secondQuestionResp2[i];
                yield return dateTextDelay;
            }
        }

        switch (clientName)
        {
            case Characters.aster:
                spriteRenderer.sprite = shyPose;
                break;
            case Characters.pseuderos:
                spriteRenderer.sprite = neutralPose;
                break;
            case Characters.helios:
                spriteRenderer.sprite = happyPose;
                break;
        }
        for (int i = 0; i < thirdDialogue.Count; i++)
        {
            dateText.text = thirdDialogue[i];
            yield return dateTextDelay;
        }



        UIManager.instance.ActivateDateUI(playerThirdQuestionsOptions[0], playerThirdQuestionsOptions[1]);
        GameManager.instance.StopTime();
        yield return new WaitForSeconds(.5f);
        if (recivedResponse == 0)
        {
            switch (clientName)
            {
                case Characters.aster:
                    spriteRenderer.sprite = shyPose;
                    break;
                case Characters.pseuderos:
                    spriteRenderer.sprite = neutralPose;
                    break;
                case Characters.helios:
                    spriteRenderer.sprite = happyPose;
                    break;
            }
            for (int i = 0; i < thirdQuestionResp1.Count; i++)
            {
                dateText.text = thirdQuestionResp1[i];
                yield return dateTextDelay;
            }
        }
        else
        {
            switch (clientName)
            {
                case Characters.aster:
                    spriteRenderer.sprite = happyPose;
                    break;
                case Characters.pseuderos:
                    spriteRenderer.sprite = neutralPose;
                    break;
                case Characters.helios:
                    spriteRenderer.sprite = happyPose;
                    break;
            }
            for (int i = 0; i < thirdQuestionResp2.Count; i++)
            {
                dateText.text = thirdQuestionResp2[i];
                yield return dateTextDelay;
            }
        }

        switch (clientName)
        {
            case Characters.aster:
                spriteRenderer.sprite = shyPose;
                break;
            case Characters.pseuderos:
                spriteRenderer.sprite = neutralPose;
                break;
            case Characters.helios:
                spriteRenderer.sprite = happyPose;
                break;
        }
        for (int i = 0; i < fourthDialogue.Count; i++)
        {
            dateText.text = fourthDialogue[i];
            yield return dateTextDelay;
        }



        UIManager.instance.ActivateDateUI(playerFourthQuestionsOptions[0], playerFourthQuestionsOptions[1]);
        GameManager.instance.StopTime();
        yield return new WaitForSeconds(1);
        switch (clientName)
        {
            case Characters.aster:
                spriteRenderer.sprite = happyPose;
                break;
            case Characters.pseuderos:
                spriteRenderer.sprite = neutralPose;
                break;
            case Characters.helios:
                spriteRenderer.sprite = happyPose;
                break;
        }
        for (int i = 0; i < lastDialogue.Count; i++)
        {
            dateText.text = lastDialogue[i];
            yield return dateTextDelay;
        }

        this.gameObject.SetActive(false);
        UIManager.instance.ActivateEndScreen(EndScreen);
    }
}
