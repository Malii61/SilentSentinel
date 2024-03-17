using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PersonArg
{
    public PersonType personType;
    public Person person;
}

public enum PersonType
{
    Student_Mark,
    Student_Aj,
    Student_Claire,
    Teacher,
    Chef,
}

public class PersonHolder : MonoBehaviour
{
    public static PersonHolder Instance { get; private set; }
    [SerializeField] private List<PersonArg> _personArgs = new();

    private void Awake()
    {
        Instance = this;
    }

    public Person GetPerson(PersonType personType)
    {
        return _personArgs.FirstOrDefault(person => person.personType == personType).person;
    }
}