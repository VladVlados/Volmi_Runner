namespace Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes {
  public class PlayerProgressData : PlayerSavedData {
    private int _playerRank;

    public override void Setup(IDataSaver saver) { }

    public void IncreaseRank(int value) {
      _playerRank += value;
      Save();
    }

    public void DecreaseRank(int value, string itemName) {
      _playerRank -= value;
      Save();
    }

    public int GetRank() {
      return _playerRank;
    }
  }
}