using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Architecture.CodeBase.Pool;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using Project.Scripts.Architecture.CodeBase.Services.Save;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Level {
  public interface ILevelGenerator {
    void GenerateLevel(ConfigurationInfo configuration);

    Queue<TileView> Tiles { get; }
  }

  public class LevelGenerator : ILevelGenerator {
    private readonly IDataProvider _dataProvider;
    private readonly TileViewPool _tileViewPool;
    private readonly ILevelAreaSetup _levelAreaSetup;
    private readonly IObstacleProvider _obstacleProvider;

    private ConfigurationInfo _currentConfiguration;
    private TileViewType _selectedViewType;

    public LevelGenerator(ITileViewFactory gameFactory, IDataProvider dataProvider, ILevelAreaSetup levelAreaSetup, IObstacleProvider obstacleProvider) {
      _dataProvider = dataProvider;
      _levelAreaSetup = levelAreaSetup;
      _obstacleProvider = obstacleProvider;
      _tileViewPool = new TileViewPool(() => gameFactory.Create<TileView>(_selectedViewType));
    }

    public void GenerateLevel(ConfigurationInfo configuration) {
      _currentConfiguration = configuration;
      ActualizeLevelData();
      Generate();
    }

    private void ActualizeLevelData() {
      _selectedViewType = _dataProvider.GetSaveData().GetData<PlayerCustomizationData>().TileViewType;
    }

    private void Generate() {
      GenerateFirstTile();

      for (var i = 0; i < _currentConfiguration.TileCount; i++) {
        GenerateTile();
      }
    }

    private void GenerateFirstTile() {
      TileView newTile = _tileViewPool.Get();
      newTile.transform.position = _levelAreaSetup.StartPoint.position;
      Tiles.Enqueue(newTile);
      newTile.transform.SetParent(_levelAreaSetup.TileHolder);
    }

    private void GenerateTile() {
      TileView lastTileView = Tiles.Last();
      Vector3 spawnPos = lastTileView.transform.position + Vector3.forward * lastTileView.Collider.size.x;

      TileView newTile = _tileViewPool.Get();
      newTile.transform.position = spawnPos;
      Tiles.Enqueue(newTile);
      newTile.transform.SetParent(_levelAreaSetup.TileHolder);
      _obstacleProvider.SpawnRandomObstacle(newTile);
    }

    public Queue<TileView> Tiles { get; } = new();
  }
}