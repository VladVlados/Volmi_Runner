using Zenject;

namespace Project.Scripts.Architecture.CodeBase.Signal {
  public class SceneReadySignal {
    public DiContainer SceneContainer;

    public SceneReadySignal(DiContainer container) {
      SceneContainer = container;
    }
  }
}