using System.Collections.Generic;
using System.Text;

/**
 * The actual flag parser, holding a map of flags connected to the
 * Options (either StrOption or BoolOption). 
 *
 * @author  bjarneh@ifi.uio.no
 * @license  public domain
 */

namespace csopt
{
    class GetOpt {

        HashSet<string> longFlags;
        Dictionary<string,Option> options;
        public GetOpt() => init();

        private void init(){
            longFlags = new HashSet<string>();
            options = new Dictionary<string,Option>();
        }

        public bool IsSet(string flag){
            Option o = options.GetValueOrDefault(flag, null);
            return o != null && o.isSet;
        }

        public string GetOption(string flag){
            Option o = options.GetValueOrDefault(flag, null);
            if( o != null ){
                StrOption so = o as StrOption;
                if( so != null ){
                    return so.GetOption();
                }
            }
            return null;
        }

        public List<string> GetOptions(string flag){
            Option o = options.GetValueOrDefault(flag, null);
            if( o != null ){
                StrOption so = o as StrOption;
                if( so != null ){
                    return so.values;
                }
            }
            return null;
        }
        
        public string[] Parse(string[] argv){
            Option o;
            List<string> rest = new List<string>();
            for (int i = 0; i < argv.Length; i++){
                if( options.ContainsKey(argv[i])){
                    o = options.GetValueOrDefault(argv[i], null);
                    if( o.GetType() == typeof(BoolOption) ){
                        o.isSet = true;
                    }else{
                        if( i + 1 < argv.Length ){
                            o = options.GetValueOrDefault(argv[i], null);
                            o.isSet = true;
                            (o as StrOption).AddParameter(argv[i + 1]);
                            i++;
                        }
                    }
                }else{
                    if(!isJuxtaPos(argv[i])){
                        rest.Add(argv[i]);
                    }
                }
            }
            return rest.ToArray();
        }

        private bool isJuxtaPos(string flag){

            int max = -1;
            StrOption currLongest = null;

            foreach(string k in this.longFlags){
                Option o = this.options.GetValueOrDefault(k, null);
                if( o.GetType() == typeof(StrOption)){
                    StrOption so = o as StrOption;
                    int num = so.LongestJuxtaPos(flag);
                    if( num > 0 && num > max ){
                        max = num;
                        currLongest = so;
                    }
                }
            }

            if( currLongest != null ){
                currLongest.isSet = true;
                currLongest.AddParameter( flag.Substring(max) );
                return true;
            }

            return false;
        }

        public void AddStrOption(IEnumerable<string> flags){
            var so = new StrOption(flags: flags);
            longFlags.Add(so.getName());
            foreach(string f in flags){
                options.Add(f, so);
            }
        }

        public void AddBoolOption(IEnumerable<string> flags){
            var bo = new BoolOption(flags: flags);
            longFlags.Add(bo.getName());
            foreach(string f in flags){
                options.Add(f, bo);
            }
        }

        public override string ToString(){
            StringBuilder sb = new StringBuilder();
            sb.Append("GetOpt( ");
            foreach(string f in longFlags){
                Option o = options.GetValueOrDefault(f, null);
                if( o != null ){
                    sb.Append(o).Append(" ");
                }
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}