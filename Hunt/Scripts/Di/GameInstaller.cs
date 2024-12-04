using HT;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private MouseMode mouseMode;
    [SerializeField] private BankAccount bankAccount;
    [SerializeField] private GlobalData globalData;
    [SerializeField] private AppEvent appEvent;
    [SerializeField] private AudioEvent audioEvent;
    [SerializeField] private GameplayEvent gameplayEvent;
    [SerializeField] private UIEvents uIEvents;


    public override void InstallBindings()
    {
        Container.Bind<MouseMode>().FromInstance(mouseMode).AsSingle();
        Container.Bind<BankAccount>().FromInstance(bankAccount).AsSingle();
        Container.Bind<GlobalData>().FromInstance(globalData).AsSingle();
        Container.Bind<AppEvent>().FromInstance(appEvent).AsSingle();
        Container.Bind<AudioEvent>().FromInstance(audioEvent).AsSingle();
        Container.Bind<GameplayEvent>().FromInstance(gameplayEvent).AsSingle();
        Container.Bind<UIEvents>().FromInstance(uIEvents).AsSingle();
    }
}