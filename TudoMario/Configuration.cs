﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudoMario
{
    class Configuration
    {
        public static Developer Dev { get; } = Developer.Soma;

        public enum Developer { Adam, Dani, Patrik, Soma }
    }
}