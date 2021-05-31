using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private float _size = 0.3f;
    [SerializeField] private float _sizePulse = 1.3f;
    [SerializeField] private float _sizeSpeed = 1;
    [SerializeField] private List<Sprite> _sprites;

    private void Awake()
    {
        int random = Random.Range(0, _sprites.Count);
        GetComponent<SpriteRenderer>().sprite = _sprites[random];

        transform.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        seq
            .Append(transform.DOScale(_size * _sizePulse, (_size * _sizePulse) / _sizeSpeed))
            .Append(transform.DOScale(_size, (transform.localScale.x - _size * _sizePulse) / _sizeSpeed)) //TODO: speed
            .AppendCallback(Shake);
    }

    private void Shake()
    {
        Sequence seq = DOTween.Sequence();
        seq
            .Append(transform.DOShakeRotation(1, new Vector3(0, 0, 30), 5)) //для ускорения разработки используется хард программирование
            .AppendInterval(2)
            .AppendCallback(Shake);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform.tag == "Player")
        {
            Sequence seq = DOTween.Sequence();
            seq
                .Append(transform.DOScale(0, transform.localScale.x / _sizeSpeed))
                .AppendCallback(() => Destroy(gameObject));
        }
    }
}
