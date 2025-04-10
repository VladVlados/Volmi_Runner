using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Architecture.CodeBase.UI.Animation {
  public class LoadingAnimation : MonoBehaviour {
    [SerializeField]
    private Image _loadingImage;

    private Tween _spinTween;

    private void OnEnable() {
      StartLoadingAnimation();
    }

    private void OnDisable() {
      StopLoadingAnimation();
    }

    private void StartLoadingAnimation() {
      StopLoadingAnimation();
      _spinTween = _loadingImage.transform.DORotate(new Vector3(0, 0, -360), 4f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void StopLoadingAnimation() {
      _spinTween?.Kill();
      _loadingImage.transform.rotation = Quaternion.identity;
    }
  }
}