using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Image questStatusImage;
    [SerializeField] private Color[] questStatusColors;

    private AQuest _currentQuest;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        _currentQuest = new HuntQuest(4, 10);
        _currentQuest.Start(6);
        StartCoroutine(CheckQuestRepeating());
    }

    private bool CheckQuestStatus(AQuest quest)
    {
        questStatusImage.color = questStatusColors[((int)quest.IsFinished()) - 1];
        questStatusImage.fillAmount = _currentQuest.CurrentProgress();
        return quest.IsFinished() > AQuest.QuestStatus.InProgress;
    }

    IEnumerator CheckQuestRepeating()
    {
        while (!CheckQuestStatus(_currentQuest))
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}