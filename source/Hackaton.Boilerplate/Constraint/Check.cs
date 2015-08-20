using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Boilerplate.Constraint
{
    public static class Check
    {
        private static string GetName(Expression<Func<object>> exp)
        {
            MemberExpression body = exp.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        public static bool IsNull(Expression<Func<object>> exp, bool throwIfFail = true)
        {
            bool fail = true;

            var compiled = exp != null
                ? exp.Compile()
                : null;

            if (compiled != null)
            {
                fail = compiled() == null;
            }

            if (throwIfFail && fail)
            {
                throw new ArgumentNullException(
                    GetName(exp)
                );
            }

            return fail;
        }
    }
}
