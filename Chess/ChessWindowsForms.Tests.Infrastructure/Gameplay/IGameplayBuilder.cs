using ChessWindowsForms.View.Contracts;
using NSubstitute;
using System;
using System.Drawing;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public class IGameplayBuilder : TestDataBuilder<IGameplay>
    {
        private Color _color;

        public IGameplayBuilder(Color color)
        {
            _color = color;
        }

        public IGameplayBuilder() : this(Color.White)
        {
        }

        public IGameplayBuilder WithColor(Color color)
        {
            _color = color;
            return this;
        }

        public override IGameplay Build()
        {
            var gameplay = Substitute.For<IGameplay>();
            gameplay.ColorWhite.Returns(_color);

            return gameplay;
        }

    }
}