using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Zenject;

namespace HT
{
    public class UI_BreedInfo_View : UI_Views
    {
        

        [SerializeField] private Button removeBtn;
        [SerializeField] private Button closeBtn;
        [SerializeField] private Texture2D deleteCursor;

        public Text statText;

        private void OnEnable()
        {
            removeBtn.onClick.AddListener(() =>
            {
                DeleteAction().Forget();
            });
        }

        private void OnDisable()
        {
            removeBtn.onClick.RemoveAllListeners();
        }

        public void SetData(BreedInfoModel model)
        {
            if (statText == null) { return; }
            statText.text = "M " + model._maleCount + " F " + model._femaleCount;
        }

        private async UniTaskVoid DeleteAction()
        {
            bool hover = true;
            mouseMode.mode = MouseMode.Mode.DELETION;
            Routine.Instance.mousePointer.Set(deleteCursor);
            while (hover)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, 200))
                    {
                        if (hit.collider != null)
                        {
                            hit.collider.TryGetComponent(out Animal animal);
                            gameplayEvent.OnGoatKilled?.Invoke(animal);
                            hover = false;
                        }
                    }
                }
                await UniTask.Yield();
            }
            mouseMode.mode = MouseMode.Mode.CAMERA;
            Routine.Instance.mousePointer.Default();
        }
    }
}
