using UnityEngine;

public class LevelManager: MonoBehaviour
{
    [field:SerializeField] public LevelData Data { get; private set; }
    [SerializeField] private PlayerController playerController;
    private void Start()
    {
        if (Data.SpawnDirection == Quaternion.identity && Data.SpawnPosition == Vector3.zero)
            return;
        playerController.transform.position = Data.SpawnPosition;
        playerController.transform.rotation = Data.SpawnDirection;
    }
}
