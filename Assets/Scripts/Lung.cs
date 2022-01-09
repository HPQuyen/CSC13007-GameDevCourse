using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lung : MonoBehaviour
{
    private void Start()
    {
        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var heartUI = FindObjectOfType<HeartUI>();
        heartUI.OnChangedValueHeart(heartUI.CurrentHeart - 1);
    }
}
