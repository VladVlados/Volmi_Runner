using System;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay;

namespace Project.Scripts.Architecture.CodeBase.Pool {
  public class RewardItemViewUIPool : ObjectPool<RewardItemViewUI> {
    public RewardItemViewUIPool(Func<RewardItemViewUI> objectGenerator) : base(objectGenerator) { }
  }
}