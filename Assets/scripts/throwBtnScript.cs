using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static global;
using static GlobalGameController;

public class throwBtnScript : MonoBehaviour
{
    int playerNum;
    public GameObject colliderP1;
    public GameObject colliderP2;

    public Material materialToChange1;
    public Material materialToChange2;

    Collider2D colP1;
    Collider2D colP2;
    GlobalGameController gameController;
    void Start()
    {

        GameObject gameControllerObject = GameObject.Find("GameController");
        colP1 = colliderP1.GetComponent<Collider2D>();
        colP2 = colliderP2.GetComponent<Collider2D>();
        if (gameControllerObject != null)
        {
            // Get the GameController component from the found GameObject
            gameController = gameControllerObject.GetComponent<GlobalGameController>();

            if (gameController == null) {
                Debug.LogError("GameController component not found on the GameController GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject named 'GameController' not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (materialToChange1 == null)
            {
                Renderer renderer = colliderP1.GetComponent<Renderer>();
                Renderer renderer1 = colliderP2.GetComponent<Renderer>();
                if (renderer != null)
                {
                    materialToChange1 = renderer.material;

                }
                if(renderer1 != null)
                {
                    materialToChange2 = renderer1.material;
                }
            }
            Vector2 worldPoint;

            if (Input.GetMouseButtonDown(0)) // For mouse click
            {
                worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("clicked ");
            }
            else // For touch
            {
                worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Debug.Log("pressed ");
            }

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                gameController.throwTopCard();
                if (gameController.turnOf == 0)
                {
                    Color color = materialToChange1.color;
                    color.a = 0.5f;
                    materialToChange1.color = color;
                    colP1.enabled = false;
                    colP2.enabled = true;
                    Color color2 = materialToChange2.color;
                    color2.a = 1f;
                    materialToChange2.color = color2;
                }
                else
                {
                    Color color2 = materialToChange2.color;
                    color2.a = 0.5f;
                    materialToChange2.color = color2;
                    colP1.enabled = true;
                    colP2.enabled = false;
                    Color color = materialToChange1.color;
                    color.a = 1f;
                    materialToChange1.color = color;
                }
                Debug.Log("Sprite clicked or tapped!");
                
            }
        }
    } 

}
