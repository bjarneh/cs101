using System;

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
        static void Main(string[] args)
        {
            var getopt = new GetOpt();

            getopt.AddBoolOption(flags: new string[]{"-h","-help","--help"});
            getopt.AddStrOption(flags:
                 new string[]{"-f","-f=","-file","-file=","--file","--file="});

            string[] rest = getopt.Parse(argv: args);

            if( getopt.IsSet("--help") ){
                Console.WriteLine("I'll help you!");
            }

            if( getopt.IsSet("-file") ){
                string opt = getopt.GetOption("-file");
                Console.WriteLine($"File name given: {opt}");
            }

            if( rest.Length > 0 ){
                foreach(string r in rest){
                    Console.WriteLine($"rest: {r}");
                }
            }

            Console.WriteLine( $" getopt: {getopt}" );
        }
    }
}