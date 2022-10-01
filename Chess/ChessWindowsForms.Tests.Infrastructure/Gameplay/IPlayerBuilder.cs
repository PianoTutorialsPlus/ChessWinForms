using ChessWindowsForms.View.Contracts;
using NSubstitute;
using System.Collections.Generic;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class IPlayerBuilder : TestDataBuilder<IPlayer>
    {
        List<IChessPieceView> _chessPieceList;
        public IPlayerBuilder()
        {
        }

        public override IPlayer Build()
        {
            var player = Substitute.For<IPlayer>();
            //player.ChessPieceList.Returns(_chessPieceList);

            return player;
        }
    }
}