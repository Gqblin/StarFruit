using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    public List<Ingredients> ingredients = new List<Ingredients>();
    public List<Decorations> decorations = new List<Decorations>();
    public Flasks flaskType;

    private SpriteRenderer spriteRenderer;

    public bool stirred;

    [SerializeField] private Sprite emptyTallFlask;
    [SerializeField] private Sprite emptyHeartFLask;
    [SerializeField] private Sprite emptyPlantFlask;
    [SerializeField] private Sprite emptySwanFlask;

    [SerializeField] private List<Sprite> filledHeartFlaskSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> filledTallFlaskSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> filledPlantFlaskSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> filledSwanFlaskSprites = new List<Sprite>();

    [SerializeField] private List<GameObject> tallFlaskHardIngredients = new List<GameObject>();
    [SerializeField] private List<GameObject> heartFlaskHardIngredients = new List<GameObject>();
    [SerializeField] private List<GameObject> plantFlaskHardIngredients = new List<GameObject>();
    [SerializeField] private List<GameObject> swanFlaskHardIngredients = new List<GameObject>();

    [SerializeField] private GameObject pourCollider;
    private bool isEnabled;

    private void Start()
    {
        flaskType = Flasks.tallFlask;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void setToEmptyFlask()
    {
        flaskType = Flasks.tallFlask;
        spriteRenderer.sprite = emptyTallFlask;
    }

    public void changeLiquid(int index)
    {
        switch (flaskType)
        {
            case Flasks.tallFlask:
                spriteRenderer.sprite = filledTallFlaskSprites[index];
                break;
            case Flasks.heartFlask:
                spriteRenderer.sprite = filledHeartFlaskSprites[index];
                break;
            case Flasks.plantFlask:
                spriteRenderer.sprite = filledPlantFlaskSprites[index];
                break;
            case Flasks.swanFlask:
                spriteRenderer.sprite = filledSwanFlaskSprites[index];
                break;
        }

    }

    public void UpdateIngredientSprites()
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            switch (ingredients[i])
            {
                case Ingredients.starfruit:
                    switch (flaskType)
                    {
                        case Flasks.tallFlask:
                            tallFlaskHardIngredients[0].SetActive(true);
                            break;
                        case Flasks.heartFlask:
                            heartFlaskHardIngredients[0].SetActive(true);
                            break;
                        case Flasks.plantFlask:
                            plantFlaskHardIngredients[0].SetActive(true);
                            break;
                        case Flasks.swanFlask:
                            swanFlaskHardIngredients[0].SetActive(true);
                            break;
                    }
                    break;
                case Ingredients.eyeball:
                    switch (flaskType)
                    {
                        case Flasks.tallFlask:
                            tallFlaskHardIngredients[1].SetActive(true);
                            break;
                        case Flasks.heartFlask:
                            heartFlaskHardIngredients[1].SetActive(true);
                            break;
                        case Flasks.plantFlask:
                            plantFlaskHardIngredients[1].SetActive(true);
                            break;
                        case Flasks.swanFlask:
                            swanFlaskHardIngredients[1].SetActive(true);
                            break;
                    }
                    break;
                case Ingredients.horn:
                    switch (flaskType)
                    {
                        case Flasks.tallFlask:
                            tallFlaskHardIngredients[2].SetActive(true);
                            break;
                        case Flasks.heartFlask:
                            heartFlaskHardIngredients[2].SetActive(true);
                            break;
                        case Flasks.plantFlask:
                            plantFlaskHardIngredients[2].SetActive(true);
                            break;
                        case Flasks.swanFlask:
                            swanFlaskHardIngredients[2].SetActive(true);
                            break;
                    }
                    break;
            }
        }

    }

    public void ChangeHardIngridients(Flasks f)
    {
        if (!isEnabled)
        {
            EnableFlask();
        }

        if(flaskType != f)
        {

            switch (f)
            {
                case Flasks.heartFlask:
                    if(spriteRenderer.sprite == emptyTallFlask || spriteRenderer.sprite == emptyPlantFlask || spriteRenderer.sprite == emptySwanFlask)
                    {
                        spriteRenderer.sprite = emptyHeartFLask;
                    }
                    else
                    {
                        switch (flaskType)
                        {
                            case Flasks.tallFlask:
                                for(int i = 0; i < filledTallFlaskSprites.Count; i++)
                                {
                                    if(spriteRenderer.sprite == filledTallFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledHeartFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < tallFlaskHardIngredients.Count; i++)
                                {
                                    if (tallFlaskHardIngredients[i].activeSelf)
                                    {
                                        heartFlaskHardIngredients[i].SetActive(true);
                                        tallFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                            case Flasks.plantFlask:
                                for (int i = 0; i < filledPlantFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledPlantFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledHeartFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < plantFlaskHardIngredients.Count; i++)
                                {
                                    if (plantFlaskHardIngredients[i].activeSelf)
                                    {
                                        heartFlaskHardIngredients[i].SetActive(true);
                                        plantFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                            case Flasks.swanFlask:
                                for (int i = 0; i < filledSwanFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledSwanFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledHeartFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < swanFlaskHardIngredients.Count; i++)
                                {
                                    if (swanFlaskHardIngredients[i].activeSelf)
                                    {
                                        heartFlaskHardIngredients[i].SetActive(true);
                                        swanFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                        }
                    }
                    break;

                case Flasks.tallFlask:
                    if (spriteRenderer.sprite == emptyHeartFLask || spriteRenderer.sprite == emptyPlantFlask || spriteRenderer.sprite == emptySwanFlask)
                    {
                        spriteRenderer.sprite = emptyTallFlask;
                    }
                    else
                    {
                        switch (flaskType)
                        {
                            case Flasks.heartFlask:
                                for (int i = 0; i < filledHeartFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledHeartFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledTallFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < heartFlaskHardIngredients.Count; i++)
                                {
                                    if (heartFlaskHardIngredients[i].activeSelf)
                                    {
                                        tallFlaskHardIngredients[i].SetActive(true);
                                        heartFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                            case Flasks.plantFlask:
                                for (int i = 0; i < filledPlantFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledPlantFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledTallFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < plantFlaskHardIngredients.Count; i++)
                                {
                                    if (plantFlaskHardIngredients[i].activeSelf)
                                    {
                                        tallFlaskHardIngredients[i].SetActive(true);
                                        plantFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                            case Flasks.swanFlask:
                                for (int i = 0; i < filledSwanFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledSwanFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledTallFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < swanFlaskHardIngredients.Count; i++)
                                {
                                    if (swanFlaskHardIngredients[i].activeSelf)
                                    {
                                        tallFlaskHardIngredients[i].SetActive(true);
                                        swanFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                        }
                    }
                    break;

                case Flasks.plantFlask:
                    if (spriteRenderer.sprite == emptyHeartFLask || spriteRenderer.sprite == emptyTallFlask || spriteRenderer.sprite == emptySwanFlask)
                    {
                        spriteRenderer.sprite = emptyPlantFlask;
                    }
                    else
                    {
                        switch (flaskType)
                        {
                            case Flasks.heartFlask:
                                for (int i = 0; i < filledHeartFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledHeartFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledPlantFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < heartFlaskHardIngredients.Count; i++)
                                {
                                    if (heartFlaskHardIngredients[i].activeSelf)
                                    {
                                        plantFlaskHardIngredients[i].SetActive(true);
                                        heartFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                            case Flasks.tallFlask:
                                for (int i = 0; i < filledTallFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledTallFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledPlantFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < tallFlaskHardIngredients.Count; i++)
                                {
                                    if (tallFlaskHardIngredients[i].activeSelf)
                                    {
                                        plantFlaskHardIngredients[i].SetActive(true);
                                        tallFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                            case Flasks.swanFlask:
                                for (int i = 0; i < filledSwanFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledSwanFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledPlantFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < swanFlaskHardIngredients.Count; i++)
                                {
                                    if (swanFlaskHardIngredients[i].activeSelf)
                                    {
                                        plantFlaskHardIngredients[i].SetActive(true);
                                        swanFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                        }
                    }
                    break;

                case Flasks.swanFlask:
                    if (spriteRenderer.sprite == emptyHeartFLask || spriteRenderer.sprite == emptyTallFlask || spriteRenderer.sprite == emptyPlantFlask)
                    {
                        spriteRenderer.sprite = emptySwanFlask;
                    }
                    else
                    {
                        switch (flaskType)
                        {
                            case Flasks.heartFlask:
                                for (int i = 0; i < filledHeartFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledHeartFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledSwanFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < heartFlaskHardIngredients.Count; i++)
                                {
                                    if (heartFlaskHardIngredients[i].activeSelf)
                                    {
                                        swanFlaskHardIngredients[i].SetActive(true);
                                        heartFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                            case Flasks.tallFlask:
                                for (int i = 0; i < filledTallFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledTallFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledSwanFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < tallFlaskHardIngredients.Count; i++)
                                {
                                    if (tallFlaskHardIngredients[i].activeSelf)
                                    {
                                        swanFlaskHardIngredients[i].SetActive(true);
                                        tallFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                            case Flasks.plantFlask:
                                for (int i = 0; i < filledPlantFlaskSprites.Count; i++)
                                {
                                    if (spriteRenderer.sprite == filledPlantFlaskSprites[i])
                                    {
                                        spriteRenderer.sprite = filledSwanFlaskSprites[i];
                                        break;
                                    }
                                }
                                for (int i = 0; i < plantFlaskHardIngredients.Count; i++)
                                {
                                    if (plantFlaskHardIngredients[i].activeSelf)
                                    {
                                        swanFlaskHardIngredients[i].SetActive(true);
                                        plantFlaskHardIngredients[i].SetActive(false);
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }




            if(f == Flasks.tallFlask)
            {
                if(spriteRenderer.sprite == emptyHeartFLask)
                {
                    spriteRenderer.sprite = emptyTallFlask;
                }
                else
                {
                    for (int i = 0; i < filledHeartFlaskSprites.Count; i++)
                    {
                        if (spriteRenderer.sprite == filledHeartFlaskSprites[i])
                        {
                            spriteRenderer.sprite = filledTallFlaskSprites[i];
                            break;
                        }
                    }
                }            

                for(int i = 0; i < heartFlaskHardIngredients.Count; i++)
                {
                    if (heartFlaskHardIngredients[i].activeSelf)
                    {
                        tallFlaskHardIngredients[i].SetActive(true);
                        heartFlaskHardIngredients[i].SetActive(false);
                    }
                }
            }
            else
            {
                if (spriteRenderer.sprite == emptyTallFlask)
                {
                    spriteRenderer.sprite = emptyHeartFLask;
                }
                else
                {
                    for (int i = 0; i < filledTallFlaskSprites.Count; i++)
                    {
                        if (spriteRenderer.sprite == filledTallFlaskSprites[i])
                        {
                            spriteRenderer.sprite = filledHeartFlaskSprites[i];
                            break;
                        }
                    }
                }              

                for (int i = 0; i < tallFlaskHardIngredients.Count; i++)
                {
                    if (tallFlaskHardIngredients[i].activeSelf)
                    {
                        heartFlaskHardIngredients[i].SetActive(true);
                        tallFlaskHardIngredients[i].SetActive(false);
                    }
                }
            }
            flaskType = f;
        }
    }

    private void EnableFlask()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        pourCollider.SetActive(true);
        isEnabled = true;
    }

    public void DisableFlask()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        pourCollider.SetActive(false);
        isEnabled = false;
    }
}
