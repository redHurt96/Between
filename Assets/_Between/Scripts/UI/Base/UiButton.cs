using UnityEngine;
using UnityEngine.UI;

namespace Between.UI.Base
{
    [RequireComponent(typeof(Button))]
    public abstract class UiButton : MonoBehaviour
    {
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(PerformOnClick);

            Init();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(PerformOnClick);
        }

        protected virtual void Init() { }
        protected abstract void PerformOnClick();
    }
}