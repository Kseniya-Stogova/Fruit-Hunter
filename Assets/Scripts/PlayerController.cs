using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HeroData _heroData;
    [SerializeField] private GameObject _child;
    [SerializeField] private float _immortalityTime = 3; 
    [SerializeField] private float _blinkFrequency = 10;

    public event Action hit;
    public event Action<Fruit> get;

    private Animator _animator;
    private Collider2D _collider;
    
    private Sequence _seq;
    private Sequence _seqBlink;

    private bool _blink;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        transform.localScale = new Vector3(_heroData.Size, _heroData.Size, _heroData.Size);
        Immortality();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) Move();
    }

    private void Move()
    {
        Vector3 cursorPosition = Input.mousePosition;
        Vector3 viewport = Camera.main.ScreenToViewportPoint(cursorPosition);

        if (viewport.x < 0 || viewport.y < 0 || viewport.x > 1 || viewport.y > 1) return;

        _animator.SetBool(_heroData.MoveBool, true);

        Vector2 destination = Camera.main.ScreenToWorldPoint(cursorPosition);
        float distance = Vector3.Distance(transform.position, destination);

        transform.localScale = destination.x > transform.position.x ? new Vector3(_heroData.Size, transform.localScale.y, transform.localScale.z) : new Vector3(-_heroData.Size, transform.localScale.y, transform.localScale.z);

        _seq.Kill();
        _seq = DOTween.Sequence();

        _seq
            .Append(transform.DOMove(destination, distance / _heroData.Speed))
            .AppendCallback(() => _animator.SetBool(_heroData.MoveBool, false));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            get?.Invoke(collision.gameObject.GetComponent<Fruit>());
        }

        if (_seqBlink.IsActive()) return;

        if (collision.gameObject.tag == "Enemy")
        {
            hit?.Invoke();
            Immortality();
        }
    }

    private void Immortality()
    {
        _blink = true;
        //_collider.enabled = false;

        Sequence seq = DOTween.Sequence();

        seq
            .AppendCallback(Blink)
            .AppendInterval(_immortalityTime)
            .AppendCallback(() => _blink = false)
            .AppendCallback(() => _collider.enabled = true);
    }

    private void Blink()
    {
        if (_blink)
        {
            _seqBlink.Kill();
            _seqBlink = DOTween.Sequence();

            _seqBlink
                .AppendCallback(() => _child.gameObject.SetActive(false))
                .AppendInterval(1 / _blinkFrequency)
                .AppendCallback(() => _child.gameObject.SetActive(true))
                .AppendInterval(1 / _blinkFrequency)
                .AppendCallback(Blink);
        }
    }
}
