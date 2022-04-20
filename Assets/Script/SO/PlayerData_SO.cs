using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "PlayerData")]

public class PlayerData_SO : ScriptableObject, ISerializationCallbackReceiver
{
    private int scoreInit = 0;
    public int score;
    private float healthInit = 100;
    public float health;

    public void OnAfterDeserialize()
    {
        score = scoreInit;
        health = healthInit;
    }

    public void OnBeforeSerialize()
    {

    }
}
