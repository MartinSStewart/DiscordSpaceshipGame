using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spaceship.Terminals;
using FixMath.NET;

namespace DiscordBotTests.SpaceshipTests
{
    [TestFixture]
    public class TerminalTests
    {
        [Test]
        public void UiFill()
        {
            var ui = new char[20, 40];
            ui.Fill('X');

            Assert.IsTrue(ui.ToEnumerable().All(item => item == 'X'));
        }

        [Test]
        public void UiDrawRectangle()
        {
            var ui = new char[4, 5];
            ui.Fill('.');
            ui.DrawRectangle('X', new Int2(1, 1), new Int2(6, 3));

            var expected = 
@"....
.XXX
.XXX
.XXX
....
".Replace("\r", "");

            var result = ui.GetText();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UiDrawRectangleOutline()
        {
            var ui = new char[4, 5];
            ui.Fill('.');
            ui.DrawRectangleOutline('X', new Int2(1, 1), new Int2(6, 3));

            var expected =
@"....
.XXX
.X..
.XXX
....
".Replace("\r", "");

            var result = ui.GetText();
            Assert.AreEqual(expected, result);
        }
    }
}
