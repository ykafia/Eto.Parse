using System;
using Eto.Parse.Parsers;
using System.Collections.Generic;
using System.IO;
using System.CodeDom.Compiler;

namespace Eto.Parse.Writers
{
	public class TextParserWriter : ParserWriter<TextParserWriterArgs>
	{
		public string Indent { get; set; }

		public TextParserWriter(IDictionary<Type, IParserWriterHandler<TextParserWriterArgs>> writers)
			: base(writers)
		{
		}

		public TextParserWriter()
		{
		}

		public string Write(Parser parser)
		{
			var writer = new StringWriter();
			Write(parser, writer);
			return writer.ToString();
		}

		public void Write(Parser parser, TextWriter writer)
		{
			var args = new TextParserWriterArgs
			{
				Output = new IndentedTextWriter(writer, Indent), 
				Writer = this
			};
			WriteParser(args, parser);
		}

		public override string WriteParser(TextParserWriterArgs args, Parser parser)
		{
			return base.WriteParser(args, parser);
		}
	}
}
