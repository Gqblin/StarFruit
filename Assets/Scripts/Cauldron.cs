using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private List<Ingredients> ingredientsInCauldron = new List<Ingredients>();

    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private List<Sprite> pourSprites = new List<Sprite>();

    private Sprite originalSprite;

    [SerializeField] private List<GameObject> hardIngredients = new List<GameObject>();

    [SerializeField] private List<StirTrigger> stirTriggers = new List<StirTrigger>();
    private int stirs;
    [SerializeField] private bool stirred;

    [SerializeField] private LayerMask flaskLayer;

    private void Start()
    {
        originalSprite = GetComponent<SpriteRenderer>().sprite;
    }
    public void AddIngredient(Ingredients ingredient)
    {
        ingredientsInCauldron.Add(ingredient);
        if(ingredient == Ingredients.water)
        {
            if(ingredientsInCauldron.Count == 0)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            else
            {
                if (ingredientsInCauldron[0] == Ingredients.water)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[0];
                }
                if (ingredientsInCauldron[0] == Ingredients.coffee)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[4];
                }
                if (ingredientsInCauldron[0] == Ingredients.wine)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[5];
                }
                if (ingredientsInCauldron[0] == Ingredients.ghostPepperExtract)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[6];
                }
            }
        }
        if(ingredient == Ingredients.coffee)
        {
            if(ingredientsInCauldron.Count == 0)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            else
            {
                if (ingredientsInCauldron[0] == Ingredients.water)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[4];
                }
                if (ingredientsInCauldron[0] == Ingredients.coffee)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
                if (ingredientsInCauldron[0] == Ingredients.wine)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[7];
                }
                if (ingredientsInCauldron[0] == Ingredients.ghostPepperExtract)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[8];
                }
            }
            
        }
        if(ingredient == Ingredients.wine)
        {
            if(ingredientsInCauldron.Count == 0)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[2];
            }
            else
            {
                if (ingredientsInCauldron[0] == Ingredients.water)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[5];
                }
                if (ingredientsInCauldron[0] == Ingredients.coffee)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[7];
                }
                if (ingredientsInCauldron[0] == Ingredients.wine)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[2];
                }
                if (ingredientsInCauldron[0] == Ingredients.ghostPepperExtract)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[9];
                }
            }
        }
        if (ingredient == Ingredients.ghostPepperExtract)
        {
            if (ingredientsInCauldron.Count == 0)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[3];
            }
            else
            {
                if (ingredientsInCauldron[0] == Ingredients.water)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[6];
                }
                if (ingredientsInCauldron[0] == Ingredients.coffee)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[8];
                }
                if (ingredientsInCauldron[0] == Ingredients.wine)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[9];
                }
                if (ingredientsInCauldron[0] == Ingredients.ghostPepperExtract)
                {
                    GetComponent<SpriteRenderer>().sprite = sprites[3];
                }
            }
            
        }
        if(ingredient == Ingredients.starfruit)
        {
            hardIngredients[0].SetActive(true);
        }
        if (ingredient == Ingredients.horn)
        {
            hardIngredients[1].SetActive(true);
        }
        if (ingredient == Ingredients.eyeball)
        {
            hardIngredients[2].SetActive(true);
        }

        if(ingredientsInCauldron.Count >= 2)
        {
            if (ingredient == Ingredients.water || ingredient == Ingredients.coffee || ingredient == Ingredients.wine || ingredient == Ingredients.ghostPepperExtract)
            {
                stirred = false;
                stirTriggers[0].gameObject.SetActive(true);
                stirTriggers[1].gameObject.SetActive(true);
            }
            
        }

    }

    public void CheckIfAboveCauldron()
    {
        if(ingredientsInCauldron.Count > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, flaskLayer);
            if (hit == true)
            {
                if (hit.transform.gameObject.GetComponentInParent<Flask>())
                {
                    Flask flask = hit.transform.gameObject.GetComponentInParent<Flask>();
                    StartCoroutine(pour(flask));
                }
                else if (hit.transform.gameObject.GetComponent<TrashCan>())
                {
                    StartCoroutine(PourIntoTrash());
                }
                else
                {
                    InteractManager.instance.RemoveObjectFromHand();
                }
            }
            else
            {
                InteractManager.instance.RemoveObjectFromHand();
            }
        }
        else
        {
            InteractManager.instance.RemoveObjectFromHand();
        }
        
    }

    private IEnumerator PourIntoTrash()
    {
        InteractManager.instance.canInteract = false;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        if (sprite == sprites[0])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[0];
        }
        if (sprite == sprites[1])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[1];
        }
        if (sprite == sprites[2])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[2];
        }
        if (sprite == sprites[3])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[3];
        }
        if (sprite == sprites[4])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[4];
        }
        if (sprite == sprites[5])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[5];
        }
        if (sprite == sprites[6])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[6];
        }
        if (sprite == sprites[7])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[7];
        }
        if (sprite == sprites[8])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[8];
        }
        if (sprite == sprites[9])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[9];
        }
        for (int i = 0; i < hardIngredients.Count; i++)
        {
            hardIngredients[i].SetActive(false);
        }
        AudioManager.instance.PlayPourEffect();
        yield return new WaitForSeconds(.75f);
        ingredientsInCauldron.Clear();
        InteractManager.instance.RemoveObjectFromHand();
        ResetSprite();
        InteractManager.instance.canInteract = true;
    }

    private IEnumerator pour(Flask f)
    {
        InteractManager.instance.canInteract = false;
        int index = 0;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        if(sprite == sprites[0])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[0];
            index = 0;
        }
        if (sprite == sprites[1])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[1];
            index = 1;
        }
        if (sprite == sprites[2])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[2];
            index = 2;
        }
        if (sprite == sprites[3])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[3];
            index = 3;
        }
        if (sprite == sprites[4])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[4];
            index = 4;
        }
        if (sprite == sprites[5])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[5];
            index = 5;
        }
        if (sprite == sprites[6])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[6];
            index = 6;
        }
        if (sprite == sprites[7])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[7];
            index = 7;
        }
        if (sprite == sprites[8])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[8];
            index = 8;
        }
        if (sprite == sprites[9])
        {
            GetComponent<SpriteRenderer>().sprite = pourSprites[9];
            index = 9;
        }
        for (int i = 0; i < hardIngredients.Count; i++)
        {
            hardIngredients[i].SetActive(false);
        }
        AudioManager.instance.PlayPourEffect();
        yield return new WaitForSeconds(.75f);
        for (int i = 0; i < ingredientsInCauldron.Count; i++)
        {
            f.ingredients.Add(ingredientsInCauldron[i]);
        }
        f.changeLiquid(index);
        f.stirred = stirred;
        ingredientsInCauldron.Clear();
        InteractManager.instance.RemoveObjectFromHand();
        f.UpdateIngredientSprites();
        ResetSprite();
        InteractManager.instance.canInteract = true;
    }

    private void ResetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
        for(int i = 0; i < hardIngredients.Count; i++)
        {
            hardIngredients[i].SetActive(false);
        }
        stirred = false;
        stirTriggers[0].gameObject.SetActive(false);
        stirTriggers[1].gameObject.SetActive(false);
    }

    public void triggered()
    {
        if(stirTriggers[0].isHit && stirTriggers[1].isHit)
        {
            stirTriggers[0].isHit = false;
            stirTriggers[1].isHit = false;
            stirs++;
            if(stirs >= 4)
            {
                stirred = true;
                stirs = 0;
                InteractManager.instance.RemoveObjectFromHand();
                stirTriggers[0].isHit = false;
                stirTriggers[1].isHit = false;
                stirTriggers[0].gameObject.SetActive(false);
                stirTriggers[1].gameObject.SetActive(false);
            }
        }
    }
}
