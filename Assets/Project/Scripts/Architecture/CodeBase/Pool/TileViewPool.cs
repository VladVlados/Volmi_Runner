using System;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;

namespace Project.Scripts.Architecture.CodeBase.Pool {
  public class TileViewPool : ObjectPool<TileView> {
    public TileViewPool(Func<TileView> objectGenerator) : base(objectGenerator) { }
  }
}