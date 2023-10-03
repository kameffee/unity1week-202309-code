using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Kameffee.Audio;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309
{
    public class BreakableObject : MonoBehaviour, IAttackReceivable
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private int _initialHp = 1;

        [SerializeField]
        private List<BreakableObject> _chainBreakableObjects;

        [SerializeField]
        private string _attackReceivedSoundKey = "BreakableObject/AttackReceived";

        public bool Broken => _currentHp <= 0;

        private int _currentHp;

        [Inject]
        private readonly AudioPlayer _audioPlayer;

        private void Awake()
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _currentHp = _initialHp;
        }

        private void Start()
        {
            var lifetimeScope = LifetimeScope.Find<LifetimeScope>();
            lifetimeScope.Container.Inject(this);
        }

        public void OnReceiveAttack()
        {
            if (Broken) return;

            if (_chainBreakableObjects.Any(breakable => !breakable.Broken))
            {
                return;
            }

            PlayBreakSound();

            _currentHp = Mathf.Max(_currentHp - 1, 0);

            transform.DOShakePosition(0.16f,
                strength: 0.08f,
                vibrato: 6,
                snapping: false,
                fadeOut: true);

            if (_currentHp <= 0)
            {
                Break();
            }
        }

        private void Break()
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            Observable.Timer(TimeSpan.FromSeconds(5f))
                .Take(1)
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);
        }

        private void PlayBreakSound()
        {
            if (string.IsNullOrEmpty(_attackReceivedSoundKey)) return;
            _audioPlayer.PlaySe(_attackReceivedSoundKey);
        }
    }
}