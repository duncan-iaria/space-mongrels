using UnityEngine;
using TMPro;
using SNDL;

namespace SM
{
  public class SMUINavHelper : MonoBehaviour
  {
    public SMLevelGate gate;
    public TextMeshPro textMesh;
    [Range(0f, 1f)]
    public float hintPositionOffset = 0.05f;
    private Animator _animator;
    private Transform _player;
    private SMGame _game;
    private Camera _gameCamera;
    private float _minHintPosition, _maxHintPosition;
    private bool _isInView;
    private int _turnOnHash = Animator.StringToHash("TurnOn");
    private int _turnOffHash = Animator.StringToHash("TurnOff");
    // Start is called before the first frame update
    void Start()
    {
      _game = Game.GetGame<Game>() as SMGame;
      _gameCamera = _game.view.cam;
      _player = _game.controller.currentPawn.transform;
      _animator = this.GetComponent<Animator>();
      _minHintPosition = 0 + hintPositionOffset;
      _maxHintPosition = 1 - hintPositionOffset;
      textMesh.text = gate.sourceLevel.displayName;
    }

    // Update is called once per frame
    void Update()
    {
      if (_player != null && _gameCamera != null)
      {

        // Check if the hint PoI is in view
        Renderer tempGateRenderer = gate.gameObject.GetComponent<Renderer>();
        if (RendererExtensions.IsVisibleFrom(tempGateRenderer, _gameCamera))
        {
          // If it's now in view, and it previously wasn't
          if (!_isInView)
          {
            _isInView = true;
            _animator.SetTrigger(_turnOffHash);
          }
          return;
        }

        // If it was previously IN view, but now isn't
        if (_isInView)
        {
          _isInView = false;
          _animator.SetTrigger(_turnOnHash);
        }


        Vector3 tempGatePoint = _gameCamera.WorldToViewportPoint(gate.transform.position);

        // Min is a value like .05f and Max is a val like .95f
        float tempX = Mathf.Clamp(tempGatePoint.x, _minHintPosition, _maxHintPosition);
        float tempY = Mathf.Clamp(tempGatePoint.y, _minHintPosition, _maxHintPosition);

        Vector3 tempPos = new Vector3(tempX, tempY, 15);
        this.transform.position = _gameCamera.ViewportToWorldPoint(tempPos);
      }
      else
      {
        Debug.Log("no player found");
      }
    }
  }
}
