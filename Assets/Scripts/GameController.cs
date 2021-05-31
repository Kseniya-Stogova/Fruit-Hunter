using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject _allHP;
    [SerializeField] private GameObject _healthPrefab;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Fruit _fruitPrefab;
    [SerializeField] private PlayerController _playerControllerPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private EndWindow _endWindow;

    private int _score;

    private PlayerController _player;

    private List<Fruit> _fruits = new List<Fruit>();

    private List<GameObject> _health = new List<GameObject>();

    public void Begin()
    {
        _player = Instantiate(_playerControllerPrefab);

        for (int i = 1; i < _gameData.EnemiesCount; i++)
        {
            Enemy enemy = Instantiate(_enemyPrefab);
            enemy.transform.position = GameParameters.RandomScreenPoint();
        }

        for (int i = 0; i < _gameData.MaxHP; i++)
        {
            GameObject health = Instantiate(_healthPrefab);
            health.transform.SetParent(_allHP.transform);
            health.transform.localScale = Vector3.one;
            _health.Add(health);
        }

        _player.hit += Hit;
        _player.get += Get;

        MakeFruit();
        Generate();
    }

    private void Hit()
    {
        if (_health.Count == 0) return;

         Destroy(_health.Last());
        _health.RemoveAt(_health.Count - 1);

        if (_health.Count == 0) GameOver();
    }

    private void Get(Fruit fruit)
    {
        _fruits.Remove(fruit);
        _score++;
        _scoreText.text = _score.ToString();
    }

    private void GameOver()
    {
        _endWindow.End(_score);
    }

    private void Generate()
    {
        Sequence seq = DOTween.Sequence();

        seq
            .AppendInterval(Random.Range(_gameData.MinFruitSpawnTime, _gameData.MaxFruitSpawnTime))
            .AppendCallback(MakeFruit)
            .AppendCallback(Generate);
    }

    private void MakeFruit()
    {
        if (GameObject.FindGameObjectsWithTag("Fruit").Length >= _gameData.FruitsMax) return;

        Fruit fruit = Instantiate(_fruitPrefab);
        fruit.transform.position = GameParameters.RandomScreenPoint();

        _fruits.Add(fruit);
    }
}
