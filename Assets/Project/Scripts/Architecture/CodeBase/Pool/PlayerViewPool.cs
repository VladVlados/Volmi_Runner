using System;
using Project.Scripts.Architecture.CodeBase.Gameplay.Player;

namespace Project.Scripts.Architecture.CodeBase.Pool {
  public class PlayerViewPool : ObjectPool<PlayerView>{
    public PlayerViewPool(Func<PlayerView> objectGenerator) : base(objectGenerator) { }
  }
}