using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagersService", menuName = "Services/GameManagersService")]
public class GameManagersService : Service
{
    [SerializeField] private BaseGameManagers _baseGameManagers;
    List<IGameManager> _gameManagerList;

    public override void AddToContainer(IServiceContainer container)
    {
        container.Register<GameManagersService>(this);
        container.Register<Service>(this);
        container.Register<IDisposable>(this);

        _baseGameManagers.Install(container);

        _gameManagerList = container.ResolveList<IGameManager>();
    }

    public override void Initialize()
    {
        foreach (var gameManager in _gameManagerList)
        {
            gameManager.Initialize();
        }
    }

    public override void OnAllServicesInitialized()
    {
        foreach (var gameManager in _gameManagerList)
        {
            gameManager.OnAllServicesInitialized();
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
public abstract class BaseGameManagers : ScriptableObject
{
    public abstract void Install(IServiceContainer container);
}

public interface IGameManager
{
    void Initialize();
    void OnAllServicesInitialized();
    void OnAllManagersInitialized();
    void Dispose();
}


