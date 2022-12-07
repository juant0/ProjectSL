using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private LayerMask playerLayer;
    private void Awake()
    {
        door.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!LayerUtilities.IsSameLayer(playerLayer, other.gameObject.layer))
            return;
        door.SetActive(false);
        gameObject.SetActive(false);
    }

}
