using System;
using System.Linq;
using Xunit;

namespace mdcat.tests
{
    public class AppConvertToConsoleLinesTests
    {
        private readonly App _app;

        public AppConvertToConsoleLinesTests()
        {
            _app = new App();
        }

        [Fact]
        public void all_plain_text()
        {
            var lines = _app.ConvertToConsoleLines(new[] { "plain text" });
            Assert.Equal(ParseMode.None, lines[0].Mode);
        }

        [Fact]
        public void a_header()
        {
            var lines = _app.ConvertToConsoleLines(new[] { "# header", "## Another", "" });
            Assert.Equal(ParseMode.Header, lines[0].Mode);
            Assert.True(lines[0].ConsoleCharacterList.All(cc => cc.Mode == ParseMode.Header));
            Assert.Equal(ParseMode.Header, lines[1].Mode);
            Assert.True(lines[1].ConsoleCharacterList.All(cc => cc.Mode == ParseMode.Header));
            Assert.Equal(ParseMode.None, lines[2].Mode);
        }

        [Fact]
        public void h1_multiline_header()
        {
            var lines = _app.ConvertToConsoleLines(new[] { "header", "=======", "", "bla" });
            Assert.Equal(ParseMode.Header, lines[0].Mode);
            Assert.True(lines[0].ConsoleCharacterList.All(cc => cc.Mode == ParseMode.Header));
            Assert.Equal(ParseMode.Header, lines[1].Mode);
            Assert.True(lines[1].ConsoleCharacterList.All(cc => cc.Mode == ParseMode.Header));
            Assert.Equal(ParseMode.None, lines[2].Mode);
            Assert.Equal(ParseMode.None, lines[3].Mode);
        }

        [Fact]
        public void h2_multiline_header()
        {
            var lines = _app.ConvertToConsoleLines(new[] { "header", "------", "", "bla" });
            Assert.Equal(ParseMode.Header, lines[0].Mode);
            Assert.Equal(ParseMode.Header, lines[1].Mode);
            Assert.Equal(ParseMode.None, lines[2].Mode);
            Assert.Equal(ParseMode.None, lines[3].Mode);
        }

        [Fact]
        public void code_by_spaces()
        {
            var lines = _app.ConvertToConsoleLines(new[] { "bla", "", "    code", "", "bla" });
            Assert.Equal(ParseMode.None, lines[0].Mode);
            Assert.Equal(ParseMode.None, lines[1].Mode);
            Assert.Equal(ParseMode.Code, lines[2].Mode);
            Assert.Equal(ParseMode.None, lines[3].Mode);
            Assert.Equal(ParseMode.None, lines[4].Mode);
        }

        [Fact]
        public void code_by_tab()
        {
            var lines = _app.ConvertToConsoleLines(new[] { "bla", "", "\tcode", "", "bla" });
            Assert.Equal(ParseMode.None, lines[0].Mode);
            Assert.Equal(ParseMode.None, lines[1].Mode);
            Assert.Equal(ParseMode.Code, lines[2].Mode);
            Assert.Equal(ParseMode.None, lines[3].Mode);
            Assert.Equal(ParseMode.None, lines[4].Mode);
        }

        [Fact]
        public void code_by_backticks()
        {
            var lines = _app.ConvertToConsoleLines(new[] { "bla", "```", "code", "```", "bla" });
            Assert.Equal(ParseMode.None, lines[0].Mode);
            Assert.Equal(ParseMode.None, lines[1].Mode);
            Assert.Equal(ParseMode.Code, lines[2].Mode);
            Assert.Equal(ParseMode.None, lines[3].Mode);
            Assert.Equal(ParseMode.None, lines[4].Mode);
        }

        [Fact]
        public void code_inline_with_backticks()
        {
            var lines = _app.ConvertToConsoleLines(new[] { "a `b` c" });
            var consoleLine = lines[0];
            Assert.Equal(ParseMode.None, consoleLine.Mode);
            Assert.Equal(ParseMode.None, consoleLine.ConsoleCharacterList[0].Mode);
            Assert.Equal(ParseMode.None, consoleLine.ConsoleCharacterList[1].Mode);
            Assert.Equal(ParseMode.None, consoleLine.ConsoleCharacterList[2].Mode);
            Assert.Equal(ParseMode.Code, consoleLine.ConsoleCharacterList[3].Mode);
            Assert.Equal(ParseMode.None, consoleLine.ConsoleCharacterList[4].Mode);
            Assert.Equal(ParseMode.None, consoleLine.ConsoleCharacterList[5].Mode);
        }
    }
}