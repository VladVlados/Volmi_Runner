using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Architecture.CodeBase.Services.GlobalData {
  public class GlobalDataService : IGlobalDataService {
    private Dictionary<Type, ItemGlobalData> _staticDataMap;

    public GlobalDataService() {
      Initialize();
    }

    public T GetGlobalData<T>() where T : ItemGlobalData {
      return _staticDataMap[typeof(T)] as T;
    }

    public void Initialize() {
      _staticDataMap = Resources.LoadAll<ItemGlobalData>(Constants.Constants.Paths.GLOBAL_DATA).ToDictionary(globalData => globalData.GetType(), itemGlobalData => itemGlobalData);

      foreach (ItemGlobalData staticData in _staticDataMap.Values) {
        staticData.LoadEssential();
      }

      IsInitialized = true;
    }

    public void Dispose() {
      _staticDataMap.Clear();
    }

    public bool IsInitialized { get; private set; }
  }
}