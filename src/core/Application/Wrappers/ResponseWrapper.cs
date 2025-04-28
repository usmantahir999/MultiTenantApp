namespace Application.Wrappers
{
    public class ResponseWrapper : IResponseWrapper
    {
        public List<string> Messages { get; set; } = [];
        public bool IsSucessful { get; set; }
        public ResponseWrapper()
        { 
        }

        //Flag Failure
        
        public static IResponseWrapper Fail()
        {
            return new ResponseWrapper
            {
                IsSucessful = false
            };
        }

        public static IResponseWrapper Fail(string message)
        {
            return new ResponseWrapper
            {
                IsSucessful = false,
                Messages = [message]
            };
        }
        public static IResponseWrapper Fail(List<string> messages)
        {
            return new ResponseWrapper
            {
                IsSucessful = false,
                Messages = messages
            };
        }

        public static Task<IResponseWrapper> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResponseWrapper> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static Task<IResponseWrapper> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }
        //Flag Success

        public static IResponseWrapper Success()
        {
            return new ResponseWrapper
            {
                IsSucessful = true
            };
        }
        public static IResponseWrapper Success(string message)
        {
            return new ResponseWrapper
            {
                IsSucessful = true,
                Messages = [message]
            };
        }

        public static IResponseWrapper Success(List<string> messages)
        {
            return new ResponseWrapper
            {
                IsSucessful = true,
                Messages = messages
            };
        }

        public static Task<IResponseWrapper> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResponseWrapper> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<IResponseWrapper> SuccessAsync(List<string> messages)
        {
            return Task.FromResult(Success(messages));
        }
    }
}
