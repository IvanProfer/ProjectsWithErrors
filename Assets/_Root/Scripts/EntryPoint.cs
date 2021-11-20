using Game;
using Profile;
using UnityEngine;
using Services.IAP;
using Services.Analytics;
using Services.Ads.UnityAds;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;
    private const TransportType TransportType = Game.TransportType.Car;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private IAPService _iapService;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;

    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, TransportType, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);

        _iapService.Initialized.AddListener(OnIapInitialized);
        _adsService.Initialized.AddListener(_adsService.InterstitialPlayer.Play);
    }

    private void OnDestroy()
    {
        _iapService.Initialized.RemoveListener(OnIapInitialized);
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _mainController.Dispose();
    }


    private void OnIapInitialized() => _iapService.Buy("product_1");
    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
}
