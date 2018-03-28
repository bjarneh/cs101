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
            Stream inputStream = getInputAsStream(fileName, inputEnc);
        }


        private static Stream getInputAsStream(string inputFile, Encoding inputEnc){
            using( MemoryStream binaryWriter = new MemoryStream() ){
                using( BinaryReader binaryReader = new BinaryReader(File.Open(inputFile, FileMode.Open), inputEnc) ){
                    binaryReader.BaseStream.CopyTo( binaryWriter );
                }
                return binaryWriter;
            }
        }

        private static void writeStreamInEncoding(string fileName, Stream stream, Encoding outputEncoding){
            using( BinaryWriter binaryWriter = new BinaryWriter(File.Open(fileName, FileMode.Open), outputEncoding)){
                binaryWriter.BaseStream.CopyTo( stream );
            }
        }

    }
}
