using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewGameData", menuName = "GameData")]

public class GameData_SO : ScriptableObject
{
    public UnityEvent onUpdateHUD;
}
