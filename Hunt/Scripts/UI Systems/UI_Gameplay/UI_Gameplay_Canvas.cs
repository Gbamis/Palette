using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace HT
{
    public class UI_Gameplay_Canvas : UICanvas
    {

        private BreedInfoModel breedInfoModel;

        [SerializeField] private UI_Goat_Info_View ui_goatInfo;
        [SerializeField] private UI_BreedInfo_View ui_breedInfo;
        [SerializeField] private UI_CoinAdded_View ui_coinAdded_view;
        [SerializeField] private UI_Coin_View ui_coinView;


        public float bouceDuration = .5f;
        public float returnDuration = 1;


        public override void OnInit()
        {
            base.OnInit();
            ui_coinView.UpdateAccount_UI();

            ui_goatInfo.gameObject.SetActive(false);

            uiEvent.OnShowGoatInfo += Event_ShowGoatInfo;
            uiEvent.OnHideGoatInfo += Event_HideGoatInfo;
            gameplayEvent.OnBreedStatChanged += Event_BreedStatChanged;
            bankAccount.OnAccountChanged += Event_BankAccountChanged;
        }

        public void OnDisable()
        {
            uiEvent.OnShowGoatInfo -= Event_ShowGoatInfo;
            uiEvent.OnHideGoatInfo -= Event_HideGoatInfo;
            gameplayEvent.OnBreedStatChanged -= Event_BreedStatChanged;
            bankAccount.OnAccountChanged -= Event_BankAccountChanged;
        }

        private void Event_ShowGoatInfo(GoatInfoModel model)
        {
            ui_goatInfo.SetData(model);
            ui_goatInfo.gameObject.SetActive(true);
            ui_goatInfo.transform.DOScale(1.3f, bouceDuration)
            .SetEase(Ease.OutBounce).OnComplete(() => ui_goatInfo.transform.DOScale(1.1f, returnDuration));
        }
        private void Event_HideGoatInfo() => ui_goatInfo.gameObject.SetActive(false);
        private void Event_BreedStatChanged(int m, int f) => breedInfoModel.PresentView(ui_breedInfo, m, f);
        private void Event_BankAccountChanged(int value)
        {
            ui_coinView.UpdateAccount_UI();
            CreateCoinAddedView(value).Forget();
        }


        private async UniTaskVoid CreateCoinAddedView(int val)
        {
            UI_CoinAdded_View coin = Instantiate(ui_coinAdded_view, transform);
            coin.SetVallue(val);
            coin.gameObject.SetActive(true);
            SFX.Core.CoinAdded();
            await coin.transform.DOLocalMoveY(4, 1);
            Destroy(coin.gameObject);

        }

    }
}
