namespace Sandbox.SOA.Portal.Models
{
    public class FormFooterViewModel
    {
        readonly FormSubmitTypes _submit;
        readonly string _close;
        readonly string _delete;

        public FormFooterViewModel(FormSubmitTypes submit, string close, string delete)
        {
            _submit = submit;
            _close = close;
            _delete = delete;
        }

        public FormSubmitTypes Submit
        {
            get { return _submit; }
        }

        public string Close
        {
            get { return _close; }
        }

        public string Delete
        {
            get { return _delete; }
        }
    }
}