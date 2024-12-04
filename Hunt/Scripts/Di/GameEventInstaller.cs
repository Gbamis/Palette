using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameEventInstaller", menuName = "Installers/GameEventInstaller")]
public class GameEventInstaller : ScriptableObjectInstaller<GameEventInstaller>
{
    public override void InstallBindings()
    {
    }
}