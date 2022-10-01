using ChessWindowsForms.View.Helper;
using System.Collections.Generic;

namespace ChessWindowsForms.Model.Contracts
{
    public interface IMarkerSpawner
    {
        List<Marker> Spawn(List<Position> positions);
    }
}
