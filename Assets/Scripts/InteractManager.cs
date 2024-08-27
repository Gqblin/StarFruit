using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    [SerializeField] private GameObject objectInHand;
    private Vector2 pickedUpObjectOriginLocation;

    public static InteractManager instance;

    private Vector2 mousePosition;

    public bool canInteract;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        canInteract = true;
    }

    void Update()
    {
        if (canInteract)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButton(0))
            {
                if (objectInHand == null)
                {
                    Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
                    if (targetObject)
                    {
                        if (targetObject.GetComponent<Ingredient>() || targetObject.GetComponent<Cauldron>()) //tijdelijke oplossing
                        {
                            pickedUpObjectOriginLocation = targetObject.transform.position;
                            objectInHand = targetObject.transform.gameObject;
                        }
                        if (targetObject.GetComponent<Decoration>())
                        {
                            GameObject deco = targetObject.transform.gameObject;
                            objectInHand = Instantiate(deco);
                        }
                        if (targetObject.GetComponent<Spoon>())
                        {
                            pickedUpObjectOriginLocation = targetObject.transform.position;
                            objectInHand = targetObject.transform.gameObject;
                        }
                        //AudioManager.instance.PlayObjectSound();
                    }
                }
                else
                {
                    objectInHand.transform.position = mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (objectInHand != null)
                {
                    if (objectInHand.GetComponent<Ingredient>())
                    {
                        objectInHand.GetComponent<Ingredient>().CheckIfAboveCauldron();
                    }
                    else if (objectInHand.GetComponent<Cauldron>())
                    {
                        objectInHand.GetComponent<Cauldron>().CheckIfAboveCauldron(); //tijdelijke oplossing
                    }
                    else if (objectInHand.GetComponent<Decoration>())
                    {
                        objectInHand.GetComponent<Decoration>().CheckIfOnFlask();
                        objectInHand = null;
                    }
                    else if (objectInHand.GetComponent<Spoon>())
                    {
                        RemoveObjectFromHand();
                    }
                }
            }
        }

        
    }

    public void RemoveObjectFromHand()
    {
        objectInHand.transform.position = pickedUpObjectOriginLocation;
        objectInHand = null;
        AudioManager.instance.PlayObjectSound();
    }
}
