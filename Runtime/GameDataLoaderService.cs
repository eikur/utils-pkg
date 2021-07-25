using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataLoaderService", menuName = "Services/GameDataLoaderService")]
public class GameDataLoaderService : Service
{
    [SerializeField] private BaseGameConfig _gameConfig;
    [SerializeField] private BaseGameModel _gameModel;

    public BaseGameConfig Config => _gameConfig;
    public BaseGameModel Model => _gameModel;

    public override void AddToContainer(IServiceContainer container)
    {
        var newGameDataLoader = ScriptableObject.CreateInstance<GameDataLoaderService>();
        newGameDataLoader._gameConfig = _gameConfig;
        newGameDataLoader._gameModel = _gameModel;
        container.Register<GameDataLoaderService>(newGameDataLoader);
        container.Register<Service>(newGameDataLoader);
        container.Register<IDisposable>(newGameDataLoader);
        
    }
}
