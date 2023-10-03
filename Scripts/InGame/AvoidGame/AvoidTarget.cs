using System;
using UnityEngine;

namespace Unity1week202309.InGame.AvoidGame
{
    public class AvoidTarget : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _center;

        [SerializeField]
        private Vector2 _size;

        public float MoveSpeed { get; private set; }

        private Vector3 _lastPosition;

        private void Update()
        {
            MoveSpeed = Vector3.Distance(_lastPosition, transform.position) / Time.deltaTime;
            _lastPosition = transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_center, _size);
        }
    }
}