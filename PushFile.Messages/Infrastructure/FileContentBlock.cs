﻿using BeetleX;
using BeetleX.Packets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushFile.Messages.Infrastructure
{
	[MessageType(1)]
	[ProtoContract]
	public class FileContentBlock : IMessageSubmitHandler
	{
		[ProtoMember(1)]
		public string FileName { get; set; }
		[ProtoMember(2)]
		public bool Eof { get; set; }
		[ProtoMember(3)]
		public int Index { get; set; }
		[ProtoMember(4)]
		public byte[] Data { get; set; }
		[ProtoMember(5)]
		public string AppName { get; set; }
		[ProtoMember(6)]
		public string Version { get; set; }

		public Action<FileContentBlock> Completed { get; set; }

		public void Execute(object sender, object message)
		{
			Completed?.Invoke(this);
		}
	}
}