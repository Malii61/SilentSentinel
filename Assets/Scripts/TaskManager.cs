using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class TaskArg
{
    public string task;
}

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    [SerializeField] private List<TaskArg> _taskArgs = new();
    [SerializeField] private TextMeshProUGUI taskTMP;
    private int currentTaskIndex = -1;
    private TaskArg currentTask;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += OnStageChanged;
    }

    private void OnStageChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        PassToNextTask();
    }

    public void PassToNextTask()
    {
        currentTaskIndex++;
        if (currentTaskIndex >= _taskArgs.Count)
        {
            Debug.Log("KAZANDIN");
        }
        taskTMP.fontStyle = FontStyles.Strikethrough;
        currentTask = _taskArgs[currentTaskIndex];
        Invoke(nameof(ShowCurrentTask), 3f);
    }

    private void ShowCurrentTask()
    {
        taskTMP.fontStyle = FontStyles.Bold;
        taskTMP.text = currentTask.task;
    }

    private void Hide()
    {
        taskTMP.enabled = false;
    }
}