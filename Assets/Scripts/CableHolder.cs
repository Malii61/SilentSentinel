using System;
using System.Collections.Generic;
using UnityEngine;

public class CableHolder : MonoBehaviour
{
    [SerializeField] private List<Transform> cables;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        if (e.CurrentState == GameManager.State.ElectricShutDown)
        {
            ResetEmissions();
        }
    }

    public void ResetEmissions()
    {
        foreach (var cable in cables)
        {
            foreach (var mat in cable.GetComponent<Renderer>().materials)
            {
                mat.SetColor("_EmissionColor", new Color(0, 0, 0));
            }
        }
    }
}