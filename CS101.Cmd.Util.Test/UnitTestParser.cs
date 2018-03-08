using System;
using Xunit;
using System.Collections.Generic;

using CS101.Cmd.Util;

namespace CS101.Cmd.Util.Test
{
    public class UnitTestParser
    {
        private GetOpt parser;

        public UnitTestParser(){
            parser = new GetOpt();
        }

        [Fact]
        public void TestStrOption()
        {
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
        }
    }
}
