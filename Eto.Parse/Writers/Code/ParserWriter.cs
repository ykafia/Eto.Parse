using System;
using Eto.Parse.Parsers;
using System.Collections.Generic;
using System.IO;

namespace Eto.Parse.Writers.Code
{

	public class ParserWriter<T> : IParserWriterHandler<TextParserWriterArgs>
		where T: Parser
	{
		public virtual string GetName(TextParserWriterArgs args, T parser)
		{
			return args.GenerateName(parser);
		}

		public virtual void WriteObject(TextParserWriterArgs args, T parser, string name)
		{
			var type = parser.GetType();
			args.Output.WriteLine("var {0} = new {1}.{2}();", name, type.Namespace, type.Name);
		}

		public virtual void WriteContents(TextParserWriterArgs args, T parser, string name)
		{
		}

		string IParserWriterHandler<TextParserWriterArgs>.Write(TextParserWriterArgs args, Parser parser)
		{
			var name = GetName(args, (T)parser);
			if (args.IsDefined(name))
				return name;
			if (!args.Parsers.Contains(parser))
			{
				WriteObject(args, (T)parser, name);
				args.Push(parser);
				WriteContents(args, (T)parser, name);
				args.Pop();
			}
			return name;
		}
	}
	
}
