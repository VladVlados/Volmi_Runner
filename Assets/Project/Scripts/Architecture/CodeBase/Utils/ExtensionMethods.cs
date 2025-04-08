using System;
using System.Collections.Generic;

namespace Project.Scripts.Architecture.CodeBase.Utils {
  public static class ExtensionMethods { }

  public static class UIExtensions {
    public static void Push<TSource>(this List<TSource> list, TSource param) {
      list.Add(param);
    }

    public static TSource PopLast<TSource>(this List<TSource> list) {
      return list.PopAt(list.Count - 1);
    }

    public static TSource PopAt<TSource>(this List<TSource> list, int index) {
      TSource r = list[index];
      list.RemoveAt(index);
      return r;
    }

    public static TSource PopFirst<TSource>(this List<TSource> list, Predicate<TSource> predicate) {
      int index = list.FindIndex(predicate);
      TSource r = list[index];
      list.RemoveAt(index);
      return r;
    }

    public static TSource PopFirstOrDefault<TSource>(this List<TSource> list, Predicate<TSource> predicate) where TSource : class {
      int index = list.FindIndex(predicate);

      if (index <= -1) {
        return null;
      }

      TSource r = list[index];
      list.RemoveAt(index);
      return r;
    }
  }
}