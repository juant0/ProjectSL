using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    [Tooltip("Layer that the player is assigned")]
    [SerializeField] private LayerMask playerLayer;
    private void OnCollisionEnter(Collision collision)
    {
        if (!LayerUtilities.IsSameLayer(playerLayer, collision.gameObject.layer))
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
