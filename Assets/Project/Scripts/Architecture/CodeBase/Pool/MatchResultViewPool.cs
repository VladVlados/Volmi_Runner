using System;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby.MatchResults;

namespace Project.Scripts.Architecture.CodeBase.Pool {
  public class MatchResultViewPool : ObjectPool<MatchResultView> {
    public MatchResultViewPool(Func<MatchResultView> objectGenerator) : base(objectGenerator) { }
  }
}