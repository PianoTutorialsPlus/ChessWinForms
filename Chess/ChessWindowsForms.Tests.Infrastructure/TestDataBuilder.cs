using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWindowsForms.Tests.Infrastructure
{
    public abstract class TestDataBuilder<T>
    {
        public abstract T Build();

        public static implicit operator T(TestDataBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}
