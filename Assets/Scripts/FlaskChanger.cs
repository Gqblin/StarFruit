using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskChanger : MonoBehaviour
{
    [SerializeField] private Flasks flaskType;

    private void OnMouseDown()
    {
        Flask flask = FindObjectOfType<Flask>();
        //flask.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        flask.ChangeHardIngridients(flaskType);
    }
}
