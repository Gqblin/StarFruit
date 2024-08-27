using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirTrigger : MonoBehaviour
{
    public bool isHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spoon"))
        {
            isHit = true;
            GetComponentInParent<Cauldron>().triggered();
        }
    }
}
