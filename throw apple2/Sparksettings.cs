using System;
using System.Collections.Generic;
using System.Text;

namespace throw_apple2
{
    static class Sparksettings
    {
        private static Random _rand = new Random();
       
        public static float RandomFloat(int min, int max)
        {
            return (float)_rand.NextDouble() * (max - min) + min;
        }
        public static int RandomInt(int min, int max)
        {
            return _rand.Next(min, max);
        }
        public static bool Flipcoin()
        {
            if (_rand.NextDouble() < 0.5)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

    }

  
}
