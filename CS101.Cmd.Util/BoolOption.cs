using System.Collections.Generic;

/**
 * Holds boolean flags or switches that require no parameters.
 *
 * @author  bjarneh@ifi.uio.no
 * @license  public domain
 */

namespace CS101.Cmd.Util
{
    class BoolOption : Option
    {
        public BoolOption(IEnumerable<string> flags) : base(flags){}
    }
}