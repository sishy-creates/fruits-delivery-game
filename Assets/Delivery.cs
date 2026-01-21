using UnityEngine;

public class Delivery : MonoBehaviour
{

        void OnTriggerEnter2D(Collider2D other) {       
            Debug.Log("Collision detected with " + other.gameObject.name);
        }
    
}
