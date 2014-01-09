using System.Collections.Generic;
using System.Linq;

using Sandbox.SOA.Portal;

using Xunit;

namespace Sandbox.SOA.Tests
{
    public class WebApiClientCommandHandlerTest
    {
        [Fact]
        public void simple_object_to_dictionary()
        {
            var input = new {One = "1"};

            var output = WebApiClientCommandHandler.ToDictionary(input)
                                                   .Single();

            Assert.Equal("One", output.Key);
            Assert.Equal("1", output.Value);
        }

        [Fact]
        public void nested_object_to_dictionary()
        {
            var input = new {One = "1", Two = new {Three = "2.3"}};

            var output = WebApiClientCommandHandler.ToDictionary(input);

            Assert.Equal(2, output.Count);

            var last = output.Last();

            Assert.Equal("Two.Three", last.Key);
            Assert.Equal("2.3", last.Value);
        }

        [Fact]
        public void merge_url_not_case_sensitive()
        {
            IDictionary<string, string> values
                = new Dictionary<string, string>
                    {
                        {"THING.one", "THINGVALUE"}
                    };

            var result = WebApiClientCommandHandler
                .MergeUrl("/blaa/{thing.one}/lala", values);

            Assert.Equal("/blaa/THINGVALUE/lala", result);
        }

        [Fact]
        public void merge_url_escapes_values()
        {
            IDictionary<string, string> values
                = new Dictionary<string, string>
                    {
                        {"THING.one", "THING/VALUE"}
                    };

            var result = WebApiClientCommandHandler
                .MergeUrl("/blaa/{thing.one}/lala", values);

            Assert.Equal("/blaa/THING%2FVALUE/lala", result);
        }

        [Fact]
        public void merge_url_removes_found()
        {
            IDictionary<string, string> values
                = new Dictionary<string, string>
                    {
                        {"THING.one", "THING/VALUE"},
                        {"NOTFOUND", "THINGVALUE"}
                    };

            WebApiClientCommandHandler
                .MergeUrl("/blaa/{thing.one}/lala", values);

            Assert.Equal("NOTFOUND", values.Single().Key);
        }

        [Fact]
        public void to_query_string()
        {
            IDictionary<string, string> values
                = new Dictionary<string, string>
                    {
                        {"THING.one", "THING/VALUE"},
                        {"NOTFOUND", "THINGVALUE"}
                    };

            var result = WebApiClientCommandHandler
                .ToQueryString( values);

            Assert.Equal("THING.one=THING%2FVALUE&NOTFOUND=THINGVALUE", result);
        }

    }
}