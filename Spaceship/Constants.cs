using FixMath.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public class Constants
    {
        public const int MessageMaxCharacters = 2000;
        public const string TextBoxAffix = "```";
        /// <summary>
        /// Width to height ratio of monospaced characters in Discord.
        /// </summary>
        public static Fix CharacterSizeRatio { get; } = 231 / (Fix)480;
    }
}
