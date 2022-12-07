using UnityEngine;
[CreateAssetMenu(fileName = "LeveData", menuName = "Custom/LevelData")]
public class LevelData : ScriptableObject, ISerializationCallbackReceiver
{
    [HideInInspector] public Vector3 SpawnPosition  = Vector3.zero;
    [HideInInspector] public Quaternion SpawnDirection = Quaternion.identity;
    [HideInInspector] public bool isCompletedTutorial = false;
    //Reset Values when editor exit play  mode
    public void OnAfterDeserialize()
    { 
        SpawnPosition = Vector3.zero;
        SpawnDirection = Quaternion.identity;
        isCompletedTutorial = false;
    }

    public void OnBeforeSerialize()
    {
    }
}
