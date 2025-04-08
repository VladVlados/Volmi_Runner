using Project.Scripts.Architecture.CodeBase.Services;

namespace Project.Scripts.Architecture.CodeBase.UI.LoadScreen {
  public interface ILoaderScreenService  : IService{
    void StartIntro();
    
    void HideIntro();
    
    bool IsIntroPlaying { get; }
  }
}