using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    [SerializeField] private Decorations decoType;

    public void CheckIfOnFlask()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        //print(colliders.Length);
        GameObject flask = FindObjectOfType<Flask>().gameObject;
        bool onFlask = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject == flask)
            {
                onFlask = true;
                break;
            }
        }

        if (onFlask)
        {
            transform.SetParent(flask.transform, true);
            Destroy(GetComponent<Decoration>());
            Destroy(GetComponent<CircleCollider2D>());
            flask.GetComponent<Flask>().decorations.Add(decoType);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
