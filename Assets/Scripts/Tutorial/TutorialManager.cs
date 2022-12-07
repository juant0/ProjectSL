using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Tooltip("Order of tutorial texts ")]
    [SerializeField] private GameObject[] tutorialsText;
    private LevelManager levelManager;
    private int index = 0;
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        SetActiveTutorialTexts();
    }

    private void SetActiveTutorialTexts()
    {
        tutorialsText[0].SetActive(true);
        for (int i = 1; i < tutorialsText.Length; i++)
            tutorialsText[i].SetActive(false);
    }

    public void NextTutorialText() 
    {
        if (index >= tutorialsText.Length)
            return;
        tutorialsText[index].SetActive(false);

        if (index >= tutorialsText.Length - 1)
        {
            levelManager.Data.isCompletedTutorial = true;
            return;
        }
        index++;  
        tutorialsText[index].SetActive(true);

    }
}
