using System;
using System.Text;
using System.IO;

/**
 * Read files in one encoding and convert them into another encoding.
 * 
 * @author  bjarneh@ifi.uio.no
 * @license  public domain
 */

namespace CS101.IO.Util
{
    public static class Conv
    {
        public static void Convert(string inputFile, string inputEnc, string outputEnc){
            Convert(inputFile,
                    Encoding.GetEncoding(inputEnc),
                    Encoding.GetEncoding(outputEnc));
        }

        public static void Convert(string fileName, Encoding inputEnc, Encoding outputEnc){
            byte[] inputBytes  = getInputBytes(fileName);
            byte[] outputBytes = Encoding.Convert(inputEnc, outputEnc, inputBytes);
            writeOutputBytes(fileName, outputBytes);
        }

        private static byte[] getInputBytes(string fileName){
            using( MemoryStream mems = new MemoryStream() ){
                using( FileStream fs = File.Open(fileName, FileMode.Open) ){
                    fs.CopyTo( mems );
                    fs.Close();
                }
                return mems.ToArray();
            }
        }

        private static void writeOutputBytes(string fileName, byte[] bytes){
            using( var fs = new FileStream( fileName, FileMode.Create, FileAccess.Write)){
                fs.Write(bytes, 0, bytes.Length);
            }
        }

    }
}
