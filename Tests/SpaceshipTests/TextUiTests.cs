using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FixMath.NET;
using Spaceship;

namespace DiscordBotTests.SpaceshipTests
{
    [TestFixture]
    public class TextUiTests
    {
        [Test]
        public void UiFill()
        {
            var ui = new char[20, 40];
            ui.Fill('X');

            Assert.IsTrue(ui.ToEnumerable().All(item => item == 'X'));
        }

        [TestCase(1, 1, 7, 4)]
        [TestCase(7, 4, 1, 1)]
        [TestCase(7, 1, 1, 4)]
        [TestCase(1, 4, 7, 1)]
        public void UiDrawRectangle(int x0, int y0, int x1, int y1)
        {
            var ui = new char[4, 5];
            ui.Fill('.');
            ui.DrawRectangle('X', new Int2(x0, y0), new Int2(x1, y1));

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

        [TestCase(1, 1, 7, 4)]
        [TestCase(7, 4, 1, 1)]
        [TestCase(7, 1, 1, 4)]
        [TestCase(1, 4, 7, 1)]
        public void UiDrawRectangleOutline(int x0, int y0, int x1, int y1)
        {
            var ui = new char[4, 5];
            ui.Fill('.');
            ui.DrawRectangleOutline('X', new Int2(x0, y0), new Int2(x1, y1));

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
