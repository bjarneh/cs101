using System;
using System.Collections.Generic;
using System.Text;

namespace csopt
{
    class Program
    {
        static void Main(string[] args)
        {
            var getopt = new GetOpt();

            getopt.AddStrOption(flags: new string[]{"-f","-f=","-file","-file="});
            getopt.AddBoolOption(flags: new string[]{"-h","-help","--help"});

            getopt.Parse(args);

            Console.WriteLine( $" getopt: {getopt}" );
        }
    }


    class GetOpt {

        HashSet<string> longFlags;
        Dictionary<string,Option> options;
        public GetOpt() => init();

        private void init(){
            if( options == null ){ options = new Dictionary<string,Option>(); }
            if( longFlags == null ){ longFlags = new HashSet<string>(); }
        }

        public void Parse(IEnumerable<string> optionStr){
            Option o;
            foreach(string s in optionStr){
                if( options.ContainsKey(s)){
                    o = options.GetValueOrDefault(s, null);
                    if( o.GetType() == typeof(BoolOption) ){
                        o.isSet = true;
                    }
                }
            }
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


    class Option {

        public bool isSet;
        string longOpt = null;
        IEnumerable<string> flags;
        public Option(IEnumerable<string> flags) => this.flags = flags;

        public string getName(){
            if( longOpt == null ){
                if( flags != null ){
                    foreach( string f in flags ){
                        if( longOpt == null || f.Length > longOpt.Length ){
                            longOpt = f;
                        }
                    }
                }
            }
            return longOpt;
        }

        public override string ToString(){
            return this.getName() + " [ "+ isSet + " ] ";
        }
    }

    class StrOption : Option
    {
        List<string> values;
        public StrOption(IEnumerable<string> flags) : base(flags){}
        public List<string> Values { get => values; }
    }

    class BoolOption : Option
    {
        public BoolOption(IEnumerable<string> flags) : base(flags){}
    }
}
