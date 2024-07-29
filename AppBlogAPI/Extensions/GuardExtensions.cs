using Ardalis.GuardClauses;

namespace AppBlogAPI.Extensions
{
    public static class GuardExtensions
    {
        public static void NullOrEmptyGuid(this IGuardClause guardClause, Guid input, string parameterName)
        {
            if (input == Guid.Empty)
            {
                throw new ArgumentException($"O Campo ID {parameterName} esta vazio.", parameterName);
            }
        }
    }
}
