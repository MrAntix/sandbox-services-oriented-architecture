using System;
using System.Web.Mvc;
using System.Web.Routing;

using Sandbox.SOA.Common.Services;

namespace Sandbox.SOA.Portal
{
    public class ActionResultBuilder<TIn, TOut>
    {
        readonly Controller _controller;
        readonly Func<object, ActionResult> _view;
        readonly Func<string, RouteValueDictionary, ActionResult> _redirect;
        readonly ICommandHandler _commandHandler;
        readonly TIn _model;

        internal ActionResultBuilder(
            Controller controller,
            Func<object, ActionResult> view,
            Func<string, RouteValueDictionary, ActionResult> redirect,
            ICommandHandler commandHandler,
            TIn model)
        {
            _commandHandler = commandHandler;
            _controller = controller;
            _model = model;
            _view = view;
            _redirect = redirect;
        }

        Func<TOut, ActionResult> _done;
        Func<TOut, ActionResult> _fail;

        public ActionResultBuilder<TIn, TOut> Done(Func<TOut, ActionResult> func)
        {
            _done = func;
            return this;
        }

        public ActionResultBuilder<TIn, TOut> Done(string actionName, Func<TOut, object> routeValues)
        {
            _done = result => _redirect(actionName,
                                        new RouteValueDictionary(routeValues(result))
                                            {
                                                {"state", "success"}
                                            });

            return this;
        }

        public ActionResultBuilder<TIn, TOut> Fail(Func<TOut, ActionResult> func)
        {
            _fail = func;
            return this;
        }

        ActionResult View()
        {
            var result = default(TOut);
            if (_controller.ModelState.IsValid)
            {
                try
                {
                    result = _commandHandler.Handle<TIn, TOut>(_model);

                    return OrDefault(_done, result);
                }
                catch (Exception ex)
                {
                    _controller.ModelState
                               .AddModelError(string.Empty, ex.Message);
                }
            }

            return OrDefault(_fail, result);
        }

        ActionResult OrDefault(Func<TOut, ActionResult> view, TOut result)
        {
            return view == null
                       ? _view(result)
                       : view(result);
        }

        public static implicit operator ActionResult(
            ActionResultBuilder<TIn, TOut> actionResultBuilder)
        {
            return actionResultBuilder.View();
        }
    }

    public class ActionResultBuilder<T>
    {
        readonly Controller _controller;
        readonly Func<object, ActionResult> _view;
        readonly Func<string, RouteValueDictionary, ActionResult> _redirect;
        readonly ICommandHandler _commandHandler;
        readonly T _model;

        internal ActionResultBuilder(
            Controller controller,
            Func<object, ActionResult> view,
            Func<string, RouteValueDictionary, ActionResult> redirect,
            ICommandHandler commandHandler,
            T model)
        {
            _commandHandler = commandHandler;
            _controller = controller;
            _model = model;
            _view = view;
            _redirect = redirect;
        }

        Func<ActionResult> _done;
        Func<ActionResult> _fail;

        public ActionResultBuilder<T> Done(Func<ActionResult> func)
        {
            _done = func;
            return this;
        }

        public ActionResultBuilder<T> Done(string actionName, object routeValues = null)
        {
            _done = () => _redirect(actionName, new RouteValueDictionary(routeValues) {{"state", "success"}});
            return this;
        }

        public ActionResultBuilder<T> Fail(Func<ActionResult> func)
        {
            _fail = func;
            return this;
        }

        ActionResult View()
        {
            if (_controller.ModelState.IsValid)
            {
                try
                {
                    _commandHandler.Handle(_model);

                    return OrDefault(_done);
                }
                catch (Exception ex)
                {
                    _controller.ModelState
                               .AddModelError(string.Empty, ex.Message);
                }
            }

            return OrDefault(_fail);
        }

        ActionResult OrDefault(Func<ActionResult> view)
        {
            return view == null
                       ? _view(_model)
                       : view();
        }

        public static implicit operator ActionResult(
            ActionResultBuilder<T> actionResultBuilder)
        {
            return actionResultBuilder.View();
        }

        public ActionResultBuilder<T, TOut> Returns<TOut>()
        {
            return new ActionResultBuilder<T, TOut>(
                _controller, _view, _redirect, _commandHandler, _model);
        }
    }
}