using UnityEngine;

public class PickupFruit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {       
        Debug.Log("Picked up " + other.gameObject.name);
    }
}
