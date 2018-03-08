using System.Collections.Generic;

/**
 * Super type that holds all flags of a certain type.
 *
 * @author  bjarneh@ifi.uio.no
 * @license  public domain
 */

namespace CS101.Cmd.Util
{
    class Option {

        public bool isSet;
        string longOpt = null;
        public IEnumerable<string> flags;
        public Option(IEnumerable<string> flags) => this.flags = flags ?? throw new System.ArgumentNullException(nameof(flags));

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

        public void Reset() => this.isSet = false;

        public override string ToString(){
            return this.getName() + " [ "+ isSet + " ] ";
        }
    }
}