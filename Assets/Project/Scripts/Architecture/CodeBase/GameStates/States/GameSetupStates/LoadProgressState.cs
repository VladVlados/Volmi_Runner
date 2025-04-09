namespace Project.Scripts.Architecture.CodeBase.GameStates.States.GameSetupStates {
  public class LoadProgressState : State {
    public override void Enter() {
      base.Enter();

      IsActive = false;
    }
  }
}