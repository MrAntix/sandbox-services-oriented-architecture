using System;
using System.Web.Mvc;
using System.Web.Routing;

using Sandbox.SOA.Common.Services;

namespace Sandbox.SOA.Portal
{
    public class ActionHandler
    {
        readonly Controller _controller;
        readonly Func<object, ActionResult> _view;
        readonly Func<string, RouteValueDictionary, ActionResult> _redirect;
        readonly ICommandHandler _commandHandler;

        public ActionHandler(
            Controller controller,
            Func<object, ActionResult> view,
            Func<string, RouteValueDictionary, ActionResult> redirect,
            ICommandHandler commandHandler)
        {
            _controller = controller;
            _view = view;
            _redirect = redirect;
            _commandHandler = commandHandler;
        }

        public ActionResultBuilder<T> With<T>(T model)
        {
            return new ActionResultBuilder<T>(
                _controller, _view, _redirect,
                _commandHandler,
                model);
        }
    }
}