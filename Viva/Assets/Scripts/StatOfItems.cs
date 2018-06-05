using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //so fields appear in inspector
public class StatOfItems {

    [SerializeField] //field appears in inspector
    private int baseValue;

    public int GetValue()
    {
        return baseValue;
    }
}
