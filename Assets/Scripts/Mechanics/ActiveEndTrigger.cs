using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEndTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Text;
    [SerializeField] private ParticleSystem effect;
    [Tooltip("Layer that the player is assigned")]
    [SerializeField] private LayerMask playerLayer;
    private void Awake()
    {
        Text.SetActive(false);
        effect.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!LayerUtilities.IsSameLayer(playerLayer, other.gameObject.layer))
            return;
        Text.SetActive(true);
        effect.Play();
        GetComponent<Collider>().enabled = false;
    }
}
