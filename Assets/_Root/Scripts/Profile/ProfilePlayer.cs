using Game;
using Tool;
using Game.Transport;
using Features.Inventory;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly InventoryModel InventoryModel;
        public readonly TransportModel CurrentTransport;


        public ProfilePlayer(float transportSpeed, TransportType transportType, GameState initialState)
        {
            CurrentState = new SubscriptionProperty<GameState>(initialState);
            InventoryModel = new InventoryModel();
            CurrentTransport = new TransportModel(transportSpeed, transportType);
        }
    }
}
