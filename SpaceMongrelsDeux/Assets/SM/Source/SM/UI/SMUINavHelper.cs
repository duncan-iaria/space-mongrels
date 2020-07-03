using UnityEngine;
using TMPro;
using SNDL;

namespace SM
{
  public class SMUINavHelper : MonoBehaviour
  {
    [Tooltip("This is the object that the UI Nav Helper will be displayed for")]
    public Transform pointOfInterest;
    public TextMeshPro textMesh;
    public string navUiTitle;

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

      // Set the text (and figure out the size)
      textMesh.text = navUiTitle;
      textMesh.ForceMeshUpdate();

      // TODO figure out how to set the offset to include the size of the text
      // Debug.Log(textMesh.textBounds.extents);

      _minHintPosition = 0 + hintPositionOffset;
      _maxHintPosition = 1 - hintPositionOffset;
    }

    // Update is called once per frame
    void Update()
    {
      if (_player != null && _gameCamera != null)
      {

        // Check if the hint PoI is in view
        Renderer tempPoiRenderer = pointOfInterest.gameObject.GetComponent<Renderer>();
        if (RendererExtensions.IsVisibleFrom(tempPoiRenderer, _gameCamera))
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


        Vector3 tempGatePoint = _gameCamera.WorldToViewportPoint(pointOfInterest.transform.position);

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
