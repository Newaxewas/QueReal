using QueReal.BLL.Exceptions;

namespace QueReal.BLL.Services
{
    internal static class ServiceHelper
    {
        public static void AssertObjectExists<T>(T @object)
        {
            if (@object == null)
            {
                var typeName = typeof(T).Name;

                throw new NotFoundException($"{typeName} not found");
            }
        }

        public static int CalculateSkipCount(int pageNumber, int pageSize)
        {
            return (pageNumber - 1) * pageSize;
        }
    }
}
