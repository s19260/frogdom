using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col; 
    public int colliderCount;


    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        detectedColliders.Add(collider);
        //colliderCount = detectedColliders.Count;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        detectedColliders.Remove(collider);
      // colliderCount = detectedColliders.Count;
    }
}
