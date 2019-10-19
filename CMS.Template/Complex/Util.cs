using System.Text.RegularExpressions;

namespace CMS.Template.Complex
{
  public  class Util
    {
        static object syncObject = new object();


        static Regex regExVarName;

        public static bool ToBool(object obj)
        {
            if (obj is bool)
                return (bool)obj;
            else if ((obj as string) != null)
            {
                string str = (string)obj;
                if (string.Compare(str, "true", true) == 0)
                    return true;
                else if (string.Compare(str, "yes", true) == 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool IsValidVariableName(string name)
        {
            return RegExVarName.IsMatch(name);
        }

        private static Regex RegExVarName
        {
            get
            {
                if ((regExVarName == null))
                {
                    System.Threading.Monitor.Enter(syncObject);
                    if (regExVarName == null)
                    {
                        try
                        {
                            regExVarName = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);
                        }
                        finally
                        {
                            System.Threading.Monitor.Exit(syncObject);
                        }
                    }
                }

                return regExVarName;
            }
        }
    }
}
