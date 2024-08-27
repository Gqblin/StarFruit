using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private Sprite standartSprite;
    [SerializeField] public Ingredients ingredientType;
    [SerializeField] private Sprite PourSprite;

    [SerializeField] private LayerMask cauldronLayer;

    public bool pouring;

    private void Start()
    {
        standartSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void CheckIfAboveCauldron()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, cauldronLayer);
        if(hit == true)
        {
            if (hit.transform.gameObject.GetComponentInParent<Cauldron>())
            {
                Cauldron cauldron = hit.transform.gameObject.GetComponentInParent<Cauldron>();
                StartCoroutine(pour(cauldron));
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

    private IEnumerator pour(Cauldron c)
    {
        InteractManager.instance.canInteract = false;
        GetComponent<SpriteRenderer>().sprite = PourSprite;
        PlaySound();
        yield return new WaitForSeconds(.75f);
        c.AddIngredient(ingredientType);
        GetComponent<SpriteRenderer>().sprite = standartSprite;
        InteractManager.instance.RemoveObjectFromHand();
        InteractManager.instance.canInteract = true;
    }

    private void PlaySound()
    {
        if (ingredientType == Ingredients.water || ingredientType == Ingredients.coffee || ingredientType == Ingredients.wine || ingredientType == Ingredients.ghostPepperExtract)
        {
            AudioManager.instance.PlayPourEffect();
        }
        else
        {
            AudioManager.instance.PlayHardIngredientPoutEffect();
        }
    }
}
