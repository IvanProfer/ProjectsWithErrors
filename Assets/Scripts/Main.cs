using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {

        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private LevelObjectView _playerView;

        private SpriteAnimatorController _playerAnimator;

        private PlayerController _playerController;

        void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
            }
            if (_playerView)
            {
                _playerAnimator.StartAnimation(_playerView.SpriteRenderer, AnimState.Idle, true, _animationSpeed);
            }

            _playerController = new PlayerController(_playerView, _playerAnimator);
        }


        void Update()
        {
            _playerAnimator.Update();
            _playerController.Update();
        }

        void FixedUpdate()
        {
            
        }
    }
}
