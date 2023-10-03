using UnityEngine;

namespace Unity1week202309.InGame.AvoidGame
{
    public class MouseCollision : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        
        public void Update()
        {
            transform.position = _camera.ScreenToWorldPoint(Input.mousePosition); 
        }
    }
}