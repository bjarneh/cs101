﻿using System;
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
        -f --from   : input encoding (utf-8,iso-8859-1,...)
        -t --to     : output encoding (utf-8,utf-16,...)
        -l --list   : list available encodings
        -u --u2i    : alias --from=utf-8 --to=iso-8859-1
        -i --i2u    : alias --from=iso-8859-1 --to=utf-8
        ";

        static void Main(string[] args)
        {

            string inputStrEnc = null;
            string outputStrEnc = null;
            Encoding inputEncoding = null;
            Encoding outputEncoding = null;

            var getopt = new GetOpt();

            getopt.AddBoolOption(flags: new string[]{"-h","-help","--help"});
            getopt.AddBoolOption(flags: new string[]{"-l","-list","--list"});
            getopt.AddBoolOption(flags: new string[]{"-u2i","--u2i"});
            getopt.AddBoolOption(flags: new string[]{"-i2u","--i2u"});
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

            if( getopt.IsSet("--list") ){
                foreach( EncodingInfo ei in Encoding.GetEncodings() ){
                    Console.WriteLine( ei.Name );
                }
                Environment.Exit(0);
            }

            if( rest.Length == 0 ){
                Console.Error.WriteLine("No filename given");
                Environment.Exit(1);
            }

            if( getopt.IsSet("-u2i")){
                inputStrEnc  = "utf-8";
                outputStrEnc = "iso-8859-1";
            }

            if( getopt.IsSet("-i2u")){
                inputStrEnc  = "iso-8859-1";
                outputStrEnc = "utf-8";
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

            if( outputEncoding.Equals(inputEncoding) ){
                Console.Error.WriteLine("Output encoding equals input encoding, will exit");
                Environment.Exit(1);
            }

            if( rest.Length > 0 ){
                foreach(string r in rest){
                    Conv.Convert(r, inputEncoding, outputEncoding);
                }
            }

            //Console.WriteLine( $" getopt: {getopt}" );
        }
    }
}