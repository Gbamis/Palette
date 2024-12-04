using UnityEngine.UI;

namespace HT
{
    public class UI_Breeding_Canvas : UICanvas
    {
        public BreedingSystem breedingSystem;
        public Button continueBtn;


        public override void OnInit()
        {
            base.OnInit();
            continueBtn.onClick.AddListener(() => GenerateRandom());
        }

        public void OnDisable()
        {
            continueBtn.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }

        private void GenerateRandom()
        {
            breedingSystem.GenerateRandomFirstGeneration();
            appEvent.Raise_OnApp_GameStarted();
            gameObject.SetActive(false);
        }
    }

}