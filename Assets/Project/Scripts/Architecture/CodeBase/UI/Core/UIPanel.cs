using System;
using Project.Scripts.Architecture.CodeBase.UI.Transitions;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.UI.Core {
  [RequireComponent(typeof(UITransitionOpen), typeof(UITransitionClose))]
  public class UIPanel : MonoBehaviour, IUIPanel {
    [SerializeField]
    protected Canvas _canvas;

    protected IUIManager UiManager;

    private UITransitionOpen _transitionIn;
    private UITransitionClose _transitionOut;

    private int _order;

    [Inject]
    public virtual void Init(DiContainer container) {
      _transitionIn = GetComponent<UITransitionOpen>();
      _transitionOut = GetComponent<UITransitionClose>();
      Instance = transform as RectTransform;
      UiManager = container.Resolve<IUIManager>();
      _transitionIn.Construct(UiManager);
      _transitionOut.Construct(UiManager);
    }

    public void Prepare() { }

    public virtual void Open() {
      gameObject.SetActive(true);
      IsOpen = true;
      OnOpen?.Invoke();
    }

    public virtual void Close() {
      gameObject.SetActive(false);
      IsOpen = false;
      OnClose?.Invoke();
    }

    public virtual void OnInitialized() { }

    public virtual void AnimatedOpen(Action onOpen) {
      _transitionIn.Transit(this, onOpen);
    }

    public virtual void AnimatedClose() {
      _transitionOut.Transit(this, Close);
    }

    public RectTransform Instance { get; private set; }

    public int Order {
      get {
        return _order;
      }
      set {
        _order = value;
        _canvas.sortingOrder = _order;
      }
    }

    public bool IsOpen { get; protected set; }

    public Action OnOpen { get; set; }

    public Action OnClose { get; set; }
  }
}