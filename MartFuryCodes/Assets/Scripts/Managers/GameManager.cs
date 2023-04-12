using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int currentLevel;
    private void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("level", 1);
    }
    public int WhatLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = PlayerPrefs.GetInt("level", 1);
        }
    }

}
