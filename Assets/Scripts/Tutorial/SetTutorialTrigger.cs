using UnityEngine;

public class SetTutorialTrigger : MonoBehaviour
{
    [Tooltip("Layer that the player is assigned")]
    [SerializeField] private LayerMask playerLayer;
    private TutorialManager tutorialManager;
    private void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!LayerUtilities.IsSameLayer(playerLayer, other.gameObject.layer))
            return;
        tutorialManager?.NextTutorialText();
        gameObject.SetActive(false);
    }

}
