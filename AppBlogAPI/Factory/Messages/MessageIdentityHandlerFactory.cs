namespace AppBlogAPI.Factory.Messages
{
    public class MessageIdentityHandlerFactory
    {
        public static MessageIdentityFactory Handler(string status)
        {
            MessageIdentityFactory factory = null;
            switch (status)
            {
                case Constants.Application.Result.InvalidUserName:
                    return new InvalidUserName();
                default:
                    break;
            }

            return factory;
        }
    }
}
