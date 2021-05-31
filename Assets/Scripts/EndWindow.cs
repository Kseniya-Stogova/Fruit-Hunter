using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _newScore;
    [SerializeField] private TMP_Text _highScore;

    public void End(int newScore)
    {
        SaveLoad.Load();

        gameObject.SetActive(true);

        _newScore.text = newScore.ToString();

        if (newScore > SaveLoad.highScore) SaveLoad.highScore = newScore;

        _highScore.text = SaveLoad.highScore.ToString();

        SaveLoad.Save();
    }
}
