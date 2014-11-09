using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPEngine.ECS.Components
{
    public interface IEntityComponent
    {
        string Name { get; }
        
        void Initialize();

        void Start();
    }
}
