using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lung : MonoBehaviour
{
    public Transform destination;

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var heartUI = FindObjectOfType<HeartUI>();
            heartUI.OnChangedValueHeart(heartUI.CurrentHeart - 1);
            Destroy(other.gameObject);
        }
    }
}
