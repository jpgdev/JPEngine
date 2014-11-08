using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Managers
{
    public interface IAudioManager 
    {
        void Play(string name);

        void Stop(string name);

    }
}
