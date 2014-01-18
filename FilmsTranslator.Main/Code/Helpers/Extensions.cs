using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FilmsTranslator.Main.Code.Helpers
{
    public static class Extensions
    {
        public static int? ToInt(this string text)
        {
            return Try<int?>(() => int.Parse(text));
        }

        private static TResult Try<TResult>(Func<TResult> func)
        {
            try
            {
                return func();
            }
            catch
            {
                return default(TResult);
            }
        }
    }
}
