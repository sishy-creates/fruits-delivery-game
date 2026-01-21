using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class DeliverMan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 5f;
    [SerializeField] TMP_Text BoostText;
    [SerializeField] TMP_Text DeliveryCompleted;
    [SerializeField] TMP_Text gameFinishedText;

    private bool stawberryIsDelivered = false;
    private bool bananaIsDelivered = false;
    private bool blueberryIsDelivered = false;
    private bool coconutIsDelivered = false;
    private bool hasFruit = false;
    private string currentFruit = "";
    private bool hasTurbo = false;
    // Update is called once per frame

    void Start()
    {
        BoostText.gameObject.SetActive(false);
        DeliveryCompleted.gameObject.SetActive(false);
        gameFinishedText.gameObject.SetActive(false);
    }
    void Update()
    {
        float x = 0f;
        float y = 0f;

        if(Keyboard.current.wKey.isPressed)
        {
            y = 1f;
        }
        else if(Keyboard.current.sKey.isPressed)
        {
            y = -1f;
        }
        else if(Keyboard.current.aKey.isPressed)
        {
            x = -1f;
        }
        else if(Keyboard.current.dKey.isPressed)
        {
            x = 1f;
        }

        Vector3 delta = new Vector3(x, y, 0f) * speed * Time.deltaTime;
        transform.Translate(delta, Space.World);
        if(stawberryIsDelivered && bananaIsDelivered && blueberryIsDelivered && coconutIsDelivered)
        {
            gameFinishedText.gameObject.SetActive(true);
        }

    }

        IEnumerator TurboSpeed()
    {
        speed = speed * 2;
        yield return new WaitForSeconds(3);
        speed = speed / 2;
        hasTurbo = false;
        GetComponent<ParticleSystem>().Stop();
        BoostText.gameObject.SetActive(false);

    }

    
        IEnumerator DeliveryCompletedCo()
    {
        yield return new WaitForSeconds(3);
        DeliveryCompleted.gameObject.SetActive(false);

    }

    void OnTriggerEnter2D(Collider2D other) { 
        Debug.Log("Collision detected with " + other.gameObject.name + hasFruit + currentFruit);
        if ((other.CompareTag("Banana") || other.CompareTag("Strawberry") || other.CompareTag("Blueberry") || other.CompareTag("Coconut")) && !hasFruit){
            hasFruit = true;
            currentFruit = other.gameObject.name;
            Debug.Log("Picked up " + other.gameObject.name);
            Destroy(other.gameObject);
        }

       if(other.CompareTag("CharacterStrawberry") && hasFruit && currentFruit.Contains("Strawberry")){
            hasFruit = false;
            Debug.Log("Delivered " + currentFruit + " to " + other.gameObject.name);
            currentFruit = "";
            DeliveryCompleted.gameObject.SetActive(true);
            StartCoroutine(DeliveryCompletedCo());
            stawberryIsDelivered = true;
        }
        else if(other.CompareTag("CharacterBanana") && hasFruit && currentFruit.Contains("Banana")){
            hasFruit = false;
            Debug.Log("Delivered " + currentFruit + " to " + other.gameObject.name);
            currentFruit = "";
            DeliveryCompleted.gameObject.SetActive(true);
            StartCoroutine(DeliveryCompletedCo());
            bananaIsDelivered = true;
        }
        else if(other.CompareTag("CharacterBlueBerry") && hasFruit && currentFruit.Contains("Blueberry")){
            hasFruit = false;
            Debug.Log("Delivered " + currentFruit + " to " + other.gameObject.name);
            currentFruit = "";
            DeliveryCompleted.gameObject.SetActive(true);
            StartCoroutine(DeliveryCompletedCo());
            blueberryIsDelivered = true;
        }
        else if(other.CompareTag("CharacterCoconut") && hasFruit && currentFruit.Contains("Coconut")){
            hasFruit = false;
            Debug.Log("Delivered " + currentFruit + " to " + other.gameObject.name);
            currentFruit = "";
            DeliveryCompleted.gameObject.SetActive(true);
            StartCoroutine(DeliveryCompletedCo());
            coconutIsDelivered = true;
        }
        else if (other.CompareTag("gold"))
        {
            Debug.Log("Collected gold from " + other.gameObject.name);
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            hasTurbo = true;
            StartCoroutine(TurboSpeed());
            BoostText.gameObject.SetActive(true);
        }
    }
}

