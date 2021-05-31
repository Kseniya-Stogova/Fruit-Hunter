using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "Fruit Hunter/Hero parameters", order = 52)]
public class HeroData : ScriptableObject
{
    [Range(1, 6)]
    [SerializeField] private int _speed = 1;
    [Range(0.1f, 1)]
    [SerializeField] private float _size = 1;
    [Header("Animations")]
    [SerializeField] private string _moveBool = "Move";
    [SerializeField] private string _attackBool = "Attack";

    public int Speed => _speed;
    public float Size => _size;
    public string MoveBool => _moveBool;
    public string AttackBool => _attackBool;
}
