namespace Sandbox.SOA.Common.Validation
{
    public class ValidationFailure
    {
        readonly string _propertyName;
        readonly string _message;
        readonly object _attemptedValue;

        public ValidationFailure(string propertyName, string message, object attemptedValue)
        {
            _propertyName = propertyName;
            _message = message;
            _attemptedValue = attemptedValue;
        }

        public string PropertyName
        {
            get { return _propertyName; }
        }

        public string Message
        {
            get { return _message; }
        }

        public object AttemptedValue
        {
            get { return _attemptedValue; }
        }
    }


}