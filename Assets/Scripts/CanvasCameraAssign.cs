using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasCameraAssign : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
