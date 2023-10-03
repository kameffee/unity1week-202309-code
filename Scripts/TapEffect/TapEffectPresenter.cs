using Kameffee.Extensions;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202309.TapEffect
{
    public class TapEffectPresenter : Presenter, ITickable
    {
        private readonly TapEffectView _tapEffectView;

        public TapEffectPresenter(TapEffectView tapEffectView)
        {
            _tapEffectView = tapEffectView;
        }

        void ITickable.Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main != null)
                {
                    var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;
                    _tapEffectView.Emit(worldPosition);
                }
            }
        }
    }
}