namespace ActorModel.Messages
{
    public class FibonacciSourceMessage
    {
        public FibonacciSourceMessage(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}
