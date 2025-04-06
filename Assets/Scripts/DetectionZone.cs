using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> collidersInZone = new List<Collider2D>();
    Collider2D detectionCol;
    public int colliderCount;

    // Start is called before the first frame update
    void Start()
    {
        detectionCol = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        collidersInZone.Add(collider);
        colliderCount = collidersInZone.Count;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        collidersInZone.Remove(collider);
        colliderCount = collidersInZone.Count;
    }
}
