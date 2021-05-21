﻿using System;
using System.IO;

namespace dotnetCampus.Ipc.PipeCore.Context
{
    /// <summary>
    /// 对方连接的事件参数
    /// 用于在框架内部使用
    /// </summary>
    internal class IpcInternalPeerConnectedArgs : EventArgs
    {
        /// <summary>
        /// 创建对方连接的事件参数
        /// </summary>
        /// <param name="peerName"></param>
        /// <param name="namedPipeServerStream"></param>
        /// <param name="ack"></param>
        /// <param name="serverStreamMessageReader"></param>
        internal IpcInternalPeerConnectedArgs(string peerName, Stream namedPipeServerStream, in Ack ack,
            ServerStreamMessageReader serverStreamMessageReader)
        {
            PeerName = peerName;
            NamedPipeServerStream = namedPipeServerStream;
            Ack = ack;
            ServerStreamMessageReader = serverStreamMessageReader;
        }

        /// <summary>
        /// 对方的服务器连接名
        /// </summary>
        public string PeerName { get; }

        /// <summary>
        /// 用于接受对方的通讯服务，只读
        /// </summary>
        public Stream NamedPipeServerStream { get; }

        /// <summary>
        /// 消息编号
        /// </summary>
        public Ack Ack { get; }

        internal ServerStreamMessageReader ServerStreamMessageReader { get; }
    }
}
