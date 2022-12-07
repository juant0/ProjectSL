using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Tooltip("Transform position and rotation where the player will be spawm when he dies")]
    [SerializeField] private Transform spawnTransform = default;
    [Tooltip("Layer that the player is assigned")]
    [SerializeField] private LayerMask playerLayer;
    [Tooltip("GameObject that contains the text to hide when player enters")]
    [SerializeField] private GameObject Text;
    private LevelManager levelManager;
    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!LayerUtilities.IsSameLayer(playerLayer, other.gameObject.layer))
            return;
        levelManager.Data.SpawnPosition = spawnTransform.position;
        levelManager.Data.SpawnDirection = spawnTransform.rotation;
        Text.SetActive(false);
    }

}
