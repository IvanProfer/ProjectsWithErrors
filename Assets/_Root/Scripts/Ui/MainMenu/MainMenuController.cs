using Tool;
using Profile;
using Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, Settings, Shed, Reward, Buy, Inventory);
        }

        
        

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void Settings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void Shed() =>
            _profilePlayer.CurrentState.Value = GameState.Shed;

        private void Inventory() =>
            _profilePlayer.CurrentState.Value = GameState.Inventory;


        private void Buy(string productId)
        {
            SubscribeIAP();
            ServiceLocator.IAPService.Buy(productId);
            Debug.Log("Buy clicked");
        }

        private void SubscribeIAP()
        {
            ServiceLocator.IAPService.PurchaseSucceed.AddListener(OnIAPSucceed);
            ServiceLocator.IAPService.PurchaseFailed.AddListener(OnIAPFailed);
        }

        private void UnsubscribeIAP()
        {
            ServiceLocator.IAPService.PurchaseSucceed.RemoveListener(OnIAPSucceed);
            ServiceLocator.IAPService.PurchaseFailed.RemoveListener(OnIAPFailed);
        }

        private void OnIAPSucceed()
        {
            UnsubscribeIAP();
            Debug.Log("Purchase succeed");
        }

        private void OnIAPFailed()
        {
            UnsubscribeIAP();
            Debug.Log("Purchase failed");
        }


        private void Reward()
        {
            SubscribeRewardedPlayer();
            ServiceLocator.AdsService.RewardedPlayer.Play();
            Debug.Log("Reward clicked");
        }

        private void SubscribeRewardedPlayer()
        {
            ServiceLocator.AdsService.RewardedPlayer.Finished += OnAdsFinished;
            ServiceLocator.AdsService.RewardedPlayer.Failed += OnAdsCancelled;
            ServiceLocator.AdsService.RewardedPlayer.Skipped += OnAdsCancelled;
        }

        private void UnsubscribeRewardedPlayer()
        {
            ServiceLocator.AdsService.RewardedPlayer.Finished -= OnAdsFinished;
            ServiceLocator.AdsService.RewardedPlayer.Failed -= OnAdsCancelled;
            ServiceLocator.AdsService.RewardedPlayer.Skipped -= OnAdsCancelled;
        }

        private void OnAdsFinished()
        {
            UnsubscribeRewardedPlayer();
            Debug.Log("You've received a reward for ads!");
        }

        private void OnAdsCancelled()
        {
            UnsubscribeRewardedPlayer();
            Debug.Log("Receiving a reward for ads has been interrupted!");
        }
    }
}
