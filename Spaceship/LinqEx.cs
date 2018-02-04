using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship
{
    public static class LinqEx
    {
        public static T FirstById<T>(this IEnumerable<T> enumerable, Id<T> id) 
            where T : IId<T> =>
            enumerable.First(item => item.Id == id);

        public static int IndexById<T>(this ImmutableList<T> list, Id<T> id)
            where T : IId<T> =>
            list.FindIndex(item => item.Id == id);

        public static T FirstIdOrDefault<T>(this IEnumerable<T> enumerable, Id<T> id)
            where T : IId<T> =>
            enumerable.FirstOrDefault(item => item.Id == id);
    }
}
