﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Resources.Scripts.Interfaces
{
    interface IGame
    {
       
        void Iteration();

        bool Answer(int answer);
    }
}
