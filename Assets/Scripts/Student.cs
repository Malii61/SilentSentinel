using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Student : Person
{
    protected Collider col;
    protected override void Start()
    {
        base.Start();
        col = GetComponent<CapsuleCollider>();
        
    }



}