using ChessWindowsForms.View.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChessWindowsForms.Presenter
{
    public class Gameplay : IGameplay
    {
        private readonly Settings _settings;

        public int Turn { get; set; }
        public Queue<IPlayer> Players => _settings.Players;
        public Color ColorWhite => _settings.ColorWhite;
        public Color ColorBlack => _settings.ColorBlack;

        public event Action<IPlayer> OnDetachedEvents;
        public event Action<IPlayer> OnAttachedEvents;

        public Gameplay(
            Settings settings)
        {
            _settings = settings;
            Turn = _settings.Turn;
        }

        public void EndTurn()
        {
            OnDetachedEvents?.Invoke(Players.Peek());
            NextPlayer();
            OnAttachedEvents?.Invoke(Players.Peek());
        }
        private void NextPlayer()
        {
            Players.Enqueue(Players.Dequeue());
        }

        [Serializable]
        public class Settings
        {
            public int Turn;
            public Queue<IPlayer> Players;
            public Color ColorWhite;
            public Color ColorBlack;
        }
    }
}