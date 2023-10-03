using UnityEngine;

namespace Unity1week202309.TapEffect
{
    public class TapEffectView : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;
        
        public void Emit(Vector3 worldPosition)
        {
            transform.position = worldPosition;
            _particleSystem.Emit(2);
        }
    }
}