﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Utils.ScriptConsole
{
    public interface IScriptParser
    {
        void Initialize();

        object[] ParseScript(string script);

        object[] ParseFile(string path);
    }
}