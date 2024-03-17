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
    private int currentTaskIndex;
    private TaskArg currentTask;

    private void Awake()
    {
        Instance = this;
    }

    public void PassToNextTask()
    {
        currentTaskIndex++;
        if (currentTaskIndex >= _taskArgs.Count)
        {
            Debug.Log("KAZANDIN");
        }

        currentTask = _taskArgs[currentTaskIndex];
        ShowCurrentTask();
    }

    private void ShowCurrentTask()
    {
        taskTMP.text = currentTask.task;
    }
}