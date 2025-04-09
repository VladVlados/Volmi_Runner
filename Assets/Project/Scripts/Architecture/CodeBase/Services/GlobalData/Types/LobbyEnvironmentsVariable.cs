using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types {
  [CreateAssetMenu(fileName = "LobbyEnvironmentsVariable", menuName = "GlobalData/LobbyEnvironmentsVariable")]
  public class LobbyEnvironmentsVariable : ItemGlobalData {
    [SerializeField]
    private List<LobbyEnvironment> _lobbyEnvironments;

    public LobbyEnvironment FindEnvironmentByName(EnvironmentName target) {
      return _lobbyEnvironments.First(environment => environment.EnvironmentName == target);
    }
  }

  [Serializable]
  public class LobbyEnvironment {
    [SerializeField]
    private EnvironmentName _name;
    [SerializeField]
    private GameObject _prefab;

    public EnvironmentName EnvironmentName {
      get {
        return _name;
      }
    }

    public GameObject Prefab {
      get {
        return _prefab;
      }
    }
  }

  public enum EnvironmentName {
    Default = 0,
    Sandbox = 1
  }
}