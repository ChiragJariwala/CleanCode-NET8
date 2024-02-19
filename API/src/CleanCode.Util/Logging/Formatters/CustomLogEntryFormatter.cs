// Copyright 2015-2020 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Modifications Copyright 2021 Vishal Patel

using System.Collections;
using System.Runtime.Serialization;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using Serilog.Parsing;

namespace CleanCode.Util.Logging.Formatters
{
    public class CustomLogEntryFormatter : ITextFormatter
    {
        /// <summary>
        /// Gets or sets a value indicating whether the message is rendered into JSON.
        /// </summary>
        protected bool IsRenderingMessage { get; set; }

        /// <summary>
        /// Format the log event into the output.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        public void Format(LogEvent logEvent, TextWriter output)
        {
            try
            {
                var buffer = new StringWriter();
                FormatContent(logEvent, buffer);

                // If formatting was successful, write to output
                output.WriteLine(buffer.ToString());
            }
            catch (Exception e)
            {
                LogNonFormattableEvent(logEvent, e);
            }
        }

        private void FormatContent(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (output == null) throw new ArgumentNullException(nameof(output));

            output.Write("{\"Timestamp\":\"");
            output.Write(logEvent.Timestamp.UtcDateTime.ToString("o"));

            output.Write("\",\"LogLevel\":\"");
            output.Write(logEvent.Level);

            output.Write("\",\"Message\":");
            var message = logEvent.MessageTemplate.Render(logEvent.Properties);
            JsonValueFormatter.WriteQuotedJsonString(message.Replace("\"", ""), output);

            if (logEvent.Exception != null)
            {
                var innermostException = GetInnermostException(logEvent.Exception);

                output.Write(",\"ErrorMessage\":");
                output.Write($"\"{innermostException.Message}\"");

                WriteCustomInnermostException(innermostException, ",", output);
            }

            output.Write(",");

            if (logEvent.Properties.Count != 0)
            {
                WriteProperties(logEvent.Properties, output);
            }

            output.Write('}');
        }

        private static void WriteProperties(
            IReadOnlyDictionary<string, LogEventPropertyValue> properties,
            TextWriter output)
        {
            var precedingDelimiter = "";

            var propertiesToOmit = new List<string>
            {
                "SourceContext", "ActionId", "RequestId", "ConnectionId", "EventId", "logMessage", "routeName",
                "method", "Scope"
            };

            foreach (var property in properties)
            {
                if (propertiesToOmit.Any(a => a == property.Key))
                    continue;

                var customKey = property.Key.ToLower() switch
                {
                    "machinename" => "HostName",
                    "elapsedmilliseconds" => "Duration",
                    "eventtype" => "EventType",
                    "description" => "Description",
                    _ => property.Key
                };

                if (string.Equals(customKey, "HttpContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (property.Value is not StructureValue sv) continue;
                    foreach (var prop in sv.Properties)
                    {
                        output.Write(precedingDelimiter);
                        precedingDelimiter = ",";
                        JsonValueFormatter.WriteQuotedJsonString(prop.Name, output);
                        output.Write(':');
                        ValueFormatter.Instance.Format(prop.Value, output);
                    }
                }
                else
                {
                    output.Write(precedingDelimiter);
                    precedingDelimiter = ",";
                    JsonValueFormatter.WriteQuotedJsonString(customKey, output);
                    output.Write(':');
                    ValueFormatter.Instance.Format(property.Value, output);
                }
            }

            //var requestPath = properties.FirstOrDefault(x => x.Key.ToLower() == "requestpath").Value;
            //if (!string.IsNullOrEmpty(requestPath.ToString()))
            //{
            //    var requestPathValues = requestPath.ToString().Replace("\"", "").Split("/").ToArray();
            //    if (requestPathValues.Length != 0)
            //    {
            //        precedingDelimiter = ",";

            //        output.Write(precedingDelimiter);
            //        JsonValueFormatter.WriteQuotedJsonString("ServiceName", output);
            //        output.Write(':');
            //        JsonValueFormatter.WriteQuotedJsonString(requestPathValues[3], output);

            //        output.Write(precedingDelimiter);
            //        JsonValueFormatter.WriteQuotedJsonString("ServiceVersion", output);
            //        output.Write(':');
            //        JsonValueFormatter.WriteQuotedJsonString(requestPathValues[2], output);
            //        output.Write(precedingDelimiter);
            //    }
            //}
        }

        private void WriteCustomInnermostException(Exception exception, string delimiter, TextWriter output)
        {
            var si = new SerializationInfo(exception.GetType(), new FormatterConverter());
            var sc = new StreamingContext();
            exception.GetObjectData(si, sc);

            var stackTrace = si.GetString("StackTraceString");
            var source = si.GetString("Source");

            output.Write(delimiter);
            JsonValueFormatter.WriteQuotedJsonString("ExceptionName", output);
            output.Write(':');
            JsonValueFormatter.WriteQuotedJsonString(exception.GetType().Name, output);

            output.Write(delimiter);
            JsonValueFormatter.WriteQuotedJsonString("ExceptionSource", output);
            output.Write(':');
            JsonValueFormatter.WriteQuotedJsonString(source, output);

            output.Write(delimiter);
            JsonValueFormatter.WriteQuotedJsonString("StackTrace", output);
            output.Write(':');
            JsonValueFormatter.WriteQuotedJsonString(stackTrace, output);

            output.Write(delimiter);
            JsonValueFormatter.WriteQuotedJsonString("ExceptionSource", output);
            output.Write(':');
            JsonValueFormatter.WriteQuotedJsonString(source, output);

            foreach (DictionaryEntry currentData in exception.Data)
            {
                output.Write(delimiter);
                JsonValueFormatter.WriteQuotedJsonString(currentData.Key.ToString(), output);
                output.Write(':');
                JsonValueFormatter.WriteQuotedJsonString(currentData.Value?.ToString(), output);
            }
        }

        private static void WriteRenderings(
            IEnumerable<IGrouping<string, PropertyToken>> tokensWithFormat,
            IReadOnlyDictionary<string, LogEventPropertyValue> properties,
            TextWriter output)
        {
            output.Write(",\"Renderings\":{");

            var rdelim = "";
            foreach (var ptoken in tokensWithFormat)
            {
                output.Write(rdelim);
                rdelim = ",";

                JsonValueFormatter.WriteQuotedJsonString(ptoken.Key, output);
                output.Write(":[");

                var fdelim = "";
                foreach (var format in ptoken)
                {
                    output.Write(fdelim);
                    fdelim = ",";

                    output.Write("{\"Format\":");
                    JsonValueFormatter.WriteQuotedJsonString(format.Format, output);

                    output.Write(",\"Rendering\":");
                    var sw = new StringWriter();
                    format.Render(properties, sw);
                    JsonValueFormatter.WriteQuotedJsonString(sw.ToString(), output);
                    output.Write('}');
                }

                output.Write(']');
            }

            output.Write('}');
        }

        private static void LogNonFormattableEvent(LogEvent logEvent, Exception e)
        {
            SelfLog.WriteLine(
                "Event at {0} with message template {1} could not be formatted into JSON and will be dropped: {2}",
                logEvent.Timestamp.ToString("o"),
                logEvent.MessageTemplate.Text,
                e);
        }

        private static Exception GetInnermostException(Exception exception)
        {
            while (true)
            {
                if (exception.InnerException == null) return exception;
            }
        }
    }

    internal static class ValueFormatter
    {
        internal static readonly JsonValueFormatter Instance = new JsonValueFormatter();
    }
}