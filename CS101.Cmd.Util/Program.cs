using System;
using System.Linq;
using System.IO;
using System.Text;
using CS101.IO.Util;

/**
 * Hello World example to test out C#
 *
 * @author  bjarneh@ifi.uio.no
 * @license  public domain
 */

namespace CS101.Cmd.Util
{
    class Program
    {

        static string help = @"
        conv - convert file encoding

        usage: conv [OPTIONS] <FILENAME(S)>

        options:
        -h --help   : print this menu and exit
        -f --from   : input encoding (UTF-8,ISO-8859-1,...)
        -t --to     : output encoding (UTF-8,UTF-16,...)
        ";

        static void Main(string[] args)
        {
            string inputFile = null;
            string inputStrEnc = null;
            string outputStrEnc = null;
            Encoding inputEncoding = null;
            Encoding outputEncoding = null;

            var getopt = new GetOpt();

            getopt.AddBoolOption(flags: new string[]{"-h","-help","--help"});
            getopt.AddStrOption(flags:
                 new string[]{"-f","-f=","-from","-from=","--from","--from="});
            getopt.AddStrOption(flags:
                 new string[]{"-t","-t=","-to","-to=","--to","--to="});

            string[] rest = getopt.Parse(argv: args);

            if( getopt.IsSet("--help") ){
                var helpFmt = String.Join("\n",
                        help.Split("\n")
                            .Select(s => String.Format("  {0}", s.Trim()) ));
                Console.WriteLine( helpFmt );
                Environment.Exit(0);
            }

            if( rest.Length == 0 ){
                Console.Error.WriteLine("No filename given");
                Environment.Exit(1);
            }else{
                inputFile = rest[0];
            }

            if( getopt.IsSet("-from")){
                inputStrEnc = getopt.GetOption("-from");
            }
            if( getopt.IsSet("-to")){
                outputStrEnc = getopt.GetOption("-to");
            }

            if( inputStrEnc == null ){
                Console.Error.WriteLine("No input encoding given");
                Environment.Exit(1);
            }

            if( outputStrEnc == null ){
                Console.Error.WriteLine("No output encoding given");
                Environment.Exit(1);
            }

            outputEncoding = Encoding.GetEncoding( outputStrEnc );
            inputEncoding  = Encoding.GetEncoding( inputStrEnc );

            Conv.Convert(inputFile, inputEncoding, outputEncoding);

            /*
            if( rest.Length > 0 ){
                foreach(string r in rest){
                    Console.WriteLine($"rest: {r}");
                }
            }
            */
            //Console.WriteLine( $" getopt: {getopt}" );
        }
    }
}