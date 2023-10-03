using System;
using UnityEngine;

namespace Unity1week202309.InGame.AvoidGame
{
    public class SynchronizeMousePosition : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        private void Update()
        {
            var mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            transform.position = mouseWorldPosition;
        }
    }
}