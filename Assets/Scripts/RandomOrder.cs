using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class RandomOrder : MonoBehaviour
{

    [SerializeField] private int numberOfIngredients = 5;
    private WaitForSeconds delay = new WaitForSeconds(1f);
    [SerializeField] private List<Ingredients> ingredients = new List<Ingredients>();
    [SerializeField] TMP_Text orderText;

    public void TakeOrder()
    {
        StartCoroutine(GenerateAndPrintRandomOrder());
    }

    private IEnumerator GenerateAndPrintRandomOrder()
    {
        string result = "I want: \n";

        for (int i = 0; i < numberOfIngredients; i++)
        {
            if (i < numberOfIngredients)
            {
                Ingredients randomIngredient = GetRandomEnum<Ingredients>();
                result += $"- {randomIngredient} \n";
                ingredients.Add(randomIngredient);
                yield return delay;
            }
            // Remove the trailing " + " at the end
            orderText.text = result;
        }
            //result = result.TrimEnd(' ', '+');
            orderText.text = result;
    }

    private T GetRandomEnum<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeOrder();
        }
    }

}
