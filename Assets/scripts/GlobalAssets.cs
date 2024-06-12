using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAssets : MonoBehaviour
{
    public GameObject P1Back;
    public GameObject P2Back;
    public Text P1Hand;
    public Text P2Hand;
    public GameObject mainCard;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BringToFront(P1Back); // Bring P1Back to front
        BringToFront(P2Back); // Bring P2Back to front
    }

    void BringToFront(GameObject obj)
    {
        if (obj != null)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = 100; // Set a higher sorting order value
            }
            else
            {
                Debug.LogError("SpriteRenderer component not found on " + obj.name);
            }
        }
        else
        {
            Debug.LogError("GameObject is null");
        }
    }
}
