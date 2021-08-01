using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameInstallerService", menuName = "Services/GameInstallerService")]
public class GameInstallerService : Service
{
    [SerializeField] private BaseGameInstaller _baseGameInstaller;
    List<IGameManager> _gameManagerList;

    public override void AddToContainer(IServiceContainer container)
    {
        container.Register<GameInstallerService>(this);
        container.Register<Service>(this);
        container.Register<IDisposable>(this);

        _baseGameInstaller.Install(container);

        _gameManagerList = container.ResolveList<IGameManager>();
    }

    public override void OnAllServicesInitialized()
    {
        foreach (var gameManager in _gameManagerList)
        {
            gameManager.Initialize();
        }
        OnAllManagersInitialized();
    }

    public void OnAllManagersInitialized()
    {
        foreach (var gameManager in _gameManagerList)
        {
            gameManager.OnAllManagersInitialized();
        }
    }

    public override void Dispose()
    {
        foreach (var gameManager in _gameManagerList)
        {
            gameManager.Dispose();
        }
    }
}

[Serializable]
public abstract class BaseGameInstaller : ScriptableObject
{
    public abstract void Install(IServiceContainer container);
}

public interface IGameManager : IInitializable
{
    void OnAllManagersInitialized();
    void Dispose();
}


