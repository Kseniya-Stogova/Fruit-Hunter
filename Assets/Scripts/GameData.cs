using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data", menuName = "Fruit Hunter/Game Data", order = 52)]
public class GameData : ScriptableObject
{
    [Range(1, 5)]
    [SerializeField] private int _maxHP = 1;
    [Range(1, 10)]
    [SerializeField] private int _fruitsMax = 1;
    [Range(1, 5)]
    [SerializeField] private int _minFruitSpawnTime = 1;
    [Range(1, 10)]
    [SerializeField] private int _maxFruitSpawnTime = 1;
    [Range(1, 6)]
    [SerializeField] private int _enemiesCount = 3;

    public int MaxHP => _maxHP;
    public int FruitsMax => _fruitsMax;
    public int MinFruitSpawnTime => _minFruitSpawnTime;
    public int MaxFruitSpawnTime => _maxFruitSpawnTime;
    public int EnemiesCount => _enemiesCount;
}
