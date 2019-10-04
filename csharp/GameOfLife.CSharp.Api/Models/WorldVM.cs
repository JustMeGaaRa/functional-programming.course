﻿using System.Collections.Generic;

namespace GameOfLife.CSharp.Api.Models
{
    public class WorldVM
    {
        public uint Generation { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public ICollection<WorldRowVM>? Rows { get; set; }
    }
}