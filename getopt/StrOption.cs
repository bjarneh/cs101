using System;
using System.Collections.Generic;

/**
 * Holds flags that take one or more parameters.
 *
 * @author  bjarneh@ifi.uio.no
 * @license  public domain
 */

namespace csopt
{
    class StrOption : Option
    {
        public List<string> values;
        public StrOption(IEnumerable<string> flags) : base(flags){}

        public override string ToString(){
            string v = base.ToString();
            if( values != null && values.Count > 0 ){
                return v + " values : [ " + String.Join(", ", values.ToArray()) + " ]";
            }
            return v;
        }

        public void AddParameter(String p){
            if( this.values == null ){
                this.values = new List<string>();
            }
            this.values.Add(p);
        }

        public int LongestJuxtaPos(string opt){
            int max = -1;
            foreach(string f in this.flags){
                int pos = opt.IndexOf(f);
                if( pos == 0 && f.Length > max ){
                    max = f.Length;
                }
            }
            return max;
        }

        public string GetOption(){
            return values[0];
        }
    }
}