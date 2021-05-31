using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HeroData _heroData;
    [SerializeField] private float _waitTime = 1;

    private Animator _animator;

    private Sequence _seq;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        transform.localScale = new Vector3(_heroData.Size, _heroData.Size, _heroData.Size);
        Move();
    }

    private void Move()
    {
        _animator.SetBool(_heroData.MoveBool, false);

        Vector2 destination = GameParameters.RandomScreenPoint();
        float distance = Vector3.Distance(transform.position, destination);

        transform.localScale = destination.x > transform.position.x ? new Vector3(_heroData.Size, transform.localScale.y, transform.localScale.z) : new Vector3(-_heroData.Size, transform.localScale.y, transform.localScale.z);

        _seq = DOTween.Sequence();

        _seq
            .AppendInterval(_waitTime)
            .AppendCallback(() => _animator.SetBool(_heroData.MoveBool, true))
            .Append(transform.DOMove(destination, distance / _heroData.Speed))
            .AppendCallback(Move);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _animator.SetBool(_heroData.AttackBool, true);

            _seq.Kill();
            _seq = DOTween.Sequence();

            _seq
                .AppendInterval(1)
                .AppendCallback(() => _animator.SetBool(_heroData.AttackBool, false))
                .AppendCallback(Move);
        }

    }
}
