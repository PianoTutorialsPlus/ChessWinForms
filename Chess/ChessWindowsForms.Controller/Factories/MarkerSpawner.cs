using ChessWindowsForms.Model;
using ChessWindowsForms.Model.Contracts;
using ChessWindowsForms.View.Helper;
using System.Collections.Generic;

namespace ChessWindowsForms.Presenter.Factories
{
    public class MarkerSpawner : IMarkerSpawner
    {
        public List<Marker> Spawn(List<Position> possiblePositions)
        {
            var markers = new List<Marker>();
            foreach (Position position in possiblePositions)
            {
                markers.Add(new Marker(position.Column, position.Row));
            }
            return markers;
        }
    }
}
