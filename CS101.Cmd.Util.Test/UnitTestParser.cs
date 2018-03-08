using Xunit;
using System.Collections.Generic;

namespace CS101.Cmd.Util.Test
{
    public class UnitTestParser
    {
        [Fact]
        public void TestStrOption()
        {
            GetOpt parser = new GetOpt();
            parser.AddStrOption(flags: new string[]{"-f","-file"});

            Assert.False(parser.IsSet("-f"), "-f option should not be set");
            Assert.False(parser.IsSet("-file"), "-file option should not be set");

            string[] rest = parser.Parse("-not option -file name bla bla -fbla".Split(" "));

            Assert.True(parser.IsSet("-f"), "-f option should be set");
            Assert.True(parser.IsSet("-file"), "-file option should be set");

            Assert.True("name".Equals(parser.GetOption("-f")), "-f should be 'name'");
            Assert.True("name".Equals(parser.GetOption("-file")), "-file should be 'name'");

            List<string> options = parser.GetOptions("-f");

            Assert.True(options[0].Equals("name"), "-file[0] should be 'name'");
            Assert.True(options[1].Equals("bla"), "-file[1] should be 'bla'");

            Assert.True("-not".Equals(rest[0]), "rest[0] should be '-not'");
            Assert.True("option".Equals(rest[1]), "rest[1] should be 'option'");
            Assert.True("bla".Equals(rest[2]), "rest[2] should be 'bla'");
            Assert.True("bla".Equals(rest[3]), "rest[3] should be 'bla'");

            parser.Reset();

            Assert.False(parser.IsSet("-f"), "-f option should not be set");
            Assert.False(parser.IsSet("-file"), "-file option should not be set");
        }


        [Fact]
        public void TestBoolOption()
        {
            GetOpt parser = new GetOpt();
            parser.AddBoolOption(flags: new string[]{"-h","-help"});

            Assert.False(parser.IsSet("-h"), "-h option should not be set");
            Assert.False(parser.IsSet("-help"), "-help option should not be set");

            string[] rest = parser.Parse(
                    "-not option -help -file name bla bla -fbla".Split(" "));

            Assert.True(parser.IsSet("-h"), "-h option should be set");
            Assert.True(parser.IsSet("-help"), "-help option should be set");

            parser.Reset();

            Assert.False(parser.IsSet("-h"), "-h option should not be set");
            Assert.False(parser.IsSet("-help"), "-help option should not be set");
        }
    }
}
