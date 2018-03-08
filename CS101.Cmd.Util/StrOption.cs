using System;
using System.Collections.Generic;

/**
 * Holds flag(s) that take one or more parameter(s).
 *
 * @author  bjarneh@ifi.uio.no
 * @license  public domain
 */

namespace CS101.Cmd.Util
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

        public new void Reset(){
            base.Reset();
            values = null;
        }

    }
}