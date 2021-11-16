using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class HttpContext
    {
        public Response Response { get; } = new Response();
    }

    public class Response
    {
        public Task WriteAsync(string message)
        {
            Console.WriteLine(message);

            return Task.CompletedTask;
        }
    }

    public delegate Task RequestDelegate(HttpContext context);

    public class ApplicationBuilder
    {
        private readonly IList<Func<RequestDelegate, RequestDelegate>> _components;
        private RequestDelegate? _application;

        public ApplicationBuilder()
        {
            _components = new List<Func<RequestDelegate, RequestDelegate>>();
        }

        public ApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _components.Add(middleware);
            return this;
        }

        public ApplicationBuilder Use(Func<HttpContext, Func<Task>, Task> handler)
        {
            Use(next =>
            {
                return context =>
                {
                    return handler(context, () => next(context));
                };
            });

            return this;
        }

        public ApplicationBuilder Build()
        {
            RequestDelegate app = context =>
            {
                return Task.CompletedTask;
            };

            foreach (var component in _components.Reverse())
            {
                app = component(app);
            }

            _application = app;

            return this;
        }

        public ApplicationBuilder Run()
        {
            if (_application == null)
            {
                throw new NotSupportedException("Please invoke the Build method first.");
            }

            _application(new HttpContext());

            return this;
        }
    }
}
