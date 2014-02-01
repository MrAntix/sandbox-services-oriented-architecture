using System;
using System.Web.Mvc;

namespace Sandbox.SOA.Portal.Models
{
    public abstract class FormViewModel<TModel> : IFormViewModel
    {
        public virtual FormSubmitTypes Submit
        {
            get { return FormSubmitTypes.None; }
        }

        protected virtual Func<UrlHelper, TModel, string> Close
        {
            get { return null; }
        }

        protected virtual Func<UrlHelper, TModel, string> Delete
        {
            get { return null; }
        }
        
        Func<UrlHelper, object, string> IFormViewModel.Close
        {
            get { return Wrap(Close); }
        }

        Func<UrlHelper, object, string> IFormViewModel.Delete
        {
            get { return Wrap(Delete); }
        }

        static Func<UrlHelper, object, string> Wrap(Func<UrlHelper, TModel, string> func)
        {
            return func == null
                       ? default(Func<UrlHelper, object, string>)
                       : (url, m) => func(url, (TModel) m);
        }
    }
}