using ChessWindowsForms;
using ChessWindowsForms.View.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChessWindowsForms.View.Contracts
{
    public interface IGameplay
    {
        Queue<IPlayer> Players { get; }
        Color ColorWhite { get; }

        event Action<IPlayer> OnDetachedEvents;
        event Action<IPlayer> OnAttachedEvents;

        void EndTurn();
    }
}